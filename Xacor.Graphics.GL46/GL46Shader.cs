using System.Diagnostics;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Xacor.Graphics.GL46
{
    internal class GL46Shader : Shader
    {
        private readonly GL46GraphicsFactory _graphicsFactory;
        private int _nativeShader;

        public static implicit operator int(GL46Shader shader)
        {
            return shader._nativeShader;
        }

        protected override void CompileInternal(ShaderStage shaderStage, string filePath, VertexType vertexType)
        {
            _nativeShader = OpenTK.Graphics.OpenGL4.GL.CreateShaderProgram(shaderStage.ToOpenTK(), 1, new[] { File.ReadAllText(filePath) });
            ValidateProgram(filePath);

            if (shaderStage == ShaderStage.Vertex && vertexType != VertexType.Unknown)
            {
                InputLayout = _graphicsFactory.CreateInputLayout(vertexType);
            }
        }

        public override void Dispose()
        {
            OpenTK.Graphics.OpenGL4.GL.DeleteProgram(_nativeShader);

            base.Dispose();
        }

        public GL46Shader(GL46GraphicsFactory graphicsFactory)
        {
            _graphicsFactory = graphicsFactory;
        }

        private void ValidateProgram(string filePath)
        {
            OpenTK.Graphics.OpenGL4.GL.ProgramParameter(_nativeShader, ProgramParameterName.ProgramSeparable, 1);
            OpenTK.Graphics.OpenGL4.GL.GetProgram(_nativeShader, GetProgramParameterName.LinkStatus, out var compiled);
            if (compiled == 0)
            {
                OpenTK.Graphics.OpenGL4.GL.GetProgramInfoLog(_nativeShader, out var programLog);
                OpenTK.Graphics.OpenGL4.GL.DeleteShader(_nativeShader);
                Debug.WriteLine($"GL46Shader contains errors:\n\n{programLog}");
            }
        }
    }
}