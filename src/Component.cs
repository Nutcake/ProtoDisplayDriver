using RPiRgbLEDMatrix;

namespace ProtoDisplayDriver;

public abstract class Component
{
    public virtual void Update(Node node, float delta)
    {
    }

    public virtual void Draw(Node node, float[,] canvas, int width, int height, float delta)
    {
    }
}