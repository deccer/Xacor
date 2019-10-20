using System;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using D3D11Resource = SharpDX.Direct3D11.Resource;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11TextureView : TextureView, IDisposable
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

        public D3D11TextureView(D3D11GraphicsDevice graphicsDevice, D3D11Resource resource, int width, int height, int depth, bool isCube, int mipLevels, TextureViewType type)
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

        public static implicit operator RenderTargetView(D3D11TextureView textureView)
        {
            return textureView._nativeRenderTargetView;
        }

        public static implicit operator ShaderResourceView(D3D11TextureView textureView)
        {
            return textureView._nativeShaderResourceView;
        }

        public static implicit operator DepthStencilView(D3D11TextureView textureView)
        {
            return textureView._nativeDepthStencilView;
        }
    }
}