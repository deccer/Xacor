using System;

namespace Xacor.Graphics.Api
{
    [Flags]
    public enum TextureViewType
    {
        RenderTarget = 1,
        ShaderResource = 2,
        DepthStencil = 4
    }
}
