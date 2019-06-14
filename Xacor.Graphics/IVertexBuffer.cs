using System;

namespace Xacor.Graphics
{
    public interface IVertexBuffer : IDisposable
    {
        VertexBufferBinding GetVertexBufferBinding();
    }
}