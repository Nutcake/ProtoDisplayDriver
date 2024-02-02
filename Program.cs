using ProtoDisplayDriver.Components;
using RPiRgbLEDMatrix;
using SixLabors.ImageSharp;
using Color = RPiRgbLEDMatrix.Color;

namespace ProtoDisplayDriver
{
    class Program
    {
        static void Main(string[] _)
        {
            using var matrix = new RGBLedMatrix(32, 2, 1);

            var virtualCanvas = new World(matrix);
            var eyeNode = new Node(new PointF(2f, 2f));
            eyeNode.AddComponent(new ImageRenderer("./res/EyeSharp.png"));
            eyeNode.AddComponent(new Wiggler(0, 0.05f, 0, 50));
            virtualCanvas.AddNode(eyeNode);
            virtualCanvas.Loop();
        }
    }
}

namespace ExtensionMethods
{
    public static class Extensions
    {
        public static Color Multiply(this Color a, float factor)
        {
            return new Color
            {
                R = (byte)(a.R * factor),
                G = (byte)(a.G * factor),
                B = (byte)(a.B * factor)
            };
        }
    }
}