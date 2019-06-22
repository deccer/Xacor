using System;

namespace Xacor.Graphics.Api
{
    public interface ITexture : IDisposable
    {
        TextureView View { get; }
    }
}