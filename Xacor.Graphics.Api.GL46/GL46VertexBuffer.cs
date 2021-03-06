﻿using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace Xacor.Graphics.Api.GL46
{
    internal class GL46VertexBuffer : IVertexBuffer
    {
        private readonly int _nativeBuffer;

        public int VertexStride { get; private set; }

        public static implicit operator int(GL46VertexBuffer vertexBuffer)
        {
            return vertexBuffer._nativeBuffer;
        }

        public static IVertexBuffer Create<T>(T[] vertices) where T : struct
        {
            var buffer = new GL46VertexBuffer();
            buffer.Initialize(vertices);
            return buffer;
        }

        public void Dispose()
        {
            OpenTK.Graphics.OpenGL4.GL.DeleteBuffer(_nativeBuffer);
        }

        private GL46VertexBuffer()
        {
            OpenTK.Graphics.OpenGL4.GL.CreateBuffers(1, out _nativeBuffer);
        }

        private void Initialize<T>(T[] vertices) where T : struct
        {
            VertexStride = Marshal.SizeOf<T>();
            var size = vertices.Length * VertexStride;
            OpenTK.Graphics.OpenGL4.GL.NamedBufferStorage(_nativeBuffer, size, vertices, BufferStorageFlags.DynamicStorageBit);
        }
    }
}