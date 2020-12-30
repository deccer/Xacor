using System;
using Xacor.Mathematics;

namespace Xacor.Graphics.Api
{
    public interface IPipeline : IDisposable
    {
        PrimitiveTopology PrimitiveTopology { get; }

        Shader VertexShader { get; }

        Shader PixelShader { get; }

        IInputLayout InputLayout { get; }

        IRasterizerState RasterizerState { get; }

        IDepthStencilState DepthStencilState { get; }

        IBlendState BlendState { get; }

        Viewport Viewport { get; }
    }
}
