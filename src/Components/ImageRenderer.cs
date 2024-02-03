using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = RPiRgbLEDMatrix.Color;

namespace ProtoDisplayDriver.Components;

public class ImageRenderer : Component
{
    private Image<Rgba32> _image;
    private float _rotation;

    public ImageRenderer(Image<Rgba32> image, float rotation)
    {
        _image = image;
        _rotation = rotation;
    }

    public ImageRenderer(string path, float rotation=0, Color? color = null) : this(Image.Load<Rgba32>(path), rotation)
    {
    }


    public override void Update(Node node, float delta)
    {
    }


    public override void Draw(Node node, float[,] canvas, int width, int height, float delta)
    {
        for (var imgY = 0; imgY < _image.Height; imgY++)
        {
            for (var imgX = 0; imgX < _image.Width; imgX++)
            {
                var pixel = _image[imgX, imgY];
                if (pixel.A < 1) continue;

                var (tfX, tfY) = _rotatePoint(new PointF(imgX + node.Position.X, imgY + node.Position.Y));

                var xFloor = (int)float.Floor(tfX);
                var xCeil = (int)float.Ceiling(tfX);
                var yFloor = (int)float.Floor(tfY);
                var yCeil = (int)float.Ceiling(tfY);
                if (xFloor < 0 || xFloor >= width ||
                    xCeil < 0 || xCeil >= width ||
                    yFloor < 0 || yFloor >= height ||
                    yCeil < 0 || yCeil >= height) continue;

                var deltaXLow = 1 - (tfX - xFloor);
                var deltaYLow = 1 - (tfY - yFloor);
                var deltaXHigh = 1 - (xCeil - tfX);
                var deltaYHigh = 1 - (yCeil - tfY);

                var ff = float.Clamp(canvas[xFloor, yFloor] + (deltaXLow + deltaYLow) / 4, 0, 1);
                canvas[xFloor, yFloor] = ff;
                var cf = float.Clamp(canvas[xCeil, yFloor] + (deltaXHigh + deltaYLow) / 4, 0, 1);
                canvas[xCeil, yFloor] = cf;
                var cc = float.Clamp(canvas[xCeil, yCeil] + (deltaXHigh + deltaYHigh) / 4, 0, 1);
                canvas[xCeil, yCeil] = cc;
                var fc = float.Clamp(canvas[xFloor, yCeil] + (deltaXLow + deltaYHigh) / 4, 0, 1);
                canvas[xFloor, yCeil] = fc;
            }
        }
    }

    private PointF _rotatePoint(PointF point)
    {
        return new PointF(
            MathF.Cos(_rotation) * point.X - MathF.Sin(_rotation) * point.Y,
            MathF.Sin(_rotation) * point.X + MathF.Cos(_rotation) * point.Y
        );
    }
}