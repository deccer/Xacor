using System;
using System.Collections.Generic;

namespace Xacor.Graphics.VK
{
    public class VKGraphicsFactory : IGraphicsFactory
    {
        public IBlendState CreateBlendState(bool isBlendEnabled, Blend sourceBlend, Blend destinationBlend,
            BlendOperation blendOperation, Blend sourceAlphaBlend, Blend destinationAlphaBlend,
            BlendOperation blendOperationAlpha)
        {
            throw new NotImplementedException();
        }

        public ICommandList CreateCommandList()
        {
            throw new NotImplementedException();
        }

        public IConstantBuffer CreateConstantBuffer<T>(T constants) where T : struct
        {
            throw new NotImplementedException();
        }

        public IDepthStencilState CreateDepthStencilState()
        {
            throw new NotImplementedException();
        }

        public IPipeline CreatePipeline(Shader vertexShader, Shader pixelShader, IInputLayout inputLayout, IBlendState blendState,
            IDepthStencilState depthStencilState, IRasterizerState rasterizerState, Viewport viewport,
            PrimitiveTopology primitiveTopology)
        {
            throw new NotImplementedException();
        }

        public IRasterizerState CreateRasterizerState(CullMode cullMode, FillMode fillMode, bool isDepthEnabled, bool isScissorEnabled,
            bool isMultiSampleEnabled, bool isAntialiasedLineEnabled)
        {
            throw new NotImplementedException();
        }

        public ISampler CreateSampler(TextureAddressMode addressModeU, TextureAddressMode addressModeV, Filter filter, ComparisonFunction comparisonFunction)
        {
            throw new NotImplementedException();
        }

        public IPipeline CreatePipeline()
        {
            throw new NotImplementedException();
        }

        public Shader CreateShaderFromFile(ShaderStage shaderStage, string filePath, VertexType vertexType, IEnumerable<(string, string)> macros)
        {
            throw new NotImplementedException();
        }

        public ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo)
        {
            throw new NotImplementedException();
        }

        public ITextureFactory CreateTextureFactory()
        {
            throw new NotImplementedException();
        }

        public IVertexBuffer CreateVertexBuffer<T>(T[] vertices) where T : struct
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}