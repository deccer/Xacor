using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xacor.Graphics.DX12
{
    public class DX12GraphicsFactory : IGraphicsFactory
    {
        public IBlendState CreateBlendState(bool isBlendEnabled, Blend sourceBlend, Blend destinationBlend,
            BlendOperation blendOperation, Blend sourceAlphaBlend, Blend destinationAlphaBlend,
            BlendOperation blendOperationAlpha)
        {
            throw new System.NotImplementedException();
        }

        public ICommandList CreateCommandList()
        {
            throw new System.NotImplementedException();
        }

        public IConstantBuffer CreateConstantBuffer<T>(T constants) where T : struct
        {
            throw new System.NotImplementedException();
        }

        public IDepthStencilState CreateDepthStencilState()
        {
            throw new System.NotImplementedException();
        }

        public IPipeline CreatePipeline(Shader vertexShader, Shader pixelShader, IInputLayout inputLayout, IBlendState blendState,
            IDepthStencilState depthStencilState, IRasterizerState rasterizerState, Viewport viewport,
            PrimitiveTopology primitiveTopology)
        {
            throw new System.NotImplementedException();
        }

        public IRasterizerState CreateRasterizerState(CullMode cullMode, FillMode fillMode, bool isDepthEnabled, bool isScissorEnabled,
            bool isMultiSampleEnabled, bool isAntialiasedLineEnabled)
        {
            throw new System.NotImplementedException();
        }

        public IPipeline CreatePipeline()
        {
            throw new System.NotImplementedException();
        }

        public Shader CreateShaderFromFile(ShaderStage shaderStage, string filePath, VertexType vertexType, IEnumerable<(string, string)> macros)
        {
            throw new System.NotImplementedException();
        }

        public ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo)
        {
            return new DX12SwapChain();
        }

        public IVertexBuffer CreateVertexBuffer<T>(T[] vertices) where T : struct
        {
            throw new System.NotImplementedException();
        }
    }
}