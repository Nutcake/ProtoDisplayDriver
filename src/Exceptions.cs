namespace ProtoDisplayDriver;

public class HasParentException : Exception
{
    public HasParentException()
    {
    }
    
    public HasParentException(string message)
        : base($"Child already has a parent. {message}")
    {
    }

    public HasParentException(string message, Exception inner)
        : base($"Child already has a parent. {message}", inner)
    {
    }
}