namespace Xacor.Graphics.GL46
{
    public class GL46GraphicsFactory : IGraphicsFactory
    {
        public ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo)
        {
            return new GL46SwapChain(swapChainInfo);
        }

        public IShader CreateShaderFromFile(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
}
