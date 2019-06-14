using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Xacor.Graphics.DX11
{
    internal class DX11VertexBuffer : IVertexBuffer
    {
        private readonly DX11GraphicsDevice _graphicsDevice;
        private Buffer _buffer;
        private VertexBufferBinding _vertexBufferBinding;

        public static implicit operator VertexBufferBinding(DX11VertexBuffer vertexBuffer)
        {
            return vertexBuffer._vertexBufferBinding;
        }

        public VertexBufferBinding GetVertexBufferBinding()
        {
            return _vertexBufferBinding;
        }

        public void Dispose()
        {
            _buffer?.Dispose();
        }

        public DX11VertexBuffer(DX11GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void Initialize<T>(T[] vertices) where T : struct
        {
            var stride = Marshal.SizeOf<T>();
            var bufferDescription = new BufferDescription(vertices.Length * stride, BindFlags.VertexBuffer, ResourceUsage.Default);
            _buffer = Buffer.Create(_graphicsDevice, vertices, bufferDescription);
            _vertexBufferBinding = new DX11VertexBufferBinding(_buffer, stride, 0);
        }
    }
}