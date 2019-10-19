using System.Drawing;

namespace Xacor
{
    public class GraphicsOptions
    {
        public Size Resolution { get; }

        public WindowState WindowState { get; }

        public bool VSync { get; }

        public GraphicsOptions(Size resolution, WindowState windowState, bool vSync)
        {
            Resolution = resolution;
            WindowState = windowState;
            VSync = vSync;
        }
    }
}