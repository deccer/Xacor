using System;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Xacor.Graphics.Gl;

internal class GlGraphicsDevice : GraphicsDevice, IGraphicsDevice
{
    private readonly GL _gl;
    private readonly ICommandExecutor _commandExecutor;
    
    public GlGraphicsDevice(IWindow window)
    {
        window.MakeCurrent();
        
        _gl = window.CreateOpenGL();
        _commandExecutor = new GlCommandExecutor(_gl);
        
        _gl.Enable(EnableCap.FramebufferSrgb);
        _gl.Enable(EnableCap.CullFace);
        _gl.CullFace(TriangleFace.Back);
        _gl.FrontFace(FrontFaceDirection.Ccw);
    }

    public unsafe GraphicsBuffer CreateBuffer<T>(
        ReadOnlySpan<T> initialData) where T : unmanaged
    {
        var sizeInBytes = (uint)(initialData.Length * sizeof(T));
        var bufferHandle = _gl.CreateBuffer();
        fixed (void* dataPtr = initialData)
        {
            _gl.NamedBufferStorage(bufferHandle, sizeInBytes, dataPtr, BufferStorageMask.None);
        }
        
        return new GraphicsBuffer(bufferHandle, sizeInBytes);
    }

    public GraphicsPipeline CreateGraphicsPipeline(
        string vertexShaderSource, 
        string fragmentShaderSource)
    {
        var vertexShader = CreateShader(ShaderType.VertexShader, vertexShaderSource);
        var fragmentShader = CreateShader(ShaderType.FragmentShader, fragmentShaderSource);
        
        var programHandle = _gl.CreateProgram();
        _gl.AttachShader(programHandle, vertexShader);
        _gl.AttachShader(programHandle, fragmentShader);
        _gl.LinkProgram(programHandle);

        var linkInfoLog = _gl.GetProgramInfoLog(programHandle);
        if (!string.IsNullOrEmpty(linkInfoLog))
        {
            _gl.DetachShader(programHandle, vertexShader);
            _gl.DetachShader(programHandle, fragmentShader);
            _gl.DeleteShader(vertexShader);
            _gl.DeleteShader(fragmentShader);
            _gl.DeleteProgram(programHandle);
            throw new InvalidOperationException(linkInfoLog);
        }
        
        _gl.DeleteShader(vertexShader);
        _gl.DeleteShader(fragmentShader);

        return new GraphicsPipeline(programHandle);
    }

    private uint CreateShader(ShaderType shaderType, string shaderSource)
    {
        var shader = _gl.CreateShader(shaderType);
        _gl.ShaderSource(shader, shaderSource);
        _gl.CompileShader(shader);
        var shaderInfoLog = _gl.GetShaderInfoLog(shader);
        if (!string.IsNullOrEmpty(shaderInfoLog))
        {
            _gl.DeleteShader(shader);
            throw new InvalidOperationException(shaderInfoLog);
        }

        return shader;
    }

    public unsafe void RenderFrame(float deltaTime)
    {
        //TODO(deccer): process resource deletions here
        
        while (SubmissionQueue.TryDequeue(out var submission))
        {
            _commandExecutor.Execute(new ReadOnlySpan<byte>((void*)submission.BufferStart, submission.Length));
            
            submission.OnComplete(submission.BufferStart);
        }
    }
}