using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace Xacor.Graphics.GL46
{
    internal class GL46IndexBuffer : IIndexBuffer
    {
        private readonly int _nativeBuffer;

        public static implicit operator int(GL46IndexBuffer indexBuffer)
        {
            return indexBuffer._nativeBuffer;
        }

        public static IIndexBuffer Create<T>(T[] indices) where T : struct
        {
            if (typeof(T) != typeof(ushort) || typeof(T) != typeof(uint))
            {
                throw new InvalidOperationException();
            }

            var buffer = new GL46IndexBuffer();
            buffer.Initialize(indices);
            buffer.Is16Bit = typeof(T) == typeof(ushort);
            return buffer;
        }

        public void Dispose()
        {
            OpenTK.Graphics.OpenGL4.GL.DeleteBuffer(_nativeBuffer);
        }

        private GL46IndexBuffer()
        {
            OpenTK.Graphics.OpenGL4.GL.CreateBuffers(1, out _nativeBuffer);
        }

        private void Initialize<T>(T[] indices) where T : struct
        {
            var size = indices.Length * Marshal.SizeOf<T>();
            OpenTK.Graphics.OpenGL4.GL.NamedBufferStorage(_nativeBuffer, size, indices, BufferStorageFlags.DynamicStorageBit);
        }

        public bool Is16Bit { get; private set; }
    }
}