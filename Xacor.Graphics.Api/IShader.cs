using System;

namespace Xacor.Graphics.Api
{
    public interface IShader : IDisposable
    {
        IInputLayout InputLayout { get; }

        void CompileAsync(ShaderStage shaderStage, string filePath, VertexType vertexType);
    }
}