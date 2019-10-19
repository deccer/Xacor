using System;
using SharpDX.Direct3D12;

namespace Xacor.Graphics.Api.DX12
{
    internal class DX12TextureView : TextureView, IDisposable
    {
        private readonly DescriptorHeap _rtvDescriptorHeap;
        private readonly DescriptorHeap _dsvDescriptorHeap;

        public void Dispose()
        {
            _rtvDescriptorHeap?.Dispose();
            _dsvDescriptorHeap?.Dispose();
        }

        public DX12TextureView(DX12GraphicsDevice graphicsDevice, 
            int descriptorCount,
            DescriptorHeapFlags descriptorHeapFlags, 
            DescriptorHeapType descriptorHeapType)
        {
            var descriptorHeapDescription = new DescriptorHeapDescription
            {
                DescriptorCount = descriptorCount,
                Flags = descriptorHeapFlags,
                Type = descriptorHeapType
            };

            if (descriptorHeapType == DescriptorHeapType.RenderTargetView)
            {
                _rtvDescriptorHeap = graphicsDevice.CreateDescriptorHeap(descriptorHeapDescription);
            }
            else if (descriptorHeapType == DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView)
            {
                _dsvDescriptorHeap = graphicsDevice.CreateDescriptorHeap(descriptorHeapDescription);
            }
        }
    }
}