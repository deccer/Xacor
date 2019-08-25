using SharpDX;
using SharpDX.Direct3D12;

namespace Xacor.Graphics.Api.DX12
{
    internal class DX12GraphicsDevice : IGraphicsDevice
    {
        private readonly Device _nativeDevice;

        public static implicit operator ComObject(DX12GraphicsDevice graphicsDevice)
        {
            return graphicsDevice._nativeDevice;
        }

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
#if DEBUG
            DebugInterface.Get().EnableDebugLayer();
#endif
            _nativeDevice = new Device();
        }

        public DescriptorHeap CreateDescriptorHeap(DescriptorHeapDescription descriptorHeapDescription)
        {
            return _nativeDevice.CreateDescriptorHeap(descriptorHeapDescription);
        }
    }
}