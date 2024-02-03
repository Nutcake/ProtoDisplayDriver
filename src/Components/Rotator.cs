using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver.Components;

public class Rotator : Component
{
    private readonly Vector3 _speed;

    public Rotator(Vector3 speed)
    {
        _speed = speed;
    }

    public override void Update(Node node, float delta)
    {
        node.Rotation = node.Rotation + _speed;
    }
}