using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = RPiRgbLEDMatrix.Color;

namespace ProtoDisplayDriver.Components;

public class ImageRenderer : Component
{
    protected Image<Rgba32> Image;
    private Color _color;

    public ImageRenderer(Image<Rgba32> image, Color? color = null)
    {
        Image = image;
        _color = color ?? new Color(255, 255, 255);
    }

    public ImageRenderer(string path, Color? color = null) : this(SixLabors.ImageSharp.Image.Load<Rgba32>(path), color)
    {
    }

    public override void Draw(Color[,] canvas, int width, int height, float delta)
    {
        var pivot = new PointF(Image.Width / 2f, Image.Height / 2f);
        var mat4 = Matrix4x4.CreateFromYawPitchRoll(Node.GlobalRotation.X, Node.GlobalRotation.Y, Node.GlobalRotation.Z);
        mat4.Translation = new Vector3(Node.GlobalPosition.X + pivot.X, Node.GlobalPosition.Y + pivot.Y, 0);
        for (var imgY = 0; imgY < Image.Height; imgY++)
        {
            for (var imgX = 0; imgX < Image.Width; imgX++)
            {
                var pixel = Image[imgX, imgY];
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
    }
}