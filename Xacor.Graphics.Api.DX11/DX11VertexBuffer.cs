using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Xacor.Graphics.Api.DX11
{
    internal class DX11VertexBuffer : IVertexBuffer
    {
        private readonly DX11GraphicsDevice _graphicsDevice;
        private Buffer _buffer;
        private VertexBufferBinding _vertexBufferBinding;

        public int VertexStride { get; private set; }

        public static implicit operator VertexBufferBinding(DX11VertexBuffer vertexBuffer)
        {
            return vertexBuffer._vertexBufferBinding;
        }

        public static IVertexBuffer Create<T>(DX11GraphicsDevice graphicsDevice, ref T[] vertices) where T : struct
        {
            var vertexBuffer = new DX11VertexBuffer(graphicsDevice);
            vertexBuffer.Initialize(ref vertices);
            return vertexBuffer;
        }

        public void Dispose()
        {
            _buffer?.Dispose();
        }

        private DX11VertexBuffer(DX11GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public VertexBufferBinding GetVertexBufferBinding()
        {
            return _vertexBufferBinding;
        }

        private void Initialize<T>(ref T[] vertices) where T : struct
        {
            VertexStride = Marshal.SizeOf<T>();
            var bufferDescription = new BufferDescription(vertices.Length * VertexStride, BindFlags.VertexBuffer, ResourceUsage.Default);
            _buffer = Buffer.Create(_graphicsDevice, vertices, bufferDescription);
            _vertexBufferBinding = new DX11VertexBufferBinding(_buffer, VertexStride, 0);
        }
    }
}