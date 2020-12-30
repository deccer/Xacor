using System;

namespace Xacor.Graphics.Api
{
    public interface IShader : IDisposable
    {
        IInputLayout InputLayout { get; }

        void CompileFile(ShaderStage shaderStage, string filePath, VertexType vertexType);

        void CompileString(ShaderStage shaderStage, string shaderText, VertexType vertexType);
    }
}
