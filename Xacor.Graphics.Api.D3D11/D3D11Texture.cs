using SharpDX.Direct3D11;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11Texture : ITexture
    {
        private readonly D3D11GraphicsDevice _graphicsDevice;
        private readonly Resource _nativeResource;

        public TextureView View { get; }

        public static implicit operator Resource(D3D11Texture texture)
        {
            return texture._nativeResource;
        }

        public void Dispose()
        {
            _nativeResource?.Dispose();
        }

        public D3D11Texture(D3D11GraphicsDevice graphicsDevice, Resource nativeResource, int width, int height, int depth, bool isCube, int mipLevels, TextureViewType type)
        {
            _graphicsDevice = graphicsDevice;
            _nativeResource = nativeResource;

            View = new D3D11TextureView(graphicsDevice, _nativeResource, width, height, depth, isCube, mipLevels, type);
        }

        public void SetData<T>(T[] data) where T: struct
        {
            _graphicsDevice.NativeDeviceContext.UpdateSubresource(data, _nativeResource);
        }
    }
}