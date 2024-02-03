using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver.Components;

public class ScaleWiggler : Wiggler
{
    public ScaleWiggler(Vector2 speed, Vector2 seed, Vector2 magnitude, Vector2 offset) : base(speed, seed, magnitude, offset)
    {
    }
    
    public override void Update(Node node, float delta)
    {
        var wiggle =  Wiggle();
        node.Scale = new Vector2
        {
            X = wiggle.X,
            Y = wiggle.Y
        };
    }
}