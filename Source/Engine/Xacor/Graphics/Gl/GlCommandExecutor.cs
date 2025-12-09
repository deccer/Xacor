using System;
using System.Runtime.CompilerServices;
using Silk.NET.OpenGL;
using Xacor.Graphics.Commands;

namespace Xacor.Graphics.Gl;

public unsafe class GlCommandExecutor : ICommandExecutor
{
    private readonly GL _gl;
    private readonly uint _defaultInputLayout;

    public GlCommandExecutor(GL gl)
    {
        _gl = gl;
        _defaultInputLayout = _gl.CreateVertexArray();
        
        _gl.EnableVertexArrayAttrib(_defaultInputLayout, 0);
        _gl.VertexArrayAttribFormat(_defaultInputLayout, 0, 3, VertexAttribType.Float, false, 0);
        _gl.VertexArrayAttribBinding(_defaultInputLayout, 0, 0); // Attrib 0 takes data from Binding 0

        // Enable Attribute 1 (Color)
        _gl.EnableVertexArrayAttrib(_defaultInputLayout, 1);
        _gl.VertexArrayAttribFormat(_defaultInputLayout, 1, 4, VertexAttribType.Float, false, 12); // Offset 12 (3*float)
        _gl.VertexArrayAttribBinding(_defaultInputLayout, 1, 0);
    }

    public void Execute(ReadOnlySpan<byte> commandBuffer)
    {
        var position = 0;

        fixed (byte* bufferPtr = commandBuffer)
        {
            while (position < commandBuffer.Length)
            {
                var header = (CommandHeader*)(bufferPtr + position);
                var payloadPtr = bufferPtr + position + sizeof(CommandHeader);

                switch (header->Type)
                {
                    case CommandType.BindPipeline:
                        ExecuteBindPipeline(Unsafe.AsRef<BindPipelineCommand>(payloadPtr));
                        break;

                    case CommandType.BindVertexBuffer:
                        ExecuteBindVertexBuffer(Unsafe.AsRef<BindVertexBufferCommand>(payloadPtr));
                        break;

                    case CommandType.BindIndexBuffer:
                        ExecuteBindIndexBuffer(Unsafe.AsRef<BindIndexBufferCommand>(payloadPtr));
                        break;

                    case CommandType.SetViewport:
                        ExecuteSetViewport(Unsafe.AsRef<SetViewportCommand>(payloadPtr));
                        break;

                    case CommandType.SetScissor:
                        ExecuteSetScissor(Unsafe.AsRef<SetScissorCommand>(payloadPtr));
                        break;

                    case CommandType.Draw:
                        ExecuteDraw(Unsafe.AsRef<DrawCommand>(payloadPtr));
                        break;

                    case CommandType.DrawIndexed:
                        ExecuteDrawIndexed(Unsafe.AsRef<DrawIndexedCommand>(payloadPtr));
                        break;

                    case CommandType.BeginRenderPass:
                        ExecuteBeginRenderPass(Unsafe.AsRef<BeginRenderPassCommand>(payloadPtr));
                        break;

                    case CommandType.EndRenderPass:
                        ExecuteEndRenderPass();
                        break;
                    
                    case CommandType.PushConstants:
                        // Read the header struct
                        ref var pushCmd = ref Unsafe.AsRef<SetPushConstantsCommand>(payloadPtr);
                        
                        // Calculate pointer to the variable data
                        // It is located at: payloadPtr + sizeof(SetPushConstantsCommand)
                        var dataPtr = payloadPtr + sizeof(SetPushConstantsCommand);
                        
                        ExecutePushConstants(pushCmd, dataPtr);
                        break;

                    default:
                        throw new InvalidOperationException($"Unknown command type: {header->Type}");
                }

                position += sizeof(CommandHeader) + header->PayloadSize;
            }
        }
    }

    private void ExecutePushConstants(in SetPushConstantsCommand cmd, void* data)
    {
        _gl.NamedBufferSubData(cmd.UniformBufferHandle, (nint)cmd.Offset, cmd.SizeInBytes, data);
    }

    private void ExecuteBindPipeline(in BindPipelineCommand cmd)
    {
        _gl.UseProgram(cmd.PipelineHandle);
        _gl.BindVertexArray(_defaultInputLayout);
    }

    private void ExecuteBindVertexBuffer(in BindVertexBufferCommand cmd)
    {
        var stride = 7u * sizeof(float);
        _gl.VertexArrayVertexBuffer(
            _defaultInputLayout, 
            cmd.Binding, 
            cmd.BufferHandle, 
            (nint)cmd.Offset, 
            stride);
    }

    private void ExecuteBindIndexBuffer(in BindIndexBufferCommand cmd)
    {
        _gl.VertexArrayElementBuffer(_defaultInputLayout, cmd.BufferHandle);
    }

    private void ExecuteSetViewport(in SetViewportCommand cmd)
    {
        _gl.Viewport((int)cmd.X, (int)cmd.Y, (uint)cmd.Width, (uint)cmd.Height);
        _gl.DepthRange(cmd.MinDepth, cmd.MaxDepth);
    }

    private void ExecuteSetScissor(in SetScissorCommand cmd)
    {
        _gl.Scissor(cmd.X, cmd.Y, (uint)cmd.Width, (uint)cmd.Height);
    }

    private void ExecuteDraw(in DrawCommand cmd)
    {
        if (cmd.InstanceCount > 1)
        {
            _gl.DrawArraysInstancedBaseInstance(GLEnum.Triangles, (int)cmd.FirstVertex, cmd.VertexCount, cmd.InstanceCount, cmd.FirstInstance);
        }
        else
        {
            _gl.DrawArrays(GLEnum.Triangles, (int)cmd.FirstVertex, cmd.VertexCount);
        }
    }

    private void ExecuteDrawIndexed(in DrawIndexedCommand cmd)
    {
        var indexType = GLEnum.UnsignedInt;
        var indexSize = sizeof(uint);
        
        _gl.DrawElementsInstancedBaseVertexBaseInstance(
            GLEnum.Triangles, cmd.IndexCount,
             indexType, (void*)(cmd.FirstIndex * indexSize), cmd.InstanceCount, 
             cmd.VertexOffset, cmd.FirstInstance);
    }

    private void ExecuteBeginRenderPass(in BeginRenderPassCommand cmd)
    {
        _gl.BindFramebuffer(FramebufferTarget.Framebuffer, cmd.FramebufferHandle);
        //TODO(deccer) clear color and depth
    }

    private void ExecuteEndRenderPass()
    {
        //TODO(deccer) nop for opengl perhaps or can we resolve MSAA here if MSAA is enabled?
    }
}