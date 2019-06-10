namespace Xacor.Graphics.GL33
{
    public class GL33GraphicsFactory : IGraphicsFactory
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
            return new GL33SwapChain(swapChainInfo);
        }
    }
}
