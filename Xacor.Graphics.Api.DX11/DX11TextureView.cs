using System;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using D3D11Resource = SharpDX.Direct3D11.Resource;

namespace Xacor.Graphics.Api.DX11
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

        public DX11TextureView(DX11GraphicsDevice graphicsDevice, D3D11Resource resource, int width, int height, int depth, bool isCube, int mipLevels, TextureViewType type)
        {
            if (type.HasFlag(TextureViewType.RenderTarget))
            {
                _nativeRenderTargetView = new RenderTargetView(graphicsDevice, resource);
            }
            if (type.HasFlag(TextureViewType.ShaderResource))
            {
                var srvDescription = new ShaderResourceViewDescription();
                if (depth > 0 && height > 0 && width > 0)
                {
                    srvDescription.Format = ((Texture3D) resource).Description.Format;
                    srvDescription.Texture1D.MipLevels = mipLevels;
                    srvDescription.Dimension = ShaderResourceViewDimension.Texture3D;
                }
                else if (depth == 0 && height > 0 && width > 0)
                {
                    srvDescription.Format = ((Texture2D)resource).Description.Format;
                    srvDescription.Texture2D.MipLevels = mipLevels;
                    srvDescription.Dimension = ShaderResourceViewDimension.Texture2D;
                    if (isCube)
                    {
                        srvDescription.Dimension = ShaderResourceViewDimension.TextureCube;
                    }
                }
                else if (depth == 0 && height == 0 && width > 0)
                {
                    srvDescription.Format = ((Texture1D)resource).Description.Format;
                    srvDescription.Texture3D.MipLevels = mipLevels;
                    srvDescription.Dimension = ShaderResourceViewDimension.Texture1D;
                }
                
                _nativeShaderResourceView = new ShaderResourceView(graphicsDevice, resource, srvDescription);
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