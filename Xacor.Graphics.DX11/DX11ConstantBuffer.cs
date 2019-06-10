using System;
using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Xacor.Graphics.DX11
{
    internal class DX11ConstantBuffer<T> : ConstantBuffer<T>, IDisposable where T : struct
    {
        internal Buffer NativeBuffer { get; }

        private readonly DX11GraphicsDevice _graphicsDevice;
        private readonly int _stride = Marshal.SizeOf<T>();

        public void Dispose()
        {
            NativeBuffer?.Dispose();
        }

        public DX11ConstantBuffer(DX11GraphicsDevice graphicsDevice, T constants)
        {
            _graphicsDevice = graphicsDevice;

            var bufferDescription = new BufferDescription(_stride, BindFlags.ConstantBuffer, ResourceUsage.Default);
            NativeBuffer = Buffer.Create(graphicsDevice.NativeDevice, ref constants, bufferDescription);
        }

        public override void UpdateBuffer(T constants)
        {
            _graphicsDevice.NativeDeviceContext.MapSubresource(NativeBuffer, 0, MapMode.Write, MapFlags.None, out var dataStream);
            dataStream.Write(constants);
            _graphicsDevice.NativeDeviceContext.UnmapSubresource(NativeBuffer, 0);
        }
    }
}