namespace ProtoDisplayDriver.Components;

public class ChildMultiplexer : Component
{
    private readonly List<Node> _nodes;

    private int _index;

    public int NodeCount => _nodes.Count;

    public int Index
    {
        get => _index;
        set
        {
            _index = value;
            Node.RemoveAllChildren();
            Node.AddChild(_nodes[_index]);
        }
    }

    public ChildMultiplexer(List<Node> nodes)
    {
        _nodes = nodes;
    }

    public override void OnAttach()
    {
        Index = 0;
    }
}