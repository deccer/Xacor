using System;

namespace Xacor.Graphics
{
    public interface IShader : IDisposable
    {
        IInputLayout InputLayout { get; }

        void CompileAsync(ShaderStage shaderStage, string filePath, VertexType vertexType);
    }
}