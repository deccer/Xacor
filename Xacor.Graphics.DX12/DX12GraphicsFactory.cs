namespace Xacor.Graphics.DX12
{
    public class DX12GraphicsFactory : IGraphicsFactory
    {
        public ICommandList CreateCommandList()
        {
            throw new System.NotImplementedException();
        }

        public IShader CreateShaderFromFile(string filePath)
        {
            throw new System.NotImplementedException();
        }

        public ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo)
        {
            return new DX12SwapChain();
        }
    }
}