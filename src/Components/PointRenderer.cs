namespace ProtoDisplayDriver.Components;

public class PointRenderer : Component
{
    public override void Draw(float[,] canvas, int width, int height, float delta)
    {
        var pos = Node.GlobalPosition;
        if (pos.X < 0 || pos.X >= width || pos.Y < 0 || pos.Y >= height) return;
        canvas[(int)MathF.Round(pos.X), (int)MathF.Round(pos.Y)] = 1;
    }
}