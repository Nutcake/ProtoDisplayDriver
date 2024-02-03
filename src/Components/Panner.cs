using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver.Components;

public class Panner : Component
{
    private readonly float _xAmplitude;
    private readonly float _yAmplitude;
    private readonly float _xFrequency;
    private readonly float _yFrequency;
    private long _frame;

    public Panner(float xAmplitude, float yAmplitude, float xFrequency, float yFrequency)
    {
        _xAmplitude = xAmplitude;
        _yAmplitude = yAmplitude;
        _xFrequency = xFrequency;
        _yFrequency = yFrequency;
    }

    public override void Update(float delta)
    {
        var oldPos = Node.Position;
        var newY = oldPos.Y + MathF.Sin(_frame / 1000f * _yFrequency) * _yAmplitude;
        var newX = oldPos.X + MathF.Sin(_frame / 1000f * _xFrequency) * _xAmplitude;
        Node.Position = new Vector2(newX, newY);
        _frame++;
    }
}