using RPiRgbLEDMatrix;

namespace ProtoDisplayDriver;


class World
{
    private bool _running = true;
    private RGBLedCanvas _canvas;
    private RGBLedMatrix _matrix;
    private HashSet<Node> _nodes = new();
    private int _currentFrame;
    private long _lastElapsed;
    
    public World(RGBLedMatrix matrix)
    {
        _canvas = matrix.CreateOffscreenCanvas();
        _matrix = matrix;
    }

    private void Update(float delta)
    {
        foreach (var node in _nodes)
        {
            node.Update(delta);
        }

        _currentFrame++;
    }

    private void Draw(float delta)
    {
        _canvas.Clear();
        foreach (var node in _nodes)
        {
            node.Draw(_canvas, delta);
        }

        _matrix.SwapOnVsync(_canvas);
    }

    public void Loop()
    {
        while (_running)
        {
            var frameStart = Environment.TickCount64;
            Update(_lastElapsed/1000f);
            Draw(_lastElapsed/1000f);
            _lastElapsed = Environment.TickCount64 - frameStart;
            if (_lastElapsed < 33) Thread.Sleep(33 - (int)_lastElapsed);
        }
    }

    public void AddNode(Node node)
    {
        _nodes.Add(node);
    }
}
