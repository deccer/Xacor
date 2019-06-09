using System.Threading.Tasks;

namespace Xacor.Graphics.DX12
{
    internal class DX12Shader : Shader
    {
        protected override Task CompileInternalAsync(ShaderStage shaderStage, string filePath, VertexType vertexType)
        {
            throw new System.NotImplementedException();
        }
    }
}