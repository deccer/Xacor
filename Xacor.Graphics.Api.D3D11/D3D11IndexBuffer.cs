using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11IndexBuffer : IIndexBuffer
    {
        private readonly D3D11GraphicsDevice _graphicsDevice;
        private Buffer _nativeBuffer;

        public bool Is16Bit { get; private set; }

        public static implicit operator Buffer(D3D11IndexBuffer indexBuffer)
        {
            return indexBuffer._nativeBuffer;
        }

        public static IIndexBuffer Create<T>(D3D11GraphicsDevice graphicsDevice, ref T[] indices) where T : struct
        {
            var indexBuffer = new D3D11IndexBuffer(graphicsDevice);
            indexBuffer.Initialize(ref indices);
            indexBuffer.Is16Bit = typeof(T) == typeof(short) || typeof(T) == typeof(ushort);
            return indexBuffer;
        }

        public void Dispose()
        {
            _nativeBuffer.Dispose();
        }

        private D3D11IndexBuffer(D3D11GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        private void Initialize<T>(ref T[] indices) where T : struct
        {
            var stride = Marshal.SizeOf<T>();
            var bufferDescription = new BufferDescription(indices.Length * stride, BindFlags.IndexBuffer, ResourceUsage.Default);
            _nativeBuffer = Buffer.Create(_graphicsDevice, indices, bufferDescription);
        }
    }
}