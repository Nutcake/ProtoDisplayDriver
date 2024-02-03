using Microsoft.Xna.Framework;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ProtoDisplayDriver.Components;

public class ImageRenderer : Component
{
    private Image<Rgba32> _image;

    public ImageRenderer(Image<Rgba32> image)
    {
        _image = image;
    }

    public ImageRenderer(string path) : this(Image.Load<Rgba32>(path))
    {
    }


    public override void Draw(Node node, float[,] canvas, int width, int height, float delta)
    {
        var pivot = new PointF(_image.Width / 2f, _image.Height / 2f);
        var mat = Matrix.CreateFromYawPitchRoll(node.Rotation.X, node.Rotation.Y, node.Rotation.Z);
        mat.Translation = new Vector3(node.Position.X + pivot.X, node.Position.Y + pivot.Y, 0);
        for (var imgY = 0; imgY < _image.Height; imgY++)
        {
            for (var imgX = 0; imgX < _image.Width; imgX++)
            {
                var pixel = _image[imgX, imgY];
                if (pixel.A < 1) continue;

                var (tfX, tfY) = Vector2.Transform(new Vector2(imgX - pivot.X, imgY - pivot.Y), mat);
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
}