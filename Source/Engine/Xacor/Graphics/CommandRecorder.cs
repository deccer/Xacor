using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xacor.Graphics.Commands;

namespace Xacor.Graphics;

//TODO(deccer) replace with ReadOnlySpan<byte>
public unsafe struct DetachedBuffer
{
    public byte* Data;
    public int Length;
}

public unsafe class CommandRecorder : IDisposable
{
    private const int DefaultCapacity = 64 * 1024;
    
    private byte* _buffer;
    private int _capacity;
    private int _position;
    private bool _disposed;
    
    //TODO(deccer) (re)use this instead of DetachedBuffer??
    public ReadOnlySpan<byte> RecordedCommands => new(_buffer, _position);
    
    public int CommandCount { get; private set; }

    public CommandRecorder(int initialCapacity = DefaultCapacity)
    {
        _capacity = initialCapacity;
        _buffer = (byte*)NativeMemory.AllocZeroed((nuint)_capacity);
        _position = 0;
    }
    
    ~CommandRecorder() => Dispose();

    /// <summary>
    /// Transfers ownership of the internal buffer to the caller.
    /// The recorder is left in an unusable state until Reset(true) is called.
    /// </summary>
    public DetachedBuffer DetachBuffer()
    {
        var ptr = _buffer;
        var len = _position;
        
        _buffer = null; // We no longer own this
        _position = 0;
        _capacity = 0;

        return new DetachedBuffer { Data = ptr, Length = len };
    }

    public void Reset(bool allocateFresh = false)
    {
        _position = 0;
        CommandCount = 0;
        
        if (allocateFresh && _buffer == null)
        {
            _capacity = 64 * 1024;
            _buffer = (byte*)NativeMemory.AllocZeroed((nuint)_capacity);
        }
    }

    public void BindPipeline(uint pipelineHandle)
    {
        ref var cmd = ref WriteCommand<BindPipelineCommand>(CommandType.BindPipeline);
        cmd.PipelineHandle = pipelineHandle;
    }

    public void BindVertexBuffer(
        uint bufferHandle, 
        uint binding = 0, 
        ulong offset = 0)
    {
        ref var cmd = ref WriteCommand<BindVertexBufferCommand>(CommandType.BindVertexBuffer);
        cmd.BufferHandle = bufferHandle;
        cmd.Binding = binding;
        cmd.Offset = offset;
    }

    public void BindIndexBuffer(
        uint bufferHandle, 
        IndexType indexType, 
        ulong offset = 0)
    {
        ref var cmd = ref WriteCommand<BindIndexBufferCommand>(CommandType.BindIndexBuffer);
        cmd.BufferHandle = bufferHandle;
        cmd.Offset = offset;
        cmd.IndexType = indexType;
    }

    public void SetViewport(
        float x, 
        float y, 
        float width, 
        float height, 
        float minDepth = 0f, 
        float maxDepth = 1f)
    {
        ref var cmd = ref WriteCommand<SetViewportCommand>(CommandType.SetViewport);
        cmd.X = x;
        cmd.Y = y;
        cmd.Width = width;
        cmd.Height = height;
        cmd.MinDepth = minDepth;
        cmd.MaxDepth = maxDepth;
    }

    public void SetScissor(
        int x, 
        int y, 
        int width, 
        int height)
    {
        ref var cmd = ref WriteCommand<SetScissorCommand>(CommandType.SetScissor);
        cmd.X = x;
        cmd.Y = y;
        cmd.Width = width;
        cmd.Height = height;
    }

    public void Draw(
        uint vertexCount, 
        uint instanceCount = 1, 
        uint firstVertex = 0, 
        uint firstInstance = 0)
    {
        ref var cmd = ref WriteCommand<DrawCommand>(CommandType.Draw);
        cmd.VertexCount = vertexCount;
        cmd.InstanceCount = instanceCount;
        cmd.FirstVertex = firstVertex;
        cmd.FirstInstance = firstInstance;
    }

    public void DrawIndexed(
        uint indexCount, 
        uint instanceCount = 1, 
        uint firstIndex = 0, 
        int vertexOffset = 0, 
        uint firstInstance = 0)
    {
        ref var cmd = ref WriteCommand<DrawIndexedCommand>(CommandType.DrawIndexed);
        cmd.IndexCount = indexCount;
        cmd.InstanceCount = instanceCount;
        cmd.FirstIndex = firstIndex;
        cmd.VertexOffset = vertexOffset;
        cmd.FirstInstance = firstInstance;
    }

    public void BeginRenderPass(
        uint framebufferHandle, 
        uint renderPassHandle,
        float clearR = 0, 
        float clearG = 0, 
        float clearB = 0, 
        float clearA = 1,
        float clearDepth = 1f, 
        uint clearStencil = 0)
    {
        ref var cmd = ref WriteCommand<BeginRenderPassCommand>(CommandType.BeginRenderPass);
        cmd.FramebufferHandle = framebufferHandle;
        cmd.RenderPassHandle = renderPassHandle;
        cmd.ClearR = clearR;
        cmd.ClearG = clearG;
        cmd.ClearB = clearB;
        cmd.ClearA = clearA;
        cmd.ClearDepth = clearDepth;
        cmd.ClearStencil = clearStencil;
    }

    public void EndRenderPass()
    {
        WriteCommandHeaderOnly(CommandType.EndRenderPass);
    }
    
    //TODO(deccer) rename to reflect ubo or just use pushconstants as if its vulkan, its similar to ubo/gluniform anyway
    public void SetPushConstants<T>(
        uint offset, 
        T data) where T : unmanaged
    {
        var dataSize = sizeof(T);
        
        ref var cmd = ref WriteCommandWithPayload<SetPushConstantsCommand>(
            CommandType.PushConstants, 
            dataSize, 
            out var payloadPtr
        );
        
        cmd.Offset = offset;
        cmd.SizeInBytes = (uint)dataSize;

        Unsafe.Write(payloadPtr, data);
    }
    
    public void SetPushConstants<T>(
        uint offset, 
        ReadOnlySpan<T> data) where T : unmanaged
    {
        var dataSize = sizeof(T) * data.Length;
        
        ref var cmd = ref WriteCommandWithPayload<SetPushConstantsCommand>(
            CommandType.PushConstants, 
            dataSize, 
            out var payloadPtr
        );

        cmd.Offset = offset;
        cmd.SizeInBytes = (uint)dataSize;

        data.CopyTo(new Span<T>(payloadPtr, data.Length));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref T WriteCommandWithPayload<T>(
        CommandType type, 
        int payloadSize, 
        out void* payloadDataStart) where T : unmanaged
    {
        var commandStructSize = sizeof(T);
        var totalSize = sizeof(CommandHeader) + commandStructSize + payloadSize; // Header + Struct + Variable Data
        
        EnsureCapacity(totalSize);

        var header = (CommandHeader*)(_buffer + _position);
        header->Type = type;
        header->PayloadSize = (ushort)(commandStructSize + payloadSize);

        var commandPtr = _buffer + _position + sizeof(CommandHeader);
        
        payloadDataStart = (byte*)commandPtr + commandStructSize;
        
        _position += totalSize;
        CommandCount++;

        return ref Unsafe.AsRef<T>(commandPtr);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref T WriteCommand<T>(CommandType type) where T : unmanaged
    {
        var payloadSize = sizeof(T);
        var totalSize = sizeof(CommandHeader) + payloadSize;
        
        EnsureCapacity(totalSize);

        var header = (CommandHeader*)(_buffer + _position);
        header->Type = type;
        header->PayloadSize = (ushort)payloadSize;

        var payloadPtr = _buffer + _position + sizeof(CommandHeader);
        
        _position += totalSize;
        CommandCount++;

        return ref Unsafe.AsRef<T>(payloadPtr);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteCommandHeaderOnly(CommandType type)
    {
        EnsureCapacity(sizeof(CommandHeader));

        var header = (CommandHeader*)(_buffer + _position);
        header->Type = type;
        header->PayloadSize = 0;

        _position += sizeof(CommandHeader);
        CommandCount++;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void EnsureCapacity(int additionalBytes)
    {
        if (_position + additionalBytes <= _capacity)
        {
            return;
        }

        var newCapacity = Math.Max(_capacity * 2, _position + additionalBytes);
        var newBuffer = (byte*)NativeMemory.Realloc(_buffer, (nuint)newCapacity);
        _buffer = newBuffer;
        _capacity = newCapacity;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        _disposed = true;
        
        if (_buffer != null)
        {
            NativeMemory.Free(_buffer);
            _buffer = null;
        }
        
        GC.SuppressFinalize(this);
    }
}