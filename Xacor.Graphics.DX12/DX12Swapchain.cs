using System;

namespace Xacor.Graphics.DX12
{
    internal class DX12SwapChain : ISwapChain
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