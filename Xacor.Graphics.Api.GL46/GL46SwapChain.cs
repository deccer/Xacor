using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform;

namespace Xacor.Graphics.Api.GL46
{
    internal class GL46SwapChain : ISwapChain
    {
        private readonly GraphicsContext _nativeContext;

        public TextureView TextureView { get; }

        public TextureView DepthStencilView { get; }

        public void Dispose()
        {
            _nativeContext?.Dispose();
        }

        public GL46SwapChain(SwapChainInfo swapChainInfo)
        {
            var windowInfo = Utilities.CreateWindowsWindowInfo(swapChainInfo.WindowHandle);
            var graphicsContextFlags = GraphicsContextFlags.Default;
#if DEBUG
            graphicsContextFlags |= GraphicsContextFlags.Debug;
#endif
            _nativeContext = new GraphicsContext(GraphicsMode.Default, windowInfo, null, 4, 6, graphicsContextFlags);
            _nativeContext.LoadAll();
            _nativeContext.MakeCurrent(windowInfo);
            _nativeContext.SwapInterval = 1;

            OpenTK.Graphics.OpenGL4.GL.Enable(EnableCap.CullFace);
            OpenTK.Graphics.OpenGL4.GL.CullFace(CullFaceMode.Back);
            OpenTK.Graphics.OpenGL4.GL.FrontFace(FrontFaceDirection.Ccw);

            OpenTK.Graphics.OpenGL4.GL.Enable(EnableCap.DepthTest);
            OpenTK.Graphics.OpenGL4.GL.DepthFunc(DepthFunction.Less);
        }

        public void Present()
        {
            _nativeContext.SwapBuffers();
        }
    }
}