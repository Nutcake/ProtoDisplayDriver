using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver.Components;

public class PositionWiggler : Wiggler
{
    public PositionWiggler(Vector2 speed, Vector2 seed, Vector2 magnitude, Vector2 offset) : base(speed, seed, magnitude, offset)
    {
    }

    public override void Update(Node node, float delta)
    {
        var wiggle =  Wiggle();
        node.Position = new Vector2
        {
            X = wiggle.X,
            Y = wiggle.Y
        };
    }
}