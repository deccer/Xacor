using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using Xacor.Mathematics;

namespace Xacor.Graphics.Api.D3D11
{
    public sealed class D3D11GraphicsFactory : IGraphicsFactory
    {
        private readonly ILogger _logger;
        private readonly D3D11GraphicsDevice _graphicsDevice;

        public IBlendState CreateBlendState(bool isBlendEnabled, Blend sourceBlend, Blend destinationBlend, BlendOperation blendOperation,
            Blend sourceAlphaBlend, Blend destinationAlphaBlend, BlendOperation blendOperationAlpha)
        {
            return new D3D11BlendState(_graphicsDevice, isBlendEnabled, sourceBlend, destinationBlend, blendOperation,
                sourceAlphaBlend, destinationAlphaBlend, blendOperationAlpha);
        }

        public ICommandList CreateCommandList()
        {
            return new D3D11CommandList(_graphicsDevice);
        }

        public IConstantBuffer CreateConstantBuffer<T>(T constants) where T: struct
        {
            return D3D11ConstantBuffer.Create(_graphicsDevice, constants);
        }

        public IDepthStencilState CreateDepthStencilState()
        {
            return new D3D11DepthStencilState(_graphicsDevice);
        }

        public IPipeline CreatePipeline(Shader vertexShader, Shader pixelShader, IInputLayout inputLayout,
            IBlendState blendState, IDepthStencilState depthStencilState, IRasterizerState rasterizerState,
            Viewport viewport, PrimitiveTopology primitiveTopology)
        {
            return new D3D11Pipeline(vertexShader, pixelShader, inputLayout, blendState, depthStencilState, rasterizerState, viewport, primitiveTopology);
        }

        public IRasterizerState CreateRasterizerState(CullMode cullMode, FillMode fillMode, 
            bool isDepthEnabled, bool isScissorEnabled, bool isMultiSampleEnabled, bool isAntialiasedLineEnabled)
        {
            return new D3D11RasterizerState(_graphicsDevice, cullMode, fillMode, isDepthEnabled, isScissorEnabled, isMultiSampleEnabled, isAntialiasedLineEnabled);
        }

        public ISampler CreateSampler(TextureAddressMode addressModeU, TextureAddressMode addressModeV, Filter filter, ComparisonFunction comparisonFunction)
        {
            return new D3D11Sampler(_graphicsDevice, addressModeU, addressModeV, filter, comparisonFunction);
        }

        public Shader CreateShaderFromFile(ShaderStage shaderStage, string filePath, VertexType vertexType, IEnumerable<(string, string)> macros)
        {
            var shader = new D3D11Shader(_graphicsDevice, this);
            foreach (var (macroName, macroValue) in macros.ToList())
            {
                shader.AddMacro(macroName, macroValue);
            }
            shader.CompileAsync(shaderStage, filePath + ".hlsl", vertexType);
            return shader;
        }

        public ISwapChain CreateSwapchain(SwapChainDescriptor swapChainDescriptor)
        {
            return new D3D11SwapChain(_graphicsDevice, swapChainDescriptor);
        }

        public ITextureFactory CreateTextureFactory()
        {
            return new D3D11TextureFactory(_graphicsDevice);
        }

        public IVertexBuffer CreateVertexBuffer<T>(T[] vertices) where T: struct
        {
            return D3D11VertexBuffer.Create(_graphicsDevice, ref vertices);
        }

        public IIndexBuffer CreateIndexBuffer<T>(T[] indices) where T : struct
        {
            return D3D11IndexBuffer.Create(_graphicsDevice, ref indices);
        }

        internal D3D11InputLayout CreateInputLayout(VertexType vertexType, byte[] shaderByteCode)
        {
            var attributes = new List<D3D11VertexAttribute>();

            switch (vertexType)
            {
                case VertexType.Position:
                    attributes.Add(new D3D11VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    break;
                case VertexType.PositionColor:
                    attributes.Add(new D3D11VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    attributes.Add(new D3D11VertexAttribute("COLOR", 0, 0, 12, Format.R32G32B32A32Float));
                    break;
                case VertexType.PositionTexture:
                    attributes.Add(new D3D11VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    attributes.Add(new D3D11VertexAttribute("TEXCOORD", 0, 0, 12, Format.R32G32Float));
                    break;
                case VertexType.PositionTextureNormalTangent:
                    attributes.Add(new D3D11VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    attributes.Add(new D3D11VertexAttribute("TEXCOORD", 0, 0, 12, Format.R32G32Float));
                    attributes.Add(new D3D11VertexAttribute("NORMAL", 0, 0, 20, Format.R32G32B32Float));
                    attributes.Add(new D3D11VertexAttribute("TANGENT", 0, 0, 32, Format.R32G32B32Float));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vertexType), vertexType, null);
            }

            return new D3D11InputLayout(_graphicsDevice, shaderByteCode, attributes);
        }

        public void Dispose()
        {
            _graphicsDevice?.Dispose();
        }

        public D3D11GraphicsFactory(ILogger logger, HardwareOptions hardwareOptions)
        {
            _logger = logger;
            _logger.Debug("Creating D3D11 Device...");
            var deviceType = hardwareOptions.UseHardwareDevice ? DeviceType.Hardware : DeviceType.Reference;
            var isDebug = hardwareOptions.IsDebug;
            _logger.Debug($"- DeviceType: {deviceType} IsDebug: {isDebug}");
            _graphicsDevice = new D3D11GraphicsDevice(deviceType, isDebug);
            _logger.Debug("Creating D3D11 Device...Done");
        }

    }
}