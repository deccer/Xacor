using System;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Xacor.Graphics.Api.D3D;
using DXGIFactory = SharpDX.DXGI.Factory1;
using D3D11Texture2D = SharpDX.Direct3D11.Texture2D;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11SwapChain : ISwapChain, IDisposable
    {
        private readonly SwapChainDescriptor _swapChainDescriptor;
        private readonly DXGIFactory _factory;
        private readonly SwapChain _swapChain;

        public TextureView TextureView { get; }

        public TextureView DepthStencilView { get; }

        public D3D11SwapChain(D3D11GraphicsDevice graphiceDevice, SwapChainDescriptor swapChainDescriptor)
        {
            _swapChainDescriptor = swapChainDescriptor;
            _factory = new DXGIFactory();

            var swapChainDescription = new SwapChainDescription
            {
                SwapEffect = swapChainDescriptor.SwapEffect.ToSharpDX(),
                BufferCount = 2,
                Flags = SwapChainFlags.None,
                IsWindowed = swapChainDescriptor.IsWindowed,
                ModeDescription = new ModeDescription(swapChainDescriptor.Width, swapChainDescriptor.Height, new Rational(60, 1), Format.R8G8B8A8UNorm.ToSharpDX()),
                OutputHandle = swapChainDescriptor.WindowHandle,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput
            };

            _swapChain = new SwapChain(_factory, graphiceDevice, swapChainDescription);
            using var resource = _swapChain.GetBackBuffer<D3D11Texture2D>(0);

            TextureView = new D3D11TextureView(graphiceDevice, resource, swapChainDescriptor.Width, swapChainDescriptor.Height, 0, false, 1, TextureViewType.RenderTarget);
            DepthStencilView = CreateDepthStencilView(graphiceDevice, swapChainDescriptor.Width, swapChainDescriptor.Height);
        }

        private static TextureView CreateDepthStencilView(D3D11GraphicsDevice graphicsDevice, int width, int height)
        {
            var depthBufferDescription = new Texture2DDescription
            {
                Format = SharpDX.DXGI.Format.D24_UNorm_S8_UInt,
                ArraySize = 1,
                MipLevels = 1,
                Width = width,
                Height = height,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };

            using var depthBuffer = new D3D11Texture2D(graphicsDevice, depthBufferDescription);

            return new D3D11TextureView(graphicsDevice, depthBuffer, width, height, 0, false, 1, TextureViewType.DepthStencil);
        }

        public void Present()
        {
            _swapChain.Present(_swapChainDescriptor.VSync ? 1 : 0, PresentFlags.None);
        }

        public void Dispose()
        {
            _swapChain?.Dispose();
            _factory?.Dispose();
        }
    }
}