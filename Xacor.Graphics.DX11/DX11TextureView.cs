using System;
using SharpDX.Direct3D11;

namespace Xacor.Graphics.DX11
{
    internal class DX11TextureView : TextureView, IDisposable
    {
        internal RenderTargetView NativeView { get; }

        public void Dispose()
        {
            NativeView?.Dispose();
        }

        public DX11TextureView(DX11GraphicsDevice graphicsDevice, DX11Texture texture)
        {
            NativeView = new RenderTargetView(graphicsDevice.NativeDevice, texture.NativeResource);
        }

        public static implicit operator RenderTargetView(DX11TextureView textureView)
        {
            return textureView.NativeView;
        }
    }
}