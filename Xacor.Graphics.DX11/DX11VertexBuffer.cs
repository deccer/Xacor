using System;
using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Xacor.Graphics.DX11
{
    internal class DX11VertexBuffer<T> : IDisposable where T: struct
    {
        private readonly Buffer _buffer;
        private readonly int _stride = Marshal.SizeOf<T>();

        private VertexBufferBinding GetVertexBufferBinding()
        {
            return new VertexBufferBinding(_buffer, _stride, 0);
        }

        public void Dispose()
        {
            _buffer?.Dispose();
        }

        public DX11VertexBuffer(DX11GraphicsDevice graphicsDevice, T[] vertices)
        {
            var bufferDescription = new BufferDescription(vertices.Length * _stride, BindFlags.VertexBuffer, ResourceUsage.Default);
            _buffer = Buffer.Create(graphicsDevice.NativeDevice, vertices, bufferDescription);
        }

        public static implicit operator VertexBufferBinding(DX11VertexBuffer<T> vertexBuffer)
        {
            return vertexBuffer.GetVertexBufferBinding();
        }
    }
}