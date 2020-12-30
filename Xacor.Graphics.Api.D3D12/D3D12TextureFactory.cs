namespace Xacor.Graphics.Api.D3D12
{
    internal class D3D12TextureFactory : ITextureFactory
    {
        private readonly DX12GraphicsDevice _graphicsDevice;
        
        public ITexture CreateRenderTarget(int width, int height, Format format)
        {
            throw new System.NotImplementedException();
        }

        public ITexture CreateTexture(int width, int height, Format format, bool createMipMaps = true)
        {
            throw new System.NotImplementedException();
        }

        public ITexture CreateTextureFromFile(string filePath, bool createMipMaps)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public D3D12TextureFactory(DX12GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }
    }
}