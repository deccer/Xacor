using System;

namespace Xacor.Graphics
{
    public interface ISwapChain : IDisposable
    {
        void Present();
    }
}