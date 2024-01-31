using RPiRgbLEDMatrix;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = RPiRgbLEDMatrix.Color;

namespace ProtoDisplayDriver;

public class Node
{
    private Image<L8> _image;
    private PointF _position;

    public PointF Position
    {
        get => _position;
        set => _position = value;
    }

    private float _rotation;
    private HashSet<Component> _components = new();

    public Node(PointF position, Image<L8> image)
    {
        _position = position;
        _image = image;
    }

    public Node(PointF position, string path) : this(position, Image.Load<L8>(path))
    {
    }

    public void AddComponent(Component component)
    {
        _components.Add(component);
    }

    public void Draw(RGBLedCanvas canvas)
    {
        var offsetY = float.Max(0, _position.Y * canvas.Height);
        var offsetX = float.Max(0, _position.X * canvas.Width);
        for (var y = offsetY; y < float.Min(offsetY + _image.Height - 1, canvas.Height - 1); y++)
        {
            for (var x = offsetX; x < float.Min(offsetX + _image.Width - 1, canvas.Width - 1); x++)
            {
                var convX = (int)float.Round(x);
                var convY = (int)float.Round(y);
                var imgX = convX - (int)float.Round(offsetX);
                var imgY = convY - (int)float.Round(offsetY);
                var value = _image[imgX, imgY].PackedValue;
                var brightnessFactor = (x - float.Floor(x) + y - float.Floor(y)) / 2;
                canvas.SetPixel(convX, convY,
                    new Color((int)(value * brightnessFactor), (int)(value * brightnessFactor) / 2, 0));
            }
        }
    }

    public void Update(int frame)
    {
        foreach (var component in _components)
        {
            component.Update(this, frame);
        }
    }
}