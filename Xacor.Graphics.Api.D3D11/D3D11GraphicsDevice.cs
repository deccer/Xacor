using System;
using System.Net.Http.Headers;
using SharpDX.Direct3D11;
using Xacor.Graphics.Api.D3D;
using D3D11Device = SharpDX.Direct3D11.Device;
using D3D11DeviceContext = SharpDX.Direct3D11.DeviceContext;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11GraphicsDevice : IGraphicsDevice
    {
        private readonly D3D11Device _nativeDevice;

        internal D3D11DeviceContext NativeDeviceContext { get; }

        public void Dispose()
        {
            _nativeDevice?.Dispose();
        }

        public D3D11GraphicsDevice(DeviceType deviceType, bool isDebug)
        {
            var deviceCreationFlags = DeviceCreationFlags.BgraSupport;
            retryCreateDevice:
            try
            {
                if (isDebug)
                {
                    deviceCreationFlags |= DeviceCreationFlags.Debug;
                }

                _nativeDevice = new D3D11Device(deviceType.ToSharpDX(), deviceCreationFlags);
            }
            catch
            {
                deviceCreationFlags &= ~DeviceCreationFlags.Debug;
                isDebug = false;
                goto retryCreateDevice;
            }

            NativeDeviceContext = _nativeDevice.ImmediateContext;
        }

        public static implicit operator D3D11Device(D3D11GraphicsDevice graphicsDevice)
        {
            return graphicsDevice._nativeDevice;
        }
    }
}