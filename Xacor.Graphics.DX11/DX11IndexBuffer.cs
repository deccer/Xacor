using System;
using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Xacor.Graphics.DX11
{
    internal class DX11IndexBuffer<T> : IDisposable where T : struct
    {
        internal Buffer NativeBuffer { get; }

        private readonly int _stride = Marshal.SizeOf<T>();

        public bool Is16Bit => typeof(T) == typeof(short) || typeof(T) == typeof(ushort);

        public void Dispose()
        {
            NativeBuffer.Dispose();
        }

        public DX11IndexBuffer(DX11GraphicsDevice graphicsDevice, T[] indices)
        {
            var bufferDescription = new BufferDescription(indices.Length * _stride, BindFlags.IndexBuffer, ResourceUsage.Default);
            NativeBuffer = Buffer.Create(graphicsDevice.NativeDevice, indices, bufferDescription);
        }
    }
}