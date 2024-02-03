using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver;

public class Node
{
    public Vector2 Position { get; set; }
    public Vector3 Rotation { get; set; }
    public Vector2 Scale { get; set; }

    public Node? Parent { get; set; }

    private readonly HashSet<Node> _children = new ();

    public Vector2 GlobalPosition => (Parent?.GlobalPosition ?? Vector2.Zero) + Position;
    public Vector2 GlobalScale => (Parent?.GlobalScale ?? Vector2.One) * Scale;
    public Vector3 GlobalRotation => (Parent?.GlobalRotation ?? Vector3.Zero) + Rotation;

    private readonly List<Component> _components = new();

    public Node(Vector2? position=null, Vector3? rotation=null, Vector2? scale=null)
    {
        Position = position ?? new Vector2();
        Rotation = rotation ?? new Vector3();
        Scale = scale ?? new Vector2(1, 1);
    }

    public void AddChild(Node node)
    {
        if (node.Parent != null)
        {
            throw new HasParentException();
        }

        node.Parent = this;
        _children.Add(node);
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

        foreach (var child in _children)
        {
            child.Draw(canvas, width, height, delta);
        }
    }

    public void Update(float delta)
    {
        foreach (var component in _components)
        {
            component.Update(this, delta);
        }

        foreach (var child in _children)
        {
            child.Update(delta);
        }
    }
}