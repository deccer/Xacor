using System;

namespace Xacor.Graphics.Api
{
    public interface ISwapChain : IDisposable
    {
        TextureView TextureView { get; }

        TextureView DepthStencilView { get; }

        void Present();
    }
}
