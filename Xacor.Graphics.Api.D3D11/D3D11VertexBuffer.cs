using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11VertexBuffer : IVertexBuffer
    {
        private readonly D3D11GraphicsDevice _graphicsDevice;
        private Buffer _buffer;
        private VertexBufferBinding _vertexBufferBinding;

        public int VertexStride { get; private set; }

        public static implicit operator VertexBufferBinding(D3D11VertexBuffer vertexBuffer)
        {
            return vertexBuffer._vertexBufferBinding;
        }

        public static IVertexBuffer Create<T>(D3D11GraphicsDevice graphicsDevice, ref T[] vertices) where T : struct
        {
            var vertexBuffer = new D3D11VertexBuffer(graphicsDevice);
            vertexBuffer.Initialize(ref vertices);
            return vertexBuffer;
        }

        public void Dispose()
        {
            _buffer?.Dispose();
        }

        private D3D11VertexBuffer(D3D11GraphicsDevice graphicsDevice)
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
            _vertexBufferBinding = new D3D11VertexBufferBinding(_buffer, VertexStride, 0);
        }
    }
}