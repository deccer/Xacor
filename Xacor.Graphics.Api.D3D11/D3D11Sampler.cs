using SharpDX.Direct3D11;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11Sampler : ISampler
    {
        private readonly SamplerState _nativeSamplerState;

        public static implicit operator SamplerState(D3D11Sampler sampler)
        {
            return sampler._nativeSamplerState;
        }

        public void Dispose()
        {
            _nativeSamplerState?.Dispose();
        }

        public D3D11Sampler(D3D11GraphicsDevice graphicsDevice, TextureAddressMode addressModeU, TextureAddressMode addressModeV, Filter filter, ComparisonFunction comparisonFunction)
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