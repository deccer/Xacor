namespace Xacor.Graphics.Api.D3D12
{
    internal class D3D12Shader : Shader
    {
        protected override void CompileFileInternal(ShaderStage shaderStage, string filePath, VertexType vertexType)
        {
            throw new System.NotImplementedException();
        }

        protected override void CompileStringInternal(ShaderStage shaderStage, string filePath, VertexType vertexType)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}