namespace Xacor.Graphics.DX12
{
    internal class DX12TextureFactory : ITextureFactory
    {
        private readonly DX12GraphicsDevice _graphicsDevice;
        
        public ITexture CreateRenderTarget(int width, int height, Format format)
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

        public DX12TextureFactory(DX12GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }
    }
}