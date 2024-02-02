using ExtensionMethods;
using RPiRgbLEDMatrix;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = RPiRgbLEDMatrix.Color;

namespace ProtoDisplayDriver.Components;

public class ImageRenderer : Component
{
    private Image<Rgba32> _image;
    private Color _color;

    public ImageRenderer(Image<Rgba32> image, Color? color = null)
    {
        _color = color ?? new Color(255, 100, 0);
        _image = image;
    }

    public ImageRenderer(string path, Color? color = null) : this(Image.Load<Rgba32>(path), color)
    {
    }


    public override void Update(Node node, float delta)
    {
    }


    public override void Draw(Node node, RGBLedCanvas canvas, float delta)
    {
        var values = new float[canvas.Width, canvas.Height];

        for (var imgY = 0; imgY < _image.Height; imgY++)
        {
            for (var imgX = 0; imgX < _image.Width; imgX++)
            {
                var pixel = _image[imgX, imgY];
                if (pixel.A < 1) continue;

                var xFloor = (int)float.Floor(imgX + node.Position.X);
                var xCeil = (int)float.Ceiling(imgX + node.Position.X);
                var yFloor = (int)float.Floor(imgY + node.Position.Y);
                var yCeil = (int)float.Ceiling(imgY + node.Position.Y);
                if (xFloor < 0 || xFloor >= canvas.Width ||
                    xCeil < 0 || xCeil >= canvas.Width ||
                    yFloor < 0 || yFloor >= canvas.Height ||
                    yCeil < 0 || yCeil >= canvas.Height) continue;

                var deltaXLow = 1 - (imgX + node.Position.X - xFloor);
                var deltaYLow = 1 - (imgY + node.Position.Y - yFloor);
                var deltaXHigh = 1 - (xCeil - (imgX + node.Position.X));
                var deltaYHigh = 1 - (yCeil - (imgY + node.Position.Y));

                var ff = float.Clamp(values[xFloor, yFloor] + (deltaXLow + deltaYLow) / 4, 0, 1);
                values[xFloor, yFloor] = ff;
                var cf = float.Clamp(values[xCeil, yFloor] + (deltaXHigh + deltaYLow) / 4, 0, 1);
                values[xCeil, yFloor] = cf;
                var cc = float.Clamp(values[xCeil, yCeil] + (deltaXHigh + deltaYHigh) / 4, 0, 1);
                values[xCeil, yCeil] = cc;
                var fc = float.Clamp(values[xFloor, yCeil] + (deltaXLow + deltaYHigh) / 4, 0, 1);
                values[xFloor, yCeil] = fc;
            }
        }

        var colors = new Color[values.Length];
        var index = 0;
        for (int y = 0; y < canvas.Height; y++)
        {
            for (int x = 0; x < canvas.Width; x++)
            {
                colors[index] = _color.Multiply(values[x, y]);
                index++;
            }
        }

        canvas.SetPixels(0, 0, canvas.Width, canvas.Height, colors);

    }
}