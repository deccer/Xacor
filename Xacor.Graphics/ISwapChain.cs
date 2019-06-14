using System;

namespace Xacor.Graphics
{
    public interface ISwapChain : IDisposable
    {
        TextureView TextureView { get; }

        TextureView DepthStencilView { get; }

        void Present();
    }
}