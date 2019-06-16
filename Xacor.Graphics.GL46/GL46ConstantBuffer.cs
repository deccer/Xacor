using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace Xacor.Graphics.GL46
{
    internal class GL46ConstantBuffer : IConstantBuffer
    {
        private readonly int _bufferSize;
        private readonly int _nativeBuffer;

        public static implicit operator int(GL46ConstantBuffer constantBuffer)
        {
            return constantBuffer._nativeBuffer;
        }

        public static IConstantBuffer Create<T>(T constants) where T : struct
        {
            var size = Marshal.SizeOf<T>();
            var buffer = new GL46ConstantBuffer(size);
            buffer.Initialize(constants);
            return buffer;
        }

        public void Dispose()
        {
            OpenTK.Graphics.OpenGL4.GL.DeleteBuffer(_nativeBuffer);
        }

        private GL46ConstantBuffer(int bufferSize)
        {
            _bufferSize = bufferSize;

            OpenTK.Graphics.OpenGL4.GL.CreateBuffers(1, out _nativeBuffer);
        }

        private void Initialize<T>(T constants) where T : struct
        {
            OpenTK.Graphics.OpenGL4.GL.NamedBufferData(_nativeBuffer, _bufferSize, ref constants, BufferUsageHint.DynamicDraw);
        }

        public void UpdateBuffer<T>(T constants) where T : struct
        {
            OpenTK.Graphics.OpenGL4.GL.NamedBufferData(_nativeBuffer, _bufferSize, ref constants, BufferUsageHint.DynamicDraw);
        }
    }
}