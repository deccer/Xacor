using SharpDX.Direct3D11;

namespace Xacor.Graphics.DX11
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

        public DX11Sampler(DX11GraphicsDevice graphicsDevice)
        {
            var samplerStateDescription = SamplerStateDescription.Default();
            _nativeSamplerState = new SamplerState(graphicsDevice, samplerStateDescription);
        }
    }
}