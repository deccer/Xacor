namespace Xacor.Graphics.GL33
{
    public class GL33GraphicsFactory : IGraphicsFactory
    {
        public ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo)
        {
            return new GL33SwapChain(swapChainInfo);
        }

        public IShader CreateShaderFromFile(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
}
