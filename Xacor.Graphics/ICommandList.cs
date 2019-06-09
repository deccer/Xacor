namespace Xacor.Graphics
{
    public interface ICommandList
    {
        void Begin(string passName, IPipeline pipeline);

        void Draw(int vertexCount);

        void DrawIndexed(int indexCount, int indexOffset, int vertexOffset);

        void End();

        void SetBlendState(IBlendState blendState);

        void SetConstantBuffer(IConstantBuffer buffer);

        void SetDepthStencilState(IDepthStencilState depthStencilState);

        void SetIndexBuffer(IIndexBuffer indexBuffer);

        void SetPixelShader(IShader pixelShader);

        void SetRasterizerState(IRasterizerState rasterizerState);

        void SetVertexBuffer(IVertexBuffer vertexBuffer);

        void SetVertexShader(IShader vertexShader);

        void Submit();
    }
}