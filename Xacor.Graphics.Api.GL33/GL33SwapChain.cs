using System;
using OpenTK.Graphics;
using OpenTK.Platform;

namespace Xacor.Graphics.Api.GL33
{
    internal class GL33SwapChain : ISwapChain, IDisposable
    {
        private readonly GraphicsContext _nativeContext;

        public TextureView TextureView { get; }

        public TextureView DepthStencilView { get; }

        public void Dispose()
        {
            _nativeContext?.Dispose();
        }

        public GL33SwapChain(SwapChainInfo swapChainInfo)
        {
            var windowInfo = Utilities.CreateWindowsWindowInfo(swapChainInfo.WindowHandle);
            _nativeContext = new GraphicsContext(GraphicsMode.Default, windowInfo, null, 3, 3, GraphicsContextFlags.Debug);
        }

        public void Present()
        {
            _nativeContext.SwapBuffers();
        }
    }
}