using System.Collections.Generic;
using Xacor.Mathematics;

namespace Xacor.Graphics.Api
{
    public interface IGraphicsFactory 
    {
        IBlendState CreateBlendState(bool isBlendEnabled, Blend sourceBlend, Blend destinationBlend, BlendOperation blendOperation, Blend sourceAlphaBlend, Blend destinationAlphaBlend, BlendOperation blendOperationAlpha);

        ICommandList CreateCommandList();

        IConstantBuffer CreateConstantBuffer<T>(T constants) where T : struct;

        IDepthStencilState CreateDepthStencilState();

        IPipeline CreatePipeline(Shader vertexShader, Shader pixelShader, IInputLayout inputLayout,
            IBlendState blendState, IDepthStencilState depthStencilState, IRasterizerState rasterizerState,
            Viewport viewport, PrimitiveTopology primitiveTopology);

        IRasterizerState CreateRasterizerState(CullMode cullMode, FillMode fillMode, bool isDepthEnabled, bool isScissorEnabled, bool isMultiSampleEnabled, bool isAntialiasedLineEnabled);

        ISampler CreateSampler(TextureAddressMode addressModeU, TextureAddressMode addressModeV, Filter filter, ComparisonFunction comparisonFunction);

        Shader CreateShaderFromFile(ShaderStage shaderStage, string filePath, VertexType vertexType, IEnumerable<(string, string)> macros);

        ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo);

        ITextureFactory CreateTextureFactory();

        IVertexBuffer CreateVertexBuffer<T>(T[] vertices) where T : struct;

        IIndexBuffer CreateIndexBuffer<T>(T[] indices) where T : struct;
    }
}