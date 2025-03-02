using Un4seen.Bass;

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
    private Stream _stream;

    public LipSyncChildMultiplexer(Dictionary<Viseme, Node> visemes) : base(visemes.Values.ToList())
    {
        _nodeMap = visemes.Keys.Select((viseme, i) => new { Key = viseme, Value = i }).ToDictionary(pair => pair.Key, pair => pair.Value);

        if (!Bass.BASS_Init(-1, 441000, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
        {
            //throw new Exception($"Failed to init Bass.NET, ERR {Bass.BASS_ErrorGetCode()}");
        }

        if (!Bass.BASS_RecordInit(-1))
        {
            //throw new Exception($"Failed to init Bass.NET Recording, ERR {Bass.BASS_ErrorGetCode()}");
        }
    }

    public override void Update(float delta)
    {

    }
}