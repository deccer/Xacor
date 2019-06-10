using SharpDX.DXGI;
using Xacor.Graphics.DX;
using DXGIFactory = SharpDX.DXGI.Factory1;

namespace Xacor.Graphics.DX11
{
    internal class DX11SwapChain : ISwapChain
    {
        private readonly DXGIFactory _factory;
        private readonly SwapChain _swapChain;

        public DX11SwapChain(DX11GraphicsDevice graphiceDevice, SwapChainInfo swapChainInfo)
        {
            _factory = new DXGIFactory();

            var device = graphiceDevice.NativeDevice;
            var swapChainDescription = new SwapChainDescription
            {
                SwapEffect = swapChainInfo.SwapEffect.ToSharpDX(),
                BufferCount = 2,
                Flags = SwapChainFlags.None,
                IsWindowed = swapChainInfo.IsWindowed,
                ModeDescription = new ModeDescription(swapChainInfo.Width, swapChainInfo.Height, new Rational(60, 1), Format.R8G8B8A8UNorm.ToSharpDX()),
                OutputHandle = swapChainInfo.WindowHandle,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput
            };

            _swapChain = new SwapChain(_factory, device, swapChainDescription);
        }

        public void Present()
        {
            _swapChain.Present(0, PresentFlags.None);
        }

        public void Dispose()
        {
            _swapChain?.Dispose();
            _factory?.Dispose();
        }
    }
}