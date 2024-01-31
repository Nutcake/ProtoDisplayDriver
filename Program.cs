using ProtoDisplayDriver.Components;
using RPiRgbLEDMatrix;
using SixLabors.ImageSharp;

namespace ProtoDisplayDriver;

class Program
{
    static void Main(string[] _)
    {
        using var matrix = new RGBLedMatrix(32, 2, 1);
        var virtualCanvas = new VirtualCanvas(matrix);
        var eyeNode = new Node(new PointF(0f, 0), "./res/EyeSharp.png");
        eyeNode.AddComponent(new Wiggler(0, 0.005f, 0, 0.2f));
        virtualCanvas.AddNode(eyeNode);
        virtualCanvas.Loop();
    }
}
