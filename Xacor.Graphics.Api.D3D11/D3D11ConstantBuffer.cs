using System.Runtime.InteropServices;
using SharpDX.Direct3D11;
using D3D11Buffer = SharpDX.Direct3D11.Buffer;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11ConstantBuffer : IConstantBuffer
    {
        private readonly D3D11GraphicsDevice _graphicsDevice;
        private D3D11Buffer _nativeBuffer;
        private int _stride;
        
        public void Dispose()
        {
            _nativeBuffer?.Dispose();
        }

        public static implicit operator D3D11Buffer(D3D11ConstantBuffer constantBuffer)
        {
            return constantBuffer._nativeBuffer;
        }

        public static IConstantBuffer Create<T>(D3D11GraphicsDevice graphicsDevice, T constants) where T: struct
        {
            var buffer = new D3D11ConstantBuffer(graphicsDevice);
            buffer.Initialize(constants);
            return buffer;
        }

        private D3D11ConstantBuffer(D3D11GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        private void Initialize<T>(T constants) where T: struct
        {
            _stride = Marshal.SizeOf<T>();
            //var bufferDescription = new BufferDescription(_stride, ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, _stride);
            var bufferDescription = new BufferDescription(_stride, ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, _stride);
            _nativeBuffer = D3D11Buffer.Create(_graphicsDevice, ref constants, bufferDescription);
        }

        public void UpdateBuffer<T>(T constants) where T: struct
        {
            _graphicsDevice.NativeDeviceContext.UpdateSubresource(ref constants, _nativeBuffer, 0, _stride);

            //_graphicsDevice.NativeDeviceContext.MapSubresource(_nativeBuffer, 0, MapMode.WriteDiscard, MapFlags.None, out var dataStream);
            //dataStream.Write(constants);
            //_graphicsDevice.NativeDeviceContext.UnmapSubresource(_nativeBuffer, 0);
        }
    }
}