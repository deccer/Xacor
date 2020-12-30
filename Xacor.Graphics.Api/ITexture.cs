using System;

namespace Xacor.Graphics.Api
{
    public interface ITexture : IDisposable
    {
        TextureView View { get; }

        void SetData<T>(T[] data) where T : struct;
    }
}