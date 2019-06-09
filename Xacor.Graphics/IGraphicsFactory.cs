namespace Xacor.Graphics
{
    public interface IGraphicsFactory
    {
        ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo);

        IShader CreateShaderFromFile(string filePath);
    }
}