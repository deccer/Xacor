using OpenTK.Graphics;
using OpenTK.Platform;

namespace Xacor.Graphics.GL33
{
    internal class GL33SwapChain : ISwapChain
    {
        public GraphicsContext NativeContext { get; }

        public void Dispose()
        {
            NativeContext?.Dispose();
        }

        public GL33SwapChain(SwapChainInfo swapChainInfo)
        {
            var windowInfo = Utilities.CreateWindowsWindowInfo(swapChainInfo.WindowHandle);
            NativeContext = new GraphicsContext(GraphicsMode.Default, windowInfo, null, 3, 3, GraphicsContextFlags.Debug);
        }

        public void Present()
        {
            NativeContext.SwapBuffers();
        }
    }
}