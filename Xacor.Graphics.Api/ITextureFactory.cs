using System;

namespace Xacor.Graphics.Api
{
    public interface ITextureFactory : IDisposable
    {
        ITexture CreateRenderTarget(int width, int height, Format format);

        ITexture CreateTexture(int width, int height, Format format, bool createMipMaps = true);

        ITexture CreateTextureFromFile(string filePath, bool createMipMaps);
    }
}
