using SixLabors.ImageSharp;

namespace ProtoDisplayDriver.Components;

public class Wiggler : Component
{
    private float _xAmplitude;
    private float _yAmplitude;
    private float _xFrequency;
    private float _yFrequency;
    private long _frame;

    public Wiggler(float xAmplitude, float yAmplitude, float xFrequency, float yFrequency)
    {
        _xAmplitude = xAmplitude;
        _yAmplitude = yAmplitude;
        _xFrequency = xFrequency;
        _yFrequency = yFrequency;
    }

    public override void Update(Node node, float delta)
    {
        var oldPos = node.Position;
        var newY = oldPos.Y + MathF.Sin((_frame/1000f) * _yFrequency) * _yAmplitude;
        var newX = oldPos.X + MathF.Sin((_frame/1000f) * _xFrequency) * _xAmplitude;
        node.Position = new PointF(newX, newY);
        _frame++;
    }

    public override void Draw(Node node, float[,] canvas, int width, int height, float delta)
    {
    }
}