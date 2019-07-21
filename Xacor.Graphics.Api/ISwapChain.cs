using System;

namespace Xacor.Graphics.Api
{
    public interface ISwapChain
    {
        TextureView TextureView { get; }

        TextureView DepthStencilView { get; }

        void Present();
    }
}