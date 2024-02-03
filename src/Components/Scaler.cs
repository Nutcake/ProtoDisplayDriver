using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver.Components;

public class Scaler : Component
{
    private Vector2 _minValue;
    private Vector2 _maxValue;
    private Vector2 _speed;

    public Scaler(Vector2 minValue, Vector2 maxValue, Vector2 speed)
    {
        _minValue = minValue;
        _maxValue = maxValue;
        _speed = speed;
    }

    public override void Update(Node node, float delta)
    {
        var tick = Environment.TickCount64 / 1000f;
        node.Scale = Vector2.Lerp(_minValue, _maxValue, tick - MathF.Floor(tick));
    }

    
}