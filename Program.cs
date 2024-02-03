using Microsoft.Xna.Framework;
using ProtoDisplayDriver.Components;
using RPiRgbLEDMatrix;
using Color = RPiRgbLEDMatrix.Color;
using Timer = System.Timers.Timer;

namespace ProtoDisplayDriver
{
    class Program
    {
        static void Main(string[] _)
        {
            using var matrix = new RGBLedMatrix(32, 2, 1);
            var random = new Random();
            var virtualCanvas = new World(matrix);

            var blinkTimer = new Timer(2000);

            var eyeNode = new Node(rotation: new Vector3(0, 0, 0.1f));
            eyeNode.AddComponent(
                new PositionWiggler(
                    new Vector2(2f, 2f),
                    new Vector2((float)random.NextDouble(), (float)random.NextDouble()),
                    new Vector2(1.5f, 1.5f),
                    new Vector2(2f, 3f)
                ));
            var eyeRenderer = new AnimatedImageRenderer("./res/EyeFrames/", speed: 3f, pingPong: true);
            eyeNode.AddComponent(eyeRenderer);
            eyeRenderer.PlaybackFinished += () =>
            {
                blinkTimer.Interval = random.Next(1000, 6000);
                blinkTimer.Start();
            };
            blinkTimer.Elapsed += (_, _) => eyeRenderer.PlayOneshot();
            blinkTimer.AutoReset = false;
            blinkTimer.Enabled = true;
            
            eyeNode.AddComponent(new ScaleWiggler(
                new Vector2(2f, 2f),
                new Vector2((float)random.NextDouble(), (float)random.NextDouble()),
                new Vector2(0.05f, 0.05f),
                new Vector2(1f, 1f)
                ));

            virtualCanvas.AddNode(eyeNode);

            var mouthNode = new Node(new Vector2(26, 22f));
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