namespace Xacor.Graphics.DX12
{
    public class DX12GraphicsFactory : IGraphicsFactory
    {
        public ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo)
        {
            return new DX12SwapChain();
        }

        public IShader CreateShaderFromFile(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
}