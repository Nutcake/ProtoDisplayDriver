using Microsoft.Xna.Framework;

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
        node.Position = new Vector2(newX, newY);
        _frame++;
    }
    
    public static float PingPong(float val, float length)
    {
        return MathF.Abs((float) ((val + (double) length) % (length * 2.0)) - length);
    }

}