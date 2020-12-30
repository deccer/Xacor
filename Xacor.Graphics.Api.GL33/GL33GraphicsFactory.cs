using System;
using System.Collections.Generic;
using Xacor.Mathematics;

namespace Xacor.Graphics.Api.GL33
{
    public class GL33GraphicsFactory : IGraphicsFactory, IDisposable
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

        public ISampler CreateSampler(TextureAddressMode addressModeU, TextureAddressMode addressModeV, Filter filter, ComparisonFunction comparisonFunction)
        {
            throw new System.NotImplementedException();
        }

        public Shader CreateShader(ShaderStage shaderStage, string shaderText, VertexType vertexType, IEnumerable<(string, string)> macros)
        {
            throw new NotImplementedException();
        }

        public Shader CreateShaderFromFile(ShaderStage shaderStage, string filePath, VertexType vertexType, IEnumerable<(string, string)> macros)
        {
            throw new System.NotImplementedException();
        }

        public ISwapChain CreateSwapchain(SwapChainDescriptor swapChainDescriptor)
        {
            return new GL33SwapChain(swapChainDescriptor);
        }

        public ITextureFactory CreateTextureFactory()
        {
            return new GL33TextureFactory();
        }

        public IVertexBuffer CreateVertexBuffer<T>(T[] vertices) where T : struct
        {
            throw new System.NotImplementedException();
        }

        public IIndexBuffer CreateIndexBuffer<T>(T[] indices) where T : struct
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
