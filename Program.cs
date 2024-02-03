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
            var world = new World(matrix);

            var faceHolder = new Node();
            faceHolder.AddComponent(
                new PositionWiggler(
                    new Vector2(2f, 2f),
                    new Vector2(1f, 1f),
                    new Vector2(0, 0)
                ));

            var spiralEyeNode = new Node(new Vector2(10, 1), scale: new Vector2(0.8f, 0.8f));
            spiralEyeNode.AddComponent(new ImageRenderer("./res/EyeSpiral.png"));
            spiralEyeNode.AddComponent(new Rotator(new Vector3(0, 0, -0.2f)));

            var happyEyeNode = new Node(new Vector2(5, 2), scale: new Vector2(0.7f, 0.7f), rotation: new Vector3(0, 0, 0.1f));
            happyEyeNode.AddComponent(new ImageRenderer("./res/EyeHappy.png"));

            var normalEyeNode = new Node(position: new Vector2(5, 2), new Vector3(0, 0, 0.1f), scale: new Vector2(0.8f, 1.0f));
            var eyeRenderer = new AnimatedImageRenderer("./res/EyeFrames/", speed: 3f, pingPong: true);
            normalEyeNode.AddComponent(eyeRenderer);
            var blinkTimer = new Timer(2000);
            eyeRenderer.PlaybackFinished += () =>
            {
                blinkTimer.Interval = random.Next(1000, 6000);
                blinkTimer.Start();
            };
            blinkTimer.Elapsed += (_, _) => { world.ScheduleExecuteNextUpdate(eyeRenderer.PlayOneshot); };
            blinkTimer.AutoReset = false;
            blinkTimer.Enabled = true;

            var multiplexer = new ChildMultiplexer(new List<Node>
            {
                normalEyeNode,
                happyEyeNode,
                spiralEyeNode
            });
            var eyeHolder = new Node();
            eyeHolder.AddComponent(multiplexer);

            var mouthNode = new Node(new Vector2(28f, 22f), rotation: new Vector3(0, 0, 0.05f));
            mouthNode.AddComponent(new ImageRenderer("./res/Mouth.png"));

            var eyeTimer = new Timer(5000);
            eyeTimer.Elapsed += (_, _) => world.ScheduleExecuteNextUpdate(() => multiplexer.Index = (multiplexer.Index + 1) % multiplexer.NodeCount);
            eyeTimer.AutoReset = true;
            eyeTimer.Enabled = true;


            faceHolder.AddChild(eyeHolder);
            faceHolder.AddChild(mouthNode);

            world.AddChild(faceHolder);
            world.Loop();
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