using System.Threading.Tasks;

namespace Xacor.Graphics.VK
{
    internal class VKShader : Shader
    {
        protected override Task CompileInternalAsync(ShaderStage shaderStage, string filePath, VertexType vertexType)
        {
            throw new System.NotImplementedException();
        }
    }
}