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
            
            var faceHolder = new Node();
            faceHolder.AddComponent(
                new PositionWiggler(
                    new Vector2(2f, 2f),
                    new Vector2(1.5f, 1.5f),
                    new Vector2(31, 15)
                ));
            faceHolder.AddComponent(new ScaleWiggler(
                new Vector2(2f, 2f),
                new Vector2(0.05f, 0.05f),
                new Vector2(1f, 1f)
            ));
            
            var eyeNode = new Node(position: new Vector2(-15, -5), new Vector3(0, 0, 0.1f));
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

            var mouthNode = new Node(new Vector2(12, 12f));
            mouthNode.AddComponent(new ImageRenderer("./res/Mouth.png"));
            
            faceHolder.AddChild(eyeNode);
            faceHolder.AddChild(mouthNode);
            
            virtualCanvas.AddChild(faceHolder);
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