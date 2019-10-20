using SharpDX.Direct3D11;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11BlendState : IBlendState
    {
        private readonly D3D11GraphicsDevice _graphicsDevice;
        private readonly BlendState _nativeBlendState;

        public void Dispose()
        {
            _nativeBlendState?.Dispose();
        }

        public D3D11BlendState(D3D11GraphicsDevice graphicsDevice, bool isBlendEnabled,
            Blend sourceBlend, Blend destinationBlend, BlendOperation blendOperation,
            Blend sourceAlphaBlend, Blend destinationAlphaBlend, BlendOperation blendOperationAlpha)
        {
            _graphicsDevice = graphicsDevice;
            _nativeBlendState = CreateBlendState(isBlendEnabled, sourceBlend, destinationBlend, blendOperation, sourceAlphaBlend,  destinationAlphaBlend, blendOperationAlpha);
        }

        private BlendState CreateBlendState(bool isBlendEnabled, 
            Blend sourceBlend, Blend destinationBlend, BlendOperation blendOperation, 
            Blend sourceAlphaBlend, Blend destinationAlphaBlend, BlendOperation blendOperationAlpha)
        {
            var blendStateDescription =  BlendStateDescription.Default();
            for (var i = 0; i < 8; i++)
            {
                blendStateDescription.RenderTarget[i].IsBlendEnabled = true;

                blendStateDescription.RenderTarget[i].SourceBlend = sourceBlend.ToSharpDX();
                blendStateDescription.RenderTarget[i].DestinationBlend = destinationBlend.ToSharpDX();
                blendStateDescription.RenderTarget[i].BlendOperation = blendOperation.ToSharpDX();

                blendStateDescription.RenderTarget[i].SourceAlphaBlend = sourceAlphaBlend.ToSharpDX();
                blendStateDescription.RenderTarget[i].DestinationAlphaBlend = destinationAlphaBlend.ToSharpDX();
                blendStateDescription.RenderTarget[i].AlphaBlendOperation = blendOperationAlpha.ToSharpDX();

                blendStateDescription.RenderTarget[i].RenderTargetWriteMask = ColorWriteMaskFlags.All;
            }
            var blendState  = new BlendState(_graphicsDevice, blendStateDescription);
            return blendState;
        }

        public static implicit operator BlendState(D3D11BlendState blendState)
        {
            return blendState._nativeBlendState;
        }
    }
}