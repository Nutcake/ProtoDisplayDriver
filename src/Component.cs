namespace ProtoDisplayDriver;

public abstract class Component
{
    private Node? _node;

    public Node Node
    {
        get => _node!;
        set
        {
            if (_node != null)
            {
                throw new HasNodeException();
            }

            _node = value;
            OnAttach();
        }
    }

    public virtual void Update(float delta)
    {
    }

    public virtual void Draw(float[,] canvas, int width, int height, float delta)
    {
    }
    
    public virtual void OnAttach()
    {
        
    }
}