using Microsoft.Xna.Framework;
using ProtoDisplayDriver.Components;
using RPiRgbLEDMatrix;
using Color = RPiRgbLEDMatrix.Color;

namespace ProtoDisplayDriver
{
    class Program
    {
        static void Main(string[] _)
        {
            using var matrix = new RGBLedMatrix(32, 2, 1);

            var virtualCanvas = new World(matrix);
            var eyeNode = new Node(new Vector2(4f, 1f), rotation: new Vector3(0, 0, 0.1f));
            eyeNode.AddComponent(new Wiggler(0, 0.03f, 0, 50));
            eyeNode.AddComponent(new Rotator(new Vector3(0.1f, 0, 0)));
            eyeNode.AddComponent(new ImageRenderer("./res/EyeSharp.png"));
            virtualCanvas.AddNode(eyeNode);

            var mouthNode = new Node(new Vector2(25, 21f));
            mouthNode.AddComponent(new ImageRenderer("./res/Mouth.png"));
            virtualCanvas.AddNode(mouthNode);

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