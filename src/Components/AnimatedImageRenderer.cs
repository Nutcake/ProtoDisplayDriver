using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ProtoDisplayDriver.Components;

public class AnimatedImageRenderer : ImageRenderer
{
    private float _speed;
    private bool _pingPong;
    private bool isPong;
    private float _frameIdx;
    private List<Image<Rgba32>> _frames;
    private bool _playing;
    private bool _stopAfterPlay;

    public Action? PlaybackFinished;
    
    public AnimatedImageRenderer(List<Image<Rgba32>> frames, float speed = 1, bool pingPong = false) : base(frames.First())
    {
        _speed = speed;
        _frames = frames;
        _pingPong = pingPong;
    }

    public AnimatedImageRenderer(IEnumerable<string> framePaths, float speed = 1, bool pingPong = false) : this(framePaths.Select(Image.Load<Rgba32>).ToList(), speed, pingPong)
    {
    }

    public AnimatedImageRenderer(string framesDir, float speed = 1, bool pingPong = false) : this(Directory.GetFiles(framesDir).OrderBy(p => p), speed, pingPong)
    {
    }

    public void PlayOneshot()
    {
        _frameIdx = 0;
        _stopAfterPlay = true;
        _playing = true;
    }

    public void PlayForever()
    {
        _frameIdx = 0;
        _stopAfterPlay = false;
        _playing = true;
    }

    public void Pause()
    {
        _playing = true;
    }

    public void Stop()
    {
        _playing = true;
        _frameIdx = 0;
        PlaybackFinished?.Invoke();
    }

    public override void Draw(Node node, float[,] canvas, int width, int height, float delta)
    {
        _image = _frames[(int)float.Floor(_frameIdx)];
        base.Draw(node, canvas, width, height, delta);
        if (!_playing) return;
        _frameIdx = isPong && _pingPong ? _frameIdx - _speed : _frameIdx + _speed;
        if (_frameIdx >= _frames.Count)
        {
            _frameIdx = _frames.Count - 1;
            isPong = true;
            if (!_pingPong)
            {
                _frameIdx = 0;
                if (_stopAfterPlay)
                {
                    _playing = false;
                    PlaybackFinished?.Invoke();
                }
            }
        }

        if (_frameIdx < 0)
        {
            _frameIdx = 0;
            isPong = false;
            if (_stopAfterPlay)
            {
                _playing = false;
                PlaybackFinished?.Invoke();
            }
        }
    }
}