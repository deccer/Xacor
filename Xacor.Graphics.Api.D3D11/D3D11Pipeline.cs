﻿using Xacor.Mathematics;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11Pipeline : IPipeline
    {
        public PrimitiveTopology PrimitiveTopology { get; }

        public Shader VertexShader { get; }

        public Shader PixelShader { get; }

        public IInputLayout InputLayout { get; }

        public IRasterizerState RasterizerState { get; }

        public IDepthStencilState DepthStencilState { get; }

        public IBlendState BlendState { get; }

        public Viewport Viewport { get; }

        public void Dispose()
        {
            VertexShader?.Dispose();
            PixelShader?.Dispose();
            RasterizerState?.Dispose();
            DepthStencilState?.Dispose();
            BlendState?.Dispose();
        }

        public D3D11Pipeline(Shader vertexShader, Shader pixelShader, IInputLayout inputLayout,
            IBlendState blendState, IDepthStencilState depthStencilState, IRasterizerState rasterizerState,
            Viewport viewport, PrimitiveTopology primitiveTopology)
        {
            VertexShader = vertexShader;
            PixelShader = pixelShader;
            InputLayout = inputLayout;
            BlendState = blendState;
            DepthStencilState = depthStencilState;
            RasterizerState = rasterizerState;
            Viewport = viewport;
            PrimitiveTopology = primitiveTopology;
        }
    }
}