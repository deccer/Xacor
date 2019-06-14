using System;
using System.Collections.Generic;
using System.Linq;

namespace Xacor.Graphics.DX11
{
    public class DX11GraphicsFactory : IGraphicsFactory
    {
        private readonly DX11GraphicsDevice _graphicsDevice;

        public DX11GraphicsFactory(DeviceType deviceType)
        {
            _graphicsDevice = new DX11GraphicsDevice(deviceType);
        }

        public IBlendState CreateBlendState(bool isBlendEnabled, Blend sourceBlend, Blend destinationBlend, BlendOperation blendOperation,
            Blend sourceAlphaBlend, Blend destinationAlphaBlend, BlendOperation blendOperationAlpha)
        {
            return new DX11BlendState(_graphicsDevice, isBlendEnabled, sourceBlend, destinationBlend, blendOperation,
                sourceAlphaBlend, destinationAlphaBlend, blendOperationAlpha);
        }

        public ICommandList CreateCommandList()
        {
            return new DX11CommandList(_graphicsDevice);
        }

        public IConstantBuffer CreateConstantBuffer<T>(T constants) where T: struct
        {
            return DX11ConstantBuffer.Create(_graphicsDevice, constants);
        }

        public IDepthStencilState CreateDepthStencilState()
        {
            return new DX11DepthStencilState(_graphicsDevice);
        }

        public IPipeline CreatePipeline(Shader vertexShader, Shader pixelShader, IInputLayout inputLayout,
            IBlendState blendState, IDepthStencilState depthStencilState, IRasterizerState rasterizerState,
            Viewport viewport, PrimitiveTopology primitiveTopology)
        {
            return new DX11Pipeline(vertexShader, pixelShader, inputLayout, blendState, depthStencilState, rasterizerState, viewport, primitiveTopology);
        }

        public IRasterizerState CreateRasterizerState(CullMode cullMode, FillMode fillMode, 
            bool isDepthEnabled, bool isScissorEnabled, bool isMultiSampleEnabled, bool isAntialiasedLineEnabled)
        {
            return new DX11RasterizerState(_graphicsDevice, cullMode, fillMode, isDepthEnabled, isScissorEnabled, isMultiSampleEnabled, isAntialiasedLineEnabled);
        }

        public Shader CreateShaderFromFile(ShaderStage shaderStage, string filePath, VertexType vertexType, IEnumerable<(string, string)> macros)
        {
            var shader = new DX11Shader(_graphicsDevice, this);
            foreach (var (macroName, macroValue) in macros.ToList())
            {
                shader.AddMacro(macroName, macroValue);
            }
            shader.CompileAsync(shaderStage, filePath, vertexType);
            return shader;
        }

        public ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo)
        {
            return new DX11SwapChain(_graphicsDevice, swapChainInfo);
        }

        public IVertexBuffer CreateVertexBuffer<T>(T[] vertices) where T: struct
        {
            var vertexBuffer = new DX11VertexBuffer(_graphicsDevice);
            vertexBuffer.Initialize(vertices);
            return vertexBuffer;
        }

        internal DX11InputLayout CreateInputLayout(VertexType vertexType, byte[] shaderByteCode)
        {
            var attributes = new List<VertexAttribute>();

            switch (vertexType)
            {
                case VertexType.Position:
                    attributes.Add(new VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    break;
                case VertexType.PositionColor:
                    attributes.Add(new VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("COLOR", 0, 0, 12, Format.R32G32B32A32Float));
                    break;
                case VertexType.PositionTexture:
                    attributes.Add(new VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("TEXTURE", 0, 0, 12, Format.R32G32Float));
                    break;
                case VertexType.PositionTextureNormalTangent:
                    attributes.Add(new VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("TEXTURE", 0, 0, 12, Format.R32G32Float));
                    attributes.Add(new VertexAttribute("NORMAL", 0, 0, 20, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("TANGENT", 0, 0, 32, Format.R32G32B32Float));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vertexType), vertexType, null);
            }

            return new DX11InputLayout(_graphicsDevice, shaderByteCode, attributes);
        }
    }
}