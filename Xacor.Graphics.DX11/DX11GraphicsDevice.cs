using SharpDX.Direct3D11;
using Xacor.Graphics.DX;
using D3D11Device = SharpDX.Direct3D11.Device;
using D3D11DeviceContext = SharpDX.Direct3D11.DeviceContext;

namespace Xacor.Graphics.DX11
{
    internal class DX11GraphicsDevice : IGraphicsDevice
    {
        private readonly D3D11Device _nativeDevice;

        public D3D11DeviceContext NativeDeviceContext { get; }

        public void Dispose()
        {
            _nativeDevice?.Dispose();
        }

        public DX11GraphicsDevice(DeviceType deviceType)
        {
            var deviceCreationFlags = DeviceCreationFlags.BgraSupport;
#if DEBUG
            deviceCreationFlags |= DeviceCreationFlags.Debug;
#endif
            _nativeDevice = new D3D11Device(deviceType.ToSharpDX(), deviceCreationFlags);
            NativeDeviceContext = _nativeDevice.ImmediateContext;
        }

        public static implicit operator D3D11Device(DX11GraphicsDevice graphicsDevice)
        {
            return graphicsDevice._nativeDevice;
        }
    }
}