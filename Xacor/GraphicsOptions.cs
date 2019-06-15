using System.Drawing;

namespace Xacor
{
    public class GraphicsOptions
    {
        public Size Resolution { get; }

        public WindowState WindowState { get; }

        public GraphicsOptions(Size resolution, WindowState windowState)
        {
            Resolution = resolution;
            WindowState = windowState;
        }
    }
}