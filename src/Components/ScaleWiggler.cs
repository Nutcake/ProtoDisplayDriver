using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver.Components;

public class ScaleWiggler : Wiggler
{
    public ScaleWiggler(Vector2 speed, Vector2 magnitude, Vector2? offset = null, Vector2? seed = null) : base(speed, seed ?? new Vector2(0.2f, 0.7f), magnitude,
        offset ?? Vector2.One)
    {
    }

    public override void Update(Node node, float delta)
    {
        var wiggle = Wiggle();
        node.Scale = new Vector2
        {
            X = wiggle.X,
            Y = wiggle.Y
        };
    }
}