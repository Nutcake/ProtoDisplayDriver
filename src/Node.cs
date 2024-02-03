using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver;

public class Node
{
    public Vector2 Position { get; set; }
    public Vector3 Rotation { get; set; }

    private readonly List<Component> _components = new();

    public Node(Vector2? position=null, Vector3? rotation=null)
    {
        Position = position ?? new Vector2();
        Rotation = rotation ?? new Vector3();
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