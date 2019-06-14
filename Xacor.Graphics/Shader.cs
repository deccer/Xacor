using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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

        protected Shader()
        {
            Macros = new Dictionary<string, string>();
        }

        protected abstract void CompileInternal(ShaderStage shaderStage, string filePath, VertexType vertexType);

        public void CompileAsync(ShaderStage shaderStage, string filePath, VertexType vertexType)
        {
            CompileInternal(shaderStage, filePath, vertexType);
        }
    }
}