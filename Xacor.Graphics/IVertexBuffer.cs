using System;

namespace Xacor.Graphics
{
    public interface IVertexBuffer : IDisposable
    {
        int VertexStride { get; }

        VertexBufferBinding GetVertexBufferBinding();
    }
}