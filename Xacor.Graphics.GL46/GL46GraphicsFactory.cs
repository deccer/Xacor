using System;
using System.Collections.Generic;
using System.Linq;

namespace Xacor.Graphics.GL46
{
    public class GL46GraphicsFactory : IGraphicsFactory
    {
        private readonly GL46GraphicsDevice _graphicsDevice;

        public IBlendState CreateBlendState(bool isBlendEnabled, Blend sourceBlend, Blend destinationBlend,
            BlendOperation blendOperation, Blend sourceAlphaBlend, Blend destinationAlphaBlend,
            BlendOperation blendOperationAlpha)
        {
            return null;
        }

        public ICommandList CreateCommandList()
        {
            return new GL46CommandList();
        }

        public IConstantBuffer CreateConstantBuffer<T>(T constants) where T : struct
        {
            return GL46ConstantBuffer.Create(constants);
        }

        public IDepthStencilState CreateDepthStencilState()
        {
            return null;
        }

        public IPipeline CreatePipeline(Shader vertexShader, Shader pixelShader, IInputLayout inputLayout, IBlendState blendState,
            IDepthStencilState depthStencilState, IRasterizerState rasterizerState, Viewport viewport,
            PrimitiveTopology primitiveTopology)
        {
            return new GL46Pipeline(vertexShader, pixelShader, inputLayout);
        }

        public IRasterizerState CreateRasterizerState(CullMode cullMode, FillMode fillMode, bool isDepthEnabled, bool isScissorEnabled,
            bool isMultiSampleEnabled, bool isAntialiasedLineEnabled)
        {
            return null;
        }

        public ISampler CreateSampler(TextureAddressMode addressModeU, TextureAddressMode addressModeV, Filter filter, ComparisonFunction comparisonFunction)
        {
            return null;
        }

        public Shader CreateShaderFromFile(ShaderStage shaderStage, string filePath, VertexType vertexType, IEnumerable<(string, string)> macros)
        {
            var shader = new GL46Shader(this);
            foreach (var (macroName, macroValue) in macros.ToList())
            {
                shader.AddMacro(macroName, macroValue);
            }
            shader.CompileAsync(shaderStage, filePath, vertexType);
            return shader;
        }

        public ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo)
        {
            return new GL46SwapChain(swapChainInfo);
        }

        public ITextureFactory CreateTextureFactory()
        {
            return new GL46TextureFactory();
        }

        public IVertexBuffer CreateVertexBuffer<T>(T[] vertices) where T : struct
        {
            return GL46VertexBuffer.Create(vertices);
        }

        public IIndexBuffer CreateIndexBuffer<T>(T[] indices) where T : struct
        {
            return GL46IndexBuffer.Create(indices);
        }

        public void Dispose()
        {
            _graphicsDevice?.Dispose();
        }

        internal IInputLayout CreateInputLayout(VertexType vertexType)
        {
            var attributes = new List<VertexAttribute>();

            switch (vertexType)
            {
                case VertexType.Position:
                    attributes.Add(new VertexAttribute("i_position", 0, 12, 0, Format.R32G32B32Float));
                    break;
                case VertexType.PositionColor:
                    attributes.Add(new VertexAttribute("i_position", 0, 12, 0, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("i_color", 1, 16, 12, Format.R32G32B32A32Float));
                    break;
                case VertexType.PositionTexture:
                    attributes.Add(new VertexAttribute("i_position", 0, 12, 0, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("i_uv", 1, 8, 12, Format.R32G32Float));
                    break;
                case VertexType.PositionTextureNormalTangent:
                    attributes.Add(new VertexAttribute("i_position", 0, 12, 0, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("i_uv", 1, 8, 12, Format.R32G32Float));
                    attributes.Add(new VertexAttribute("i_normal", 2, 12, 20, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("i_tangent", 3, 12, 32, Format.R32G32B32Float));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vertexType), vertexType, null);
            }

            return new GL46InputLayout(attributes);
        }

        public GL46GraphicsFactory()
        {
            _graphicsDevice = new GL46GraphicsDevice();
        }
    }
}
