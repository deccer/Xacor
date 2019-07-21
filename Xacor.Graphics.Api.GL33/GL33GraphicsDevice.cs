using System;
using OpenTK.Graphics;

namespace Xacor.Graphics.Api.GL33
{
    internal class GL33GraphicsDevice : IGraphicsDevice, IDisposable
    {
        private readonly GraphicsContext _nativeContext;

        public void Dispose()
        {
            _nativeContext?.Dispose();
        }

        public GL33GraphicsDevice()
        {
            var graphicsContextFlags = GraphicsContextFlags.Default;
#if DEBUG
            graphicsContextFlags |= GraphicsContextFlags.Debug;
#endif
            _nativeContext = new GraphicsContext(GraphicsMode.Default, null, null, 3, 3, graphicsContextFlags);
        }
    }
}