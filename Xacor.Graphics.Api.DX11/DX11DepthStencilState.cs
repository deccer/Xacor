using SharpDX.Direct3D11;

namespace Xacor.Graphics.Api.DX11
{
    internal class DX11DepthStencilState : IDepthStencilState
    {
        private readonly DX11GraphicsDevice _graphicsDevice;
        private readonly DepthStencilState _nativeDepthStencilState;

        public void Dispose()
        {
            _nativeDepthStencilState?.Dispose();
        }

        public DX11DepthStencilState(DX11GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _nativeDepthStencilState = CreateDepthStencilState();
        }

        private DepthStencilState CreateDepthStencilState()
        {
            var depthStencilStateDescription = DepthStencilStateDescription.Default();
            return new DepthStencilState(_graphicsDevice, depthStencilStateDescription);
        }

        public static implicit operator DepthStencilState(DX11DepthStencilState depthStencilState)
        {
            return depthStencilState._nativeDepthStencilState;
        }
    }
}