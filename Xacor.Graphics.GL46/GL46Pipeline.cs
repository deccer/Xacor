using OpenTK.Graphics.OpenGL4;

namespace Xacor.Graphics.GL46
{
    internal class GL46Pipeline : IPipeline
    {
        public PrimitiveTopology PrimitiveTopology { get; }

        public Shader VertexShader { get; }

        public Shader PixelShader { get; }

        public IInputLayout InputLayout { get; }

        public IRasterizerState RasterizerState { get; }

        public IDepthStencilState DepthStencilState { get; }

        public IBlendState BlendState { get; }

        public Viewport Viewport { get; }

        private readonly int _nativePipeline;

        public static implicit operator int(GL46Pipeline pipeline)
        {
            return pipeline._nativePipeline;
        }

        public GL46Pipeline(Shader vertexShader, Shader pixelShader, IInputLayout inputLayout)
        {
            VertexShader = vertexShader;
            PixelShader = pixelShader;
            InputLayout = inputLayout;

            OpenTK.Graphics.OpenGL4.GL.CreateProgramPipelines(1, out _nativePipeline);
            OpenTK.Graphics.OpenGL4.GL.UseProgramStages(_nativePipeline, ProgramStageMask.VertexShaderBit, (GL46Shader)vertexShader);
            OpenTK.Graphics.OpenGL4.GL.UseProgramStages(_nativePipeline, ProgramStageMask.FragmentShaderBit, (GL46Shader)pixelShader);
        }

        public void Dispose()
        {
            RasterizerState?.Dispose();
            DepthStencilState?.Dispose();
            BlendState?.Dispose();

            OpenTK.Graphics.OpenGL4.GL.DeleteProgramPipeline(_nativePipeline);
        }
    }
}
