namespace ProtoDisplayDriver;

public class HasParentException : Exception
{
    private const string DefaultMessage = "Node already has a parent assigned.";

    public HasParentException() : base(DefaultMessage)
    {
    }

    public HasParentException(string message)
        : base($"{DefaultMessage} {message}")
    {
    }

    public HasParentException(string message, Exception inner)
        : base($"{DefaultMessage} {message}", inner)
    {
    }
}

public class HasNodeException : Exception
{
    private const string DefaultMessage = "Component already has a Node assigned.";

    public HasNodeException() : base(DefaultMessage)
    {
    }

    public HasNodeException(string message)
        : base($"{DefaultMessage} {message}")
    {
    }

    public HasNodeException(string message, Exception inner)
        : base($"{DefaultMessage} {message}", inner)
    {
    }
}