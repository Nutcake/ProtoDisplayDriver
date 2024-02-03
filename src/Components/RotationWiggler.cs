using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver.Components;

public class RotationWiggler : Wiggler
{
    public RotationWiggler(Vector3 speed, Vector3 seed, Vector3 magnitude, Vector3 offset) : base(speed, seed, magnitude, offset)
    {
    }

    public override void Update(Node node, float delta)
    {
        
    }
}