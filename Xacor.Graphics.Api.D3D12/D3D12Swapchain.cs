using System;
using SharpDX.Direct3D12;
using SharpDX.DXGI;
using Xacor.Graphics.Api.D3D;
using DXGIFactory = SharpDX.DXGI.Factory4;

namespace Xacor.Graphics.Api.D3D12
{
    internal class DX12SwapChain : ISwapChain, IDisposable
    {
        private readonly DXGIFactory _factory;
        private readonly SwapChain3 _swapChain;

        

        public TextureView TextureView { get; }

        public TextureView DepthStencilView { get; }

        public DX12SwapChain(DX12GraphicsDevice graphicsDevice, SwapChainDescriptor swapChainDescriptor)
        {
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

            using var tempSwapChain = new SwapChain(_factory, graphicsDevice, swapChainDescription);
            _swapChain = tempSwapChain.QueryInterface<SwapChain3>();

            TextureView = new D3D12TextureView(graphicsDevice, 1, DescriptorHeapFlags.ShaderVisible, DescriptorHeapType.RenderTargetView);
        }

        public void Present()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _factory?.Dispose();
        }
    }
}