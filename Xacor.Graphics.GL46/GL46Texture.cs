using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace Xacor.Graphics.GL46
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

        public GL46Texture(int width, int height, Format format, Filter filter, TextureAddressMode textureAddressMode, Image<Rgba32> imageData)
        {
            OpenTK.Graphics.OpenGL4.GL.CreateTextures(TextureTarget.Texture2D, 1, out _nativeTexture);
            OpenTK.Graphics.OpenGL4.GL.TextureStorage2D(_nativeTexture, 1, format.ToOpenTK(), width, height);

            OpenTK.Graphics.OpenGL4.GL.TextureParameter(_nativeTexture, TextureParameterName.TextureMinFilter, (int)filter.MinFilterToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TextureParameter(_nativeTexture, TextureParameterName.TextureMagFilter, (int)filter.MagFilterToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapR, (int)textureAddressMode.ToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapS, (int)textureAddressMode.ToOpenTK());
            OpenTK.Graphics.OpenGL4.GL.TextureParameter(_nativeTexture, TextureParameterName.TextureWrapT, (int)textureAddressMode.ToOpenTK());

            OpenTK.Graphics.OpenGL4.GL.TextureSubImage2D(_nativeTexture, 0, 0, 0, width, height, PixelFormat.Rgba, PixelType.Byte, imageData.GetPixelSpan().ToArray());

            View = new GL46TextureView(this);
        }
    }
}