
namespace ProtoDisplayDriver.Components;

public enum Viseme
{
    None,
    Aa,
}

public class LipSyncChildMultiplexer : ChildMultiplexer
{
    private const int SampleRate = 44100;
    private const int DeviceIndex = 0;
    private readonly Dictionary<Viseme, int> _nodeMap;
    private float[] _recording = new float[1024];
    private int _samplesAvailable;
        
    public LipSyncChildMultiplexer(Dictionary<Viseme, Node> visemes) : base(visemes.Values.ToList())
    {
        /*
        _nodeMap = visemes.Keys.Select((viseme, i) => new { Key = viseme, Value = i }).ToDictionary(pair => pair.Key, pair => pair.Value);

        PortAudio.Initialize();

        DeviceInfo info = PortAudio.GetDeviceInfo(DeviceIndex);

        Console.WriteLine();
        Console.WriteLine($"Using device {DeviceIndex} ({info.name})");
        
        var param = new StreamParameters
        {
            device = DeviceIndex,
            channelCount = 1,
            sampleFormat = SampleFormat.Float32,
            suggestedLatency = info.defaultLowInputLatency,
            hostApiSpecificStreamInfo = IntPtr.Zero,
        };

        StreamCallbackResult Callback(IntPtr input, IntPtr output, uint frameCount, ref StreamCallbackTimeInfo timeInfo, StreamCallbackFlags statusFlags, IntPtr userData)
        {
            _samplesAvailable = (int)frameCount;
            Marshal.Copy(input, _recording, 0, 1024);

            return StreamCallbackResult.Continue;
        }

        var stream = new PortAudioSharp.Stream(inParams: param, outParams: null, sampleRate: SampleRate,
            framesPerBuffer: 0,
            streamFlags: StreamFlags.ClipOff,
            callback: Callback,
            userData: IntPtr.Zero
        );

        Console.WriteLine(param);
        Console.WriteLine("Started! Please speak");

        stream.Start();
        */
    }

    private void ReadAudioInput()
    {
    }

    public override void Update(float delta)
    {
        if (_samplesAvailable > 0)
            Console.WriteLine(_recording[.._samplesAvailable].Max());
    }
}