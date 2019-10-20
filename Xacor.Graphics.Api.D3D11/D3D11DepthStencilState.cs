using SharpDX.Direct3D11;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11DepthStencilState : IDepthStencilState
    {
        private readonly D3D11GraphicsDevice _graphicsDevice;
        private readonly DepthStencilState _nativeDepthStencilState;

        public void Dispose()
        {
            _nativeDepthStencilState?.Dispose();
        }

        public D3D11DepthStencilState(D3D11GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _nativeDepthStencilState = CreateDepthStencilState();
        }

        private DepthStencilState CreateDepthStencilState()
        {
            var depthStencilStateDescription = DepthStencilStateDescription.Default();
            return new DepthStencilState(_graphicsDevice, depthStencilStateDescription);
        }

        public static implicit operator DepthStencilState(D3D11DepthStencilState depthStencilState)
        {
            return depthStencilState._nativeDepthStencilState;
        }
    }
}