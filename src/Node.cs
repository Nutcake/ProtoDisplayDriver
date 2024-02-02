using RPiRgbLEDMatrix;
using SixLabors.ImageSharp;

namespace ProtoDisplayDriver;

public class Node
{
    private PointF _position;

    public PointF Position
    {
        get => _position;
        set => _position = value;
    }

    private float _rotation;
    private HashSet<Component> _components = new();

    public Node(PointF position)
    {
        _position = position;
    }

    public void AddComponent(Component component)
    {
        _components.Add(component);
    }

    public void Draw(RGBLedCanvas canvas, float delta)
    {
        foreach (var component in _components)
        {
            component.Draw(this, canvas, delta);
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