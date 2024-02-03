using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver.Components;

public class RotationWiggler : Wiggler
{
    public RotationWiggler(Vector3 speed, Vector3 magnitude, Vector3? offset = null, Vector3? seed = null) : base(speed, seed ?? new Vector3(0.2f, 0.5f, 0.7f), magnitude,
        offset ?? Vector3.Zero)
    {
    }

    public override void Update(Node node, float delta)
    {
        
    }
}