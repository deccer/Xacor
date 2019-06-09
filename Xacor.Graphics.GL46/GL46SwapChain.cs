using OpenTK.Graphics;
using OpenTK.Platform;

namespace Xacor.Graphics.GL46
{
    internal class GL46SwapChain : ISwapChain
    {
        public GraphicsContext NativeContext { get; }

        public void Dispose()
        {
            NativeContext?.Dispose();
        }

        public GL46SwapChain(SwapChainInfo swapChainInfo)
        {
            var windowInfo = Utilities.CreateWindowsWindowInfo(swapChainInfo.WindowHandle);
            NativeContext = new GraphicsContext(GraphicsMode.Default, windowInfo, null, 4, 6, GraphicsContextFlags.Debug);
        }

        public void Present()
        {
            NativeContext.SwapBuffers();
        }
    }
}