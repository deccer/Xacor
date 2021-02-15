using System;
using Xacor.Graphics.Api;

namespace Xacor.Graphics
{
    public class GBuffer : IDisposable
    {
        private readonly ITexture _buffer1;
        private readonly ITexture _buffer2;
        private readonly ITexture _buffer3;
        private readonly ITexture _depthBuffer;

        public GBuffer(ITextureFactory textureFactory, int width, int height)
        {
            _buffer1 = textureFactory.CreateRenderTarget(width, height, Format.R16G16B16A16Float);
            _buffer2 = textureFactory.CreateRenderTarget(width, height, Format.R32G32B32A32Float);
            _buffer3 = textureFactory.CreateRenderTarget(width, height, Format.R16G16Float);
            _depthBuffer = textureFactory.CreateRenderTarget(width, height, Format.D24UnormS8UInt);
        }

        public ITexture DepthBuffer => _depthBuffer;

        public void Dispose()
        {
            _buffer1.Dispose();
            _buffer2.Dispose();
            _buffer3.Dispose();
            _depthBuffer.Dispose();
        }

        public static implicit operator TextureView[](GBuffer gBuffer)
        {
            return new[]
            {
                gBuffer._buffer1.View,
                gBuffer._buffer2.View,
                gBuffer._buffer3.View,
            };
        }
    }
}
