namespace Xacor.Graphics
{
    public interface IPipeline
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