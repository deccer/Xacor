using SharpDX.Direct3D11;

namespace Xacor.Graphics.Api.DX11
{
    internal class DX11Sampler : ISampler
    {
        private readonly SamplerState _nativeSamplerState;

        public static implicit operator SamplerState(DX11Sampler sampler)
        {
            return sampler._nativeSamplerState;
        }

        public void Dispose()
        {
            _nativeSamplerState?.Dispose();
        }

        public DX11Sampler(DX11GraphicsDevice graphicsDevice, TextureAddressMode addressModeU, TextureAddressMode addressModeV, Filter filter, ComparisonFunction comparisonFunction)
        {
            var samplerStateDescription = SamplerStateDescription.Default();
            samplerStateDescription.AddressU = addressModeU.ToSharpDX();
            samplerStateDescription.AddressV = addressModeV.ToSharpDX();
            samplerStateDescription.ComparisonFunction = comparisonFunction.ToSharpDX();
            samplerStateDescription.Filter = filter.ToSharpDX();
            _nativeSamplerState = new SamplerState(graphicsDevice, samplerStateDescription);
        }
    }
}