using System;

namespace Xacor.Graphics.VK
{
    public class VKGraphicsFactory : IGraphicsFactory
    {
        public ICommandList CreateCommandList()
        {
            throw new NotImplementedException();
        }

        public IShader CreateShaderFromFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo)
        {
            throw new NotImplementedException();
        }
    }
}