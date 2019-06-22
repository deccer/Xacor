using SharpDX.Direct3D11;

namespace Xacor.Graphics.Api.DX11
{
    internal class DX11RasterizerState : IRasterizerState
    {
        private readonly DX11GraphicsDevice _graphicsDevice;
        private readonly RasterizerState _nativeRasterizerState;

        public CullMode CullMode { get; }

        public FillMode FillMode { get; }

        public bool IsDepthEnabled => _nativeRasterizerState.Description.IsDepthClipEnabled;

        public bool IsScissorEnabled => _nativeRasterizerState.Description.IsScissorEnabled;

        public bool IsMultiSampleEnabled => _nativeRasterizerState.Description.IsMultisampleEnabled;

        public bool IsAntialiasedLineEnabled => _nativeRasterizerState.Description.IsAntialiasedLineEnabled;

        public void Dispose()
        {
            _nativeRasterizerState?.Dispose();
        }

        public DX11RasterizerState(DX11GraphicsDevice graphicsDevice, CullMode cullMode, FillMode fillMode, bool isDepthEnabled, bool isScissorEnabled, bool isMultiSampleEnabled, bool isAntialiasedLineEnabled)
        {
            _graphicsDevice = graphicsDevice;
            CullMode = cullMode;
            FillMode = fillMode;
            _nativeRasterizerState = CreateRasterizerState(isDepthEnabled, isScissorEnabled, isMultiSampleEnabled, isAntialiasedLineEnabled);
        }

        private RasterizerState CreateRasterizerState(bool isDepthEnabled, bool isScissorEnabled, bool isMultiSampleEnabled, bool isAntialiasedLineEnabled)
        {
            var rasterizerDescription = RasterizerStateDescription.Default();
            rasterizerDescription.CullMode = CullMode.ToSharpDX();
            rasterizerDescription.FillMode = FillMode.ToSharpDX();
            rasterizerDescription.IsDepthClipEnabled = isDepthEnabled;
            rasterizerDescription.IsScissorEnabled = isScissorEnabled;
            rasterizerDescription.IsMultisampleEnabled = isMultiSampleEnabled;
            rasterizerDescription.IsAntialiasedLineEnabled = isAntialiasedLineEnabled;
            return new RasterizerState(_graphicsDevice, rasterizerDescription);
        }

        public static implicit operator RasterizerState(DX11RasterizerState rasterizerState)
        {
            return rasterizerState._nativeRasterizerState;
        }
    }
}
