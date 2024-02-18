using ExtensionMethods;
using RPiRgbLEDMatrix;

namespace ProtoDisplayDriver;

class World
{
    private static readonly Color Color = new(255, 100, 0);
    private bool _running = true;
    private readonly RGBLedCanvas _canvas;
    private readonly RGBLedMatrix _matrix;
    private long _lastElapsed;
    private readonly Node _rootNode = new();
    private Action? _updateRun;

    public World(RGBLedMatrix matrix)
    {
        _canvas = matrix.CreateOffscreenCanvas();
        Console.WriteLine($"Width: {_canvas.Width}, Height: {_canvas.Height}");
        _matrix = matrix;
    }

    public void ScheduleExecuteNextUpdate(Action runnable)
    {
        Action? wrappedRunnable = null;
        wrappedRunnable = () =>
        {
            runnable.Invoke();
            _updateRun -= wrappedRunnable;
        };
        _updateRun += wrappedRunnable;
    }

    public void Dispose()
    {
        _running = false;
    }

    private void Update(float delta)
    {
        _updateRun?.Invoke();
        _rootNode.Update(delta);
    }

    private void Draw(float delta)
    {
        _canvas.Clear();
        var values = new float[_canvas.Width, _canvas.Height / 2];

        _rootNode.Draw(values, _canvas.Width, _canvas.Height / 2, delta);

        var colors = new Color[values.Length];
        var index = 0;
        for (var y = 0; y < _canvas.Height / 2; y++)
        {
            for (var x = 0; x < _canvas.Width; x++)
            {
                colors[index] = Color.Multiply(values[x, y]);
                index++;
            }
        }

        _canvas.SetPixels(0, _canvas.Height / 2, _canvas.Width, _canvas.Height / 2, colors);
        for (var y = 0; y < _canvas.Height / 2; y++)
        {
            for (var x = 0; x < _canvas.Width; x++)
            {
                _canvas.SetPixel(_canvas.Width-1-x, y, Color.Multiply(values[x, y]));
            }
        }

        _matrix.SwapOnVsync(_canvas);
    }

    public void Loop()
    {
        while (_running)
        {
            var frameStart = Environment.TickCount64;
            Update(_lastElapsed / 1000f);
            Draw(_lastElapsed / 1000f);
            _lastElapsed = Environment.TickCount64 - frameStart;
            if (_lastElapsed < 33) Thread.Sleep(33 - (int)_lastElapsed);
        }
    }

    public void AddChild(Node node)
    {
        _rootNode.AddChild(node);
    }
}