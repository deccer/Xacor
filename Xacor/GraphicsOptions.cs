using System.Drawing;

namespace Xacor
{
    public class GraphicsOptions
    {
        public RenderApi RenderApi { get; }

        public Size WindowResolution { get; }

        public Size RenderResolution { get; }

        public WindowState WindowState { get; }

        public bool VSync { get; }

        public GraphicsOptions(RenderApi renderApi, Size windowResolution, Size renderResolution, WindowState windowState, bool vSync)
        {
            RenderApi = renderApi;
            WindowResolution = windowResolution;
            RenderResolution = renderResolution;
            WindowState = windowState;
            VSync = vSync;
        }
    }
}
