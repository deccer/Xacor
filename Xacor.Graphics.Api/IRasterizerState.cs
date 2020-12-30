using System;

namespace Xacor.Graphics.Api
{
    public interface IRasterizerState : IDisposable
    {
        CullMode CullMode { get; }

        FillMode FillMode { get; }

        bool IsDepthEnabled { get; }

        bool IsScissorEnabled { get; }

        bool IsMultiSampleEnabled { get; }

        bool IsAntialiasedLineEnabled { get; }
    }
}
