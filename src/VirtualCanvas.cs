using RPiRgbLEDMatrix;

namespace ProtoDisplayDriver;


class VirtualCanvas
{
    private bool _running = true;
    private RGBLedCanvas _canvas;
    private RGBLedMatrix _matrix;
    private HashSet<Node> _nodes = new();
    private int _currentFrame;
    
    public VirtualCanvas(RGBLedMatrix matrix)
    {
        _canvas = matrix.CreateOffscreenCanvas();
        _matrix = matrix;
    }

    private void Update()
    {
        foreach (var node in _nodes)
        {
            node.Update(_currentFrame);
        }

        _currentFrame++;
    }

    private void Draw()
    {
        _canvas.Clear();
        foreach (var node in _nodes)
        {
            node.Draw(_canvas);
        }

        _matrix.SwapOnVsync(_canvas);
    }

    public void Loop()
    {
        while (_running)
        {
            var frameStart = Environment.TickCount64;
            Update();
            Draw();
            var elapsed = Environment.TickCount64 - frameStart;
            if (elapsed < 33) Thread.Sleep(33 - (int)elapsed);
        }
    }

    public void AddNode(Node node)
    {
        _nodes.Add(node);
    }
}
