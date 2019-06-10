namespace Xacor.Graphics.GL46
{
    public class GL46GraphicsFactory : IGraphicsFactory
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
            return new GL46SwapChain(swapChainInfo);
        }
    }
}
