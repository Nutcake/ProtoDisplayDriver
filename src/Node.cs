using ExtensionMethods;
using RPiRgbLEDMatrix;
using SixLabors.ImageSharp;
using Color = RPiRgbLEDMatrix.Color;

namespace ProtoDisplayDriver;

public class Node
{
    private PointF _position;

    public PointF Position
    {
        get => _position;
        set => _position = value;
    }

    private readonly List<Component> _components = new();

    public Node(PointF position)
    {
        _position = position;
    }

    public void AddComponent(Component component)
    {
        _components.Add(component);
    }

    public void Draw(float[,] canvas, int width, int height, float delta)
    {
        foreach (var component in _components)
        {
            component.Draw(this, canvas, width, height, delta);
        }
    }

    public void Update(float delta)
    {
        foreach (var component in _components)
        {
            component.Update(this, delta);
        }
    }
}