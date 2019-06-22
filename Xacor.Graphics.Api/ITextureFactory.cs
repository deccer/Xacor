using System;

namespace Xacor.Graphics.Api
{
    public interface ITextureFactory : IDisposable
    {
        ITexture CreateRenderTarget(int width, int height, Format format);

        ITexture CreateTextureFromFile(string filePath, bool createMipMaps);
    }
}