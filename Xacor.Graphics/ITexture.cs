using System;

namespace Xacor.Graphics
{
    public interface ITexture : IDisposable
    {
        TextureView View { get; }
    }
}