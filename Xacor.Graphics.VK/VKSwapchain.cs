using System;

namespace Xacor.Graphics.VK
{
    internal class VKSwapChain : ISwapChain
    {
        public TextureView TextureView { get; }

        public TextureView DepthStencilView { get; }

        public void Present()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}