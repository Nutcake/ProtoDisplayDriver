using SixLabors.ImageSharp;

namespace ProtoDisplayDriver.Components;

public class Wiggler : Component
{
    private float _xAmplitude;
    private float _yAmplitude;
    private float _xFrequency;
    private float _yFrequency;
    public Wiggler(float xAmplitude, float yAmplitude, float xFrequency, float yFrequency)
    {
        _xAmplitude = xAmplitude;
        _yAmplitude = yAmplitude;
        _xFrequency = xFrequency;
        _yFrequency = yFrequency;
    }
    
    public override void Update(Node node, int frame)
    {
        var oldPos = node.Position;
        var newY = oldPos.Y + MathF.Sin(frame*_yFrequency)*_yAmplitude;
        node.Position = new PointF(oldPos.X, newY);
    }
}