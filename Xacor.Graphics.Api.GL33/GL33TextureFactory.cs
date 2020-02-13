using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Xacor.Graphics.Api.GL33
{
    internal class GL33TextureFactory : ITextureFactory
    {
        public void Dispose()
        {

        }

        public ITexture CreateRenderTarget(int width, int height, Format format)
        {
            throw new NotImplementedException();
        }

        public ITexture CreateTextureFromFile(string filePath, bool createMipMaps)
        {
            using var fileStream = File.OpenRead(filePath);
            using var image = new Bitmap(fileStream);

            var imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var pixelData = new float[image.Width * image.Height * 4];
            var i = 0;

            unsafe
            {
                var ptr = (byte*)imageData.Scan0;
                var remain = imageData.Stride - imageData.Width * 4;
                for (var y = 0; y < image.Height; y++)
                {
                    for (var x = 0; x < image.Width; x++)
                    {
                        pixelData[i++] = ptr[2] / 255.0f;
                        pixelData[i++] = ptr[1] / 255.0f;
                        pixelData[i++] = ptr[0] / 255.0f;
                        pixelData[i++] = ptr[3] / 255.0f;
                        ptr += 4;
                    }
                    ptr += remain;
                }
            }

            var texture = new GL33Texture(image.Width, image.Height, Filter.Nearest, TextureAddressMode.Clamp, ref pixelData);

            image.UnlockBits(imageData);
            return texture;
        }
    }
}