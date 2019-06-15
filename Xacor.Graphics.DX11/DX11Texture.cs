using SharpDX.Direct3D11;

namespace Xacor.Graphics.DX11
{
    internal class DX11Texture : ITexture
    {
        private readonly Resource _nativeResource;

        public TextureView View { get; }

        public static implicit operator Resource(DX11Texture texture)
        {
            return texture._nativeResource;
        }

        public void Dispose()
        {
            _nativeResource?.Dispose();
        }

        public DX11Texture(DX11GraphicsDevice graphicsDevice, Resource nativeResource, int width, int height, int depth, bool isCube, int mipLevels, TextureViewType type)
        {
            _nativeResource = nativeResource;

            View = new DX11TextureView(graphicsDevice, _nativeResource, width, height, depth, isCube, mipLevels, type);
        }
    }
}