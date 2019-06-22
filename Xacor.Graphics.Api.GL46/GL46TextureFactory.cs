using System;
using System.IO;
using Image = SixLabors.ImageSharp.Image;

namespace Xacor.Graphics.Api.GL46
{
    internal class GL46TextureFactory : ITextureFactory
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ITexture CreateRenderTarget(int width, int height, Format format)
        {
            throw new NotImplementedException();
        }

        public ITexture CreateTextureFromFile(string filePath, bool createMipMaps)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                var image = Image.Load(fileStream);
                var texture = new GL46Texture(image.Width, image.Height, Format.R8G8B8A8UNorm, Filter.Nearest, TextureAddressMode.Clamp, image);
                return texture;
            }
        }
    }
}
