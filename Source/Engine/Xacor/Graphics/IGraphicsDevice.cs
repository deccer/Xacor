using System;

namespace Xacor.Graphics;

public interface IGraphicsDevice
{
    GraphicsBuffer CreateBuffer<T>(
        ReadOnlySpan<T> initialData) where T : unmanaged;

    GraphicsPipeline CreateGraphicsPipeline(
        string vertexShaderSource,
        string fragmentShaderSource);
    
    CommandRecorder CreateCommandRecorder();
    
    void Submit(CommandRecorder recorder);
    
    void RenderFrame(float deltaTime);
}