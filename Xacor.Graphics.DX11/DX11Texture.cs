using SharpDX.Direct3D11;

namespace Xacor.Graphics.DX11
{
    internal class DX11Texture : Texture
    {
        internal Resource NativeResource { get; }

        public DX11Texture(DX11GraphicsDevice graphicsDevice)
        {
            View = new DX11TextureView(graphicsDevice, this);
        }
    }
}