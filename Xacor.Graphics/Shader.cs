using System.Collections.Generic;
using System.Diagnostics;

namespace Xacor.Graphics
{
    public abstract class Shader : IShader
    {
        public IInputLayout InputLayout { get; protected set; }

        protected Dictionary<string, string> Macros { get; }

        public void AddMacro(string name, string value)
        {
            if (!Macros.TryAdd(name, value))
            {
                Debug.WriteLine($"Key already exists {name}.");
            }
        }

        protected abstract void CompileInternal(ShaderStage shaderStage, string filePath, VertexType vertexType);

        public void CompileAsync(ShaderStage shaderStage, string filePath, VertexType vertexType)
        {
            CompileInternal(shaderStage, filePath, vertexType);
        }

        public virtual void Dispose()
        {
        }

        protected Shader()
        {
            Macros = new Dictionary<string, string>();
        }
    }
}