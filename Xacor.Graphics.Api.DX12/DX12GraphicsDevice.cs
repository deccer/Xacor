using SharpDX.Direct3D12;

namespace Xacor.Graphics.Api.DX12
{
    internal class DX12GraphicsDevice : IGraphicsDevice
    {
        private readonly Device _nativeDevice;

        public static implicit operator Device(DX12GraphicsDevice graphicsDevice)
        {
            return graphicsDevice._nativeDevice;
        }

        public void Dispose()
        {
            _nativeDevice?.Dispose();
        }

        public DX12GraphicsDevice()
        {
            _nativeDevice = new Device();
        }
    }
}