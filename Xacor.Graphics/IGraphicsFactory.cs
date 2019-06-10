namespace Xacor.Graphics
{
    public interface IGraphicsFactory
    {
        ICommandList CreateCommandList();

        IShader CreateShaderFromFile(string filePath);

        ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo);
    }
}