namespace ProtoDisplayDriver.Components;

public class GaussianFilter : Component
{
    private static readonly float[,] _kernel =
    {
        { 0.0625f, 0.125f, 0.0625f },
        { 0.1250f, 0.250f, 0.1250f },
        { 0.0625f, 0.125f, 0.0625f },
    };
    
    public override void Draw(float[,] canvas, int width, int height, float delta)
    {
        var target = new float[width, height];
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                for (var kY = 0; kY < _kernel.GetLength(0); kY++)
                {
                    for (var kX = 0; kX < _kernel.GetLength(1); kX++)
                    {
                        var offsX = x + (kX - 1);
                        var offsY = y + (kY - 1);
                        if (offsX < 0 || offsX >= width || offsY < 0 || offsY >= height || (kX == 1 && kY == 1)) continue;
                        target[x, y] += _kernel[kX, kY] * canvas[offsX, offsY];
                    }
                }
            }
        }
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                canvas[x, y] = target[x, y];
            }
        }
    }
}