using PortAudioSharp;
namespace ProtoDisplayDriver.Components;

public enum Viseme
{
    None,
    Aa,
}

public class LipSyncChildMultiplexer : ChildMultiplexer
{
    private readonly Dictionary<Viseme, int> _nodeMap;
    private short[] _recording = new short[2000];
    private int samplesAvailable;

    public LipSyncChildMultiplexer(Dictionary<Viseme, Node> visemes) : base(visemes.Values.ToList())
    {
        _nodeMap = visemes.Keys.Select((viseme, i) => new { Key = viseme, Value = i }).ToDictionary(pair => pair.Key, pair => pair.Value);

        PortAudio.Initialize();
        Console.WriteLine($"Number of devices: {PortAudio.DeviceCount}");
        for (int i = 0; i != PortAudio.DeviceCount; ++i)
        {
            Console.WriteLine($" Device {i}");
            DeviceInfo deviceInfo = PortAudio.GetDeviceInfo(i);
            Console.WriteLine($"   Name: {deviceInfo.name}");
            Console.WriteLine($"   Max input channels: {deviceInfo.maxInputChannels}");
            Console.WriteLine($"   Default sample rate: {deviceInfo.defaultSampleRate}");
        }
        int deviceIndex = PortAudio.DefaultInputDevice;
        if (deviceIndex == PortAudio.NoDevice)
        {
            Console.WriteLine("No default input device found");
            Environment.Exit(1);
        }

        DeviceInfo info = PortAudio.GetDeviceInfo(deviceIndex);

        Console.WriteLine();
        Console.WriteLine($"Use default device {deviceIndex} ({info.name})");

        
       var audioInputThread = new Thread(ReadAudioInput);
       audioInputThread.Start();
    }

    private void ReadAudioInput()
    {
        
    }

    public override void Update(float delta)
    {
        if (samplesAvailable > 0)
            Console.WriteLine(_recording[..samplesAvailable].Select(x => Math.Abs(x - 128)).Max() * 80 / 128);
    }
}