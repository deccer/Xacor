using System.Threading.Tasks;

namespace Xacor.Graphics
{
    public interface IShader
    {
        IInputLayout InputLayout { get; }

        Task CompileAsync(ShaderStage shaderStage, string filePath, VertexType vertexType);
    }
}
