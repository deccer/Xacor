namespace Xacor.Graphics;

public enum CommandType : byte
{
    BindPipeline,
    BindVertexBuffer,
    BindIndexBuffer,
    BindDescriptorSet, //TODO(deccer) this is vulkan terminology, but perhaps we can cook something with gl to resemble that, its a map of what resource to bind only anyway
    SetViewport,
    SetScissor,
    Draw,
    DrawIndexed,
    DrawIndirect,
    Dispatch,
    CopyBuffer,
    CopyTexture,
    PipelineBarrier,
    BeginRenderPass,
    EndRenderPass,
    PushConstants,
}