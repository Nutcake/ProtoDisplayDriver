using OpenTK.Audio.OpenAL;
using RPiRgbLEDMatrix;

namespace ProtoDisplayDriver.Components;

public enum Viseme
{
    None,
    Aa
}

public class LipSyncChildMultiplexer : ChildMultiplexer
{
    private const int SampleRate = 44100;
    private readonly Dictionary<Viseme, int> _nodeMap;
    private readonly float[] _recording = new float[1024];
    private readonly ALCaptureDevice _device;
    private double[] _power = new double[513];

    public LipSyncChildMultiplexer(Dictionary<Viseme, Node> visemes) : base(visemes.Values.ToList())
    {
        _nodeMap = visemes.Keys.Select((viseme, i) => new { Key = viseme, Value = i }).ToDictionary(pair => pair.Key, pair => pair.Value);
        _device = ALC.CaptureOpenDevice("ALSA Default", SampleRate, ALFormat.MonoFloat32Ext, 1024);
        if (_device == ALDevice.Null)
        {
            Console.WriteLine($"Failed to open audio device: {AL.GetError()}");
            return;
        }

        ALC.CaptureStart(_device);
    }

    public override void Update(float delta)
    {
        var nSamples = ALC.GetInteger(_device, AlcGetInteger.CaptureSamples);
        if (nSamples > 0)
        {
            ALC.CaptureSamples(_device, _recording, 1024);
            var spectrum = FftSharp.FFT.Forward(Array.ConvertAll(_recording, x => (double)x));
            _power = FftSharp.FFT.Power(spectrum);
            var bins = new double[64];
            for (var i = 0; i < 64; i++)
            {
                bins[i] = (_power.Skip(i * 8).Take(8).Average() + 100) / 100;
            }
            
        }
    }
    /*
    public override void Draw(Color[,] canvas, int width, int height, float delta)
    {
        var bins = new double[64];
        for (var i = 0; i < 64; i++)
        {
            bins[i] = _power.Skip(i * 8).Take(8).Average();
        }

        for (var binIdx = 0; binIdx < 64; binIdx++)
        {
            var bin = bins[binIdx];
            for (var y = 0; y < Math.Min((bin + 100) / 100 * height, height); y++)
            {
                canvas[binIdx, height - y - 1] = new Color(255, 255, 255);
            }
        }
    }
    */
}