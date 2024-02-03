using Microsoft.Xna.Framework;

namespace ProtoDisplayDriver.Components;

public abstract class Wiggler : Component
{
    public Vector3 Speed;
    public Vector3 Seed;
    public Vector3 Magnitude;
    public Vector3 Offset;

    protected Wiggler(Vector3 speed, Vector3 seed, Vector3 magnitude, Vector3 offset)
    {
        Speed = speed;
        Seed = seed;
        Magnitude = magnitude;
        Offset = offset;
    }

    protected Wiggler(Vector2 speed, Vector2 seed, Vector2 magnitude, Vector2 offset)
    {
        Speed = new Vector3(speed, 0);
        Seed = new Vector3(seed, 0);
        Magnitude = new Vector3(magnitude, 0);
        Offset = new Vector3(offset, 0);
    }

    protected Vector3 Wiggle()
    {
        var tick = Environment.TickCount64 / 10000f;
        return Offset + new Vector3
        {
            X = (Noise.Generate(tick * Speed.X + Seed.X) * 2 - 1) * Magnitude.X,
            Y = (Noise.Generate(tick * Speed.Y + Seed.Y) * 2 - 1) * Magnitude.Y,
            Z = (Noise.Generate(tick * Speed.Z + Seed.Z) * 2 - 1) * Magnitude.Z
        };
    }
}