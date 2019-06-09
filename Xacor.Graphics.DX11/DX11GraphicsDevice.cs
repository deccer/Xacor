using SharpDX.Direct3D11;
using D3D11Device = SharpDX.Direct3D11.Device;
using D3D11DeviceContext = SharpDX.Direct3D11.DeviceContext;

namespace Xacor.Graphics.DX11
{
    internal class DX11GraphicsDevice : IGraphicsDevice
    {
        public D3D11Device NativeDevice { get; }

        public D3D11DeviceContext NativeDeviceContext { get; }

        public DX11GraphicsDevice(DeviceType deviceType)
        {
            var deviceCreationFlags = DeviceCreationFlags.BgraSupport;
#if DEBUG
            deviceCreationFlags = deviceCreationFlags | DeviceCreationFlags.Debug;
#endif
            NativeDevice = new D3D11Device(deviceType.ToSharpDX(), deviceCreationFlags);
            NativeDeviceContext = NativeDevice.ImmediateContext;
        }
    }
}