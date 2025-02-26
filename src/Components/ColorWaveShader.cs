using RPiRgbLEDMatrix;

namespace ProtoDisplayDriver.Components;

public class ColorWaveShader : Component
{
    public ColorWaveShader(float speed)
    {
        _speed = speed;
    }

    private float _offset;
    private float _speed;

    public override void Draw(Color[,] canvas, int width, int height, float delta)
    {
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var pixel = canvas[x, y];
                if (pixel.Value() < 10) continue;
                canvas[x, y] = new Color(
                    (byte)(127 * (1 + MathF.Sin((x + y) / 4f + _offset))),
                    (byte)(127 * (1 + MathF.Sin((x + y) / 4f + _offset + 2 * MathF.PI / 3))),
                    (byte)(127 * (1 + MathF.Sin((x + y) / 4f + _offset + 4 * MathF.PI / 3)))
                );
            }
        }

        _offset += delta * _speed;
    }
}