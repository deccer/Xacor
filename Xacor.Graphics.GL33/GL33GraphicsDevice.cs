using OpenTK.Graphics;

namespace Xacor.Graphics.GL33
{
    internal class GL33GraphicsDevice : IGraphicsDevice
    {
        public GraphicsContext NativeContext { get; }

        public void Dispose()
        {
            NativeContext?.Dispose();
        }

        public GL33GraphicsDevice()
        {
            NativeContext = new GraphicsContext(GraphicsMode.Default, null);
        }
    }
}