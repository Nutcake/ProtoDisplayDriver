using ExtensionMethods;
using RPiRgbLEDMatrix;

namespace ProtoDisplayDriver;


class World
{
    private static Color _color = new(255, 100, 0);
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
        var values = new float[_canvas.Width, _canvas.Height];

        foreach (var node in _nodes)
        {
            node.Draw(values, _canvas.Width, _canvas.Height, delta);
        }
        
        var colors = new Color[values.Length];
        var index = 0;
        for (var y = 0; y < _canvas.Height; y++)
        {
            for (var x = 0; x < _canvas.Width; x++)
            {
                colors[index] = _color.Multiply(values[x, y]);
                index++;
            }
        }

        _canvas.SetPixels(0, 0, _canvas.Width, _canvas.Height, colors);

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
