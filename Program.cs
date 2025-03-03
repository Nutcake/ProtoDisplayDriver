using System.Diagnostics;
using ProtoDisplayDriver.Components;
using RPiRgbLEDMatrix;
using System.Numerics;
using Color = RPiRgbLEDMatrix.Color;
using Timer = System.Timers.Timer;

namespace ProtoDisplayDriver
{
    internal static class Program
    {
        private static void Main(string[] _)
        {
            Console.WriteLine("Waiting for debugger to attach");
            while (Debugger.IsAttached)
            {
                Thread.Sleep(100);
            }

            Console.WriteLine("Debugger attached");

            using var matrix = new RGBLedMatrix(new RGBLedMatrixOptions
            {
                Parallel = 2,
                Rows = 32,
                Cols = 64
            });
            Scene(matrix);
        }

        static void Scene(RGBLedMatrix matrix)
        {
            var random = new Random();
            var world = new World(matrix);

            var faceHolder = new Node();
            faceHolder.AddComponent(
                new PositionWiggler(
                    new Vector2(2f, 2f),
                    new Vector2(1f, 1f),
                    Vector2.Zero
                ));
            var staticFace = new Node();
            staticFace.AddComponent(
                new PositionWiggler(
                    new Vector2(0.1f, 0.1f),
                    new Vector2(0.1f, 0.1f), Vector2.Zero
                ));

            var spiralEyeNode = new Node(new Vector2(18, 1), scale: new Vector2(0.8f, 0.8f));
            spiralEyeNode.AddComponent(new ImageRenderer("./res/EyeSpiral.png"));
            spiralEyeNode.AddComponent(new Rotator(new Vector3(0, 0, -0.2f)));
            var happyEyeNode = new Node(new Vector2(14, 2), scale: new Vector2(0.7f, 0.7f), rotation: new Vector3(0, 0, 0.1f));
            happyEyeNode.AddComponent(new ImageRenderer("./res/EyeHappy.png"));
            var normalEyeNode = new Node(position: new Vector2(14, 2), new Vector3(0, 0, 0.1f), scale: new Vector2(0.8f, 1.0f));
            var eyeRenderer = new AnimatedImageRenderer("./res/EyeFrames/", speed: 3f, pingPong: true, color: new Color(255, 80, 0));
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

            var closedMouthNode = new Node();
            closedMouthNode.AddComponent(new ImageRenderer("./res/Mouth.png", color: new Color(255, 80, 0)));

            var openMouthNode = new Node();
            openMouthNode.AddComponent(new ImageRenderer("./res/Box.png", color: new Color(255, 80, 0)));

            var mouthNode = new Node(new Vector2(32f, 21), rotation: new Vector3(0, 0, 0.05f));
            mouthNode.AddComponent(new MouthFftDisplay());

            faceHolder.AddChild(normalEyeNode);
            faceHolder.AddChild(mouthNode);

            var sideIlluminator = new Node(new Vector2(-8, 16), scale: new Vector2(1f, 1f));
            sideIlluminator.AddComponent(new ImageRenderer("./res/Circle32.png", color: new Color(255, 80, 0)));

            var nose = new Node(position: new Vector2(58, -4));
            nose.AddComponent(new ImageRenderer("./res/Circle8.png", color: new Color(255, 80, 0)));

            staticFace.AddChild(nose);
            staticFace.AddChild(sideIlluminator);

            world.AddChild(faceHolder);
            world.AddChild(staticFace);
            //world.AddShader(new ColorWaveShader(30));
            world.Loop();
        }
    }
}