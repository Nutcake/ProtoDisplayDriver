using RPiRgbLEDMatrix;

namespace ProtoDisplayDriver;

public abstract class Component
{
    public abstract void Update(Node node, float delta);

    public abstract void Draw(Node node, RGBLedCanvas canvas, float delta);
}
