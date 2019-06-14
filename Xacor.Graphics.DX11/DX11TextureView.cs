using System;
using SharpDX;
using SharpDX.Direct3D11;
using D3D11Resource = SharpDX.Direct3D11.Resource;

namespace Xacor.Graphics.DX11
{
    internal class DX11TextureView : TextureView, IDisposable
    {
        private readonly RenderTargetView _nativeRenderTargetView;
        private readonly ShaderResourceView _nativeShaderResourceView;
        private readonly DepthStencilView _nativeDepthStencilView;

        public void Dispose()
        {
            _nativeRenderTargetView?.Dispose();
            _nativeShaderResourceView?.Dispose();
            _nativeDepthStencilView?.Dispose();
        }

        public DX11TextureView(DX11GraphicsDevice graphicsDevice, D3D11Resource resource, TextureViewType type)
        {
            if (type.HasFlag(TextureViewType.RenderTarget))
            {
                _nativeRenderTargetView = new RenderTargetView(graphicsDevice, resource);
            }
            if (type.HasFlag(TextureViewType.ShaderResource))
            {
                _nativeShaderResourceView = new ShaderResourceView(graphicsDevice, resource);
            }
            if (type.HasFlag(TextureViewType.DepthStencil))
            {
                _nativeDepthStencilView = new DepthStencilView(graphicsDevice, resource);
            }
        }

        public static implicit operator RenderTargetView(DX11TextureView textureView)
        {
            return textureView._nativeRenderTargetView;
        }

        public static implicit operator ShaderResourceView(DX11TextureView textureView)
        {
            return textureView._nativeShaderResourceView;
        }

        public static implicit operator DepthStencilView(DX11TextureView textureView)
        {
            return textureView._nativeDepthStencilView;
        }
    }
}