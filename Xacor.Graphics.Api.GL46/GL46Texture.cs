using System;
using OpenTK.Graphics.OpenGL4;
using Xacor.Graphics.Api.GL;

namespace Xacor.Graphics.Api.GL46
{
    internal class GL46Texture : ITexture, IDisposable
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _nativeTexture;

        public TextureView View { get; }

        public static implicit operator int(GL46Texture texture)
        {
            return texture._nativeTexture;
        }

        public void Dispose()
        {
            OpenTK.Graphics.OpenGL4.GL.DeleteTexture(_nativeTexture);
        }

        public GL46Texture(int width, int height, Filter filter, TextureAddressMode textureAddressMode, ref float[] pixelData)
        {
            _width = width;
            _height = height;
            OpenTK.Graphics.OpenGL4.GL.CreateTextures(TextureTarget.Texture2D, 1, out _nativeTexture);
            OpenTK.Graphics.OpenGL4.GL.TextureStorage2D(_nativeTexture, 1, SizedInternalFormat.Rgba32f, width, height);

            OpenTK.Graphics.OpenGL4.GL.TextureParameter(_nativeTexture, TextureParameterName.TextureMinFilter, (int)filter.MinFilterToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TextureParameter(_nativeTexture, TextureParameterName.TextureMagFilter, (int)filter.MagFilterToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapR, (int)textureAddressMode.ToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapS, (int)textureAddressMode.ToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapT, (int)textureAddressMode.ToOpenTK());

            OpenTK.Graphics.OpenGL4.GL.TextureSubImage2D(_nativeTexture, 0, 0, 0, width, height, PixelFormat.Rgba, PixelType.Float, pixelData);

            View = new GL46TextureView(this);
        }

        public void SetData<T>(T[] data) where T : struct
        {
            OpenTK.Graphics.OpenGL4.GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba8,
                _width,
                _height,
                0,
                PixelFormat.Rgba,
                PixelType.Byte,
                data);
        }
    }
}