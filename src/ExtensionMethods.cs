using RPiRgbLEDMatrix;

namespace ProtoDisplayDriver;

public static class ExtensionMethods
{
    public static Color Multiply(this Color color, float value)
    {
        return new Color((byte)(color.R * value), (byte)(color.G * value), (byte)(color.B * value));
    }

    public static Color Multiply(this Color color, Color other)
    {
        return new Color((byte)(color.R * other.R / 255f), (byte)(color.G * other.G / 255f), (byte)(color.B * other.B / 255f));
    }

    public static Color Add(this Color a, Color b)
    {
        return new Color(
            (byte)Math.Clamp(a.R + b.R, byte.MinValue, byte.MaxValue),
            (byte)Math.Clamp(a.G + b.G, byte.MinValue, byte.MaxValue),
            (byte)Math.Clamp(a.B + b.B, byte.MinValue, byte.MaxValue)
        );
    }

    public static float Luminance(this Color color)
    {
        return 0.299f * color.R + 0.587f * color.G + 0.114f * color.B;
    }

    public static float Value(this Color color)
    {
        return Math.Max(color.R, Math.Max(color.G, color.B));
    }
}