using System;
using OpenTK.Graphics.OpenGL4;

namespace Xacor.Graphics.Api.GL46
{
    internal class GL46Texture : ITexture
    {
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
    }
}