using System;

namespace Xacor.Graphics.Api
{
    public interface IVertexBuffer : IDisposable
    {
        int VertexStride { get; }
    }
}
