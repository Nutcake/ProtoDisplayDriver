using System.Numerics;
using OpenTK.Audio.OpenAL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = RPiRgbLEDMatrix.Color;

namespace ProtoDisplayDriver.Components;

public enum Viseme
{
    None,
    Aa
}

public class MouthFftDisplay : Component
{
    private const int SampleRate = 44100;
    private const int BufferSize = 1024;
    private readonly float[] _recording = new float[BufferSize];
    private readonly ALCaptureDevice _device;
    private double[] _power = new double[513];
    private readonly Color _color = new(255, 80, 0);
    private readonly Image<Rgba32> _image;

    public MouthFftDisplay()
    {
        _image = Image.Load<Rgba32>("./res/MouthThin.png");
        _device = ALC.CaptureOpenDevice("ALSA Default", SampleRate, ALFormat.MonoFloat32Ext, BufferSize);
        if (_device == ALDevice.Null)
        {
            Console.WriteLine($"Failed to open audio device: {AL.GetError()}");
            return;
        }

        ALC.CaptureStart(_device);
    }

    public override void Update(float delta)
    {
        var nSamples = ALC.GetInteger(_device, AlcGetInteger.CaptureSamples);
        if (nSamples > 0)
        {
            ALC.CaptureSamples(_device, _recording, BufferSize);
            var spectrum = FftSharp.FFT.Forward(Array.ConvertAll(_recording, x => (double)x));
            _power = FftSharp.FFT.Power(spectrum)[16..128];
        }
    }

    public override void Draw(Color[,] canvas, int width, int height, float delta)
    {
        var pivot = new PointF(_image.Width / 2f, _image.Height / 2f);
        var xLowBound = int.MaxValue;
        var xHighBound = int.MinValue;
        var yHighBounds = new int[width];

        var mat4 = Matrix4x4.CreateFromYawPitchRoll(Node.GlobalRotation.X, Node.GlobalRotation.Y, Node.GlobalRotation.Z);
        mat4.Translation = new Vector3(Node.GlobalPosition.X + pivot.X, Node.GlobalPosition.Y + pivot.Y, 0);
        for (var imgY = 0; imgY < _image.Height; imgY++)
        {
            for (var imgX = 0; imgX < _image.Width; imgX++)
            {
                var pixel = _image[imgX, imgY];
                if (pixel.A < 1) continue;

                var pixCol = new Color(pixel.R, pixel.G, pixel.B).Multiply(pixel.A / 255f).Multiply(_color);

                // Transform the pixel position according to node transformation
                var tfV = Vector2.Transform(new Vector2(imgX - pivot.X, imgY - pivot.Y), mat4) * Node.GlobalScale;
                var tfX = tfV.X;
                var tfY = tfV.Y;

                // Do not draw if the pixel is outside the canvas
                var xFloor = (int)float.Floor(tfX);
                var xCeil = (int)float.Ceiling(tfX);
                var yFloor = (int)float.Floor(tfY);
                var yCeil = (int)float.Ceiling(tfY);
                if (xFloor < 0 || xFloor >= width ||
                    xCeil < 0 || xCeil >= width ||
                    yFloor < 0 || yFloor >= height ||
                    yCeil < 0 || yCeil >= height) continue;

                xLowBound = Math.Min(xLowBound, (int)tfX);
                xHighBound = Math.Max(xHighBound, (int)tfX);
                if (yHighBounds[imgX] == 0)
                {
                    yHighBounds[imgX] = (int)tfY;
                }

                // Calculate how much this image pixel covers the nearest 4 integer coordinates of the canvas
                var deltaXLow = 1 - (tfX - xFloor);
                var deltaYLow = 1 - (tfY - yFloor);
                var deltaXHigh = 1 - (xCeil - tfX);
                var deltaYHigh = 1 - (yCeil - tfY);

                // Add the image color to the affected pixels based on the amount of coverage
                var ff = canvas[xFloor, yFloor].Add(pixCol.Multiply((deltaXLow + deltaYLow) / 4));
                canvas[xFloor, yFloor] = ff;
                var cf = canvas[xCeil, yFloor].Add(pixCol.Multiply((deltaXHigh + deltaYLow) / 4));
                canvas[xCeil, yFloor] = cf;
                var cc = canvas[xCeil, yCeil].Add(pixCol.Multiply((deltaXHigh + deltaYHigh) / 4));
                canvas[xCeil, yCeil] = cc;
                var fc = canvas[xFloor, yCeil].Add(pixCol.Multiply((deltaXLow + deltaYHigh) / 4));
                canvas[xFloor, yCeil] = fc;
            }
        }

        var boundWidth = xHighBound - xLowBound + 1;

        var binSize = (_power.Length - 1) / boundWidth;
        var bins = new double[boundWidth];

        for (var i = 0; i < boundWidth; i++)
        {
            bins[i] = _power.Skip(i * binSize).Take(binSize).Average();
        }

        for (var binIdx = 0; binIdx < boundWidth; binIdx++)
        {
            var bin = (bins[binIdx] + 100) / 100;
            bin *= Math.Pow(bin * 1.5, 2);
            var boundHeight = 2 * (height - yHighBounds[binIdx]);
            for (var y = -Math.Min(bin * boundHeight, boundHeight); y < Math.Min(bin * boundHeight, boundHeight); y++)
            {
                var xIdx = xLowBound + binIdx;
                var yIdx = -(int)y + yHighBounds[binIdx] + 2;
                if (xIdx < 0 || xIdx >= width) continue;
                if (yIdx < 0 || yIdx >= height) continue;
                canvas[xIdx, yIdx] = new Color(255, 80, 0);
            }
        }
    }
}