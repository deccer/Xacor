using System;
using OpenTK.Graphics.OpenGL4;
using Xacor.Graphics.Api.GL;

namespace Xacor.Graphics.Api.GL33
{
    internal class GL33Texture : ITexture, IDisposable
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _nativeTexture;

        public TextureView View { get; }

        public static implicit operator int(GL33Texture texture)
        {
            return texture._nativeTexture;
        }

        public void Dispose()
        {
            OpenTK.Graphics.OpenGL4.GL.DeleteTexture(_nativeTexture);
        }

        public GL33Texture(int width, int height, Filter filter, TextureAddressMode textureAddressMode, ref float[] pixelData)
        {
            _width = width;
            _height = height;
            _nativeTexture = OpenTK.Graphics.OpenGL4.GL.GenTexture();
            OpenTK.Graphics.OpenGL4.GL.BindTexture(TextureTarget.Texture2D, _nativeTexture);
            OpenTK.Graphics.OpenGL4.GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)filter.MinFilterToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)filter.MagFilterToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapR, (int)textureAddressMode.ToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)textureAddressMode.ToOpenTK());

            OpenTK.Graphics.OpenGL4.GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, _width, _height, 0, PixelFormat.Rgba, PixelType.Float, pixelData);

            View = new GL33TextureView(this);
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