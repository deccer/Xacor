using System;

namespace Xacor.Graphics.Api.GL33
{
    internal class GL33Texture : ITexture, IDisposable
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public TextureView View { get; }
    }
}