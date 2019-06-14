namespace Xacor.Graphics.DX11
{
    internal class DX11Pipeline : IPipeline
    {
        public PrimitiveTopology PrimitiveTopology { get; }

        public Shader VertexShader { get; }

        public Shader PixelShader { get; }

        public IInputLayout InputLayout { get; }

        public IRasterizerState RasterizerState { get; }

        public IDepthStencilState DepthStencilState { get; }

        public IBlendState BlendState { get; }

        public Viewport Viewport { get; }

        public DX11Pipeline(Shader vertexShader, Shader pixelShader, IInputLayout inputLayout,
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