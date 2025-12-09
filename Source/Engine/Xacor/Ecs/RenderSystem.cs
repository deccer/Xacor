using System;
using Xacor.Graphics;

namespace Xacor.Ecs;

public struct Vertex
{
    public float X, Y, Z;
    public float R, G, B, A;

    public Vertex(float x, float y, float z, float r, float g, float b, float a)
    {
        X = x; Y = y; Z = z;
        R = r; G = g; B = b; A = a;
    }
}

public class RenderSystem
{
    private readonly Scene _scene;
    private readonly Lazy<IGraphicsDevice> _graphicsDeviceProvider;
    private readonly CommandRecorder _commandRecorder;
    private IGraphicsDevice? _graphicsDevice;

    private GraphicsPipeline _pipeline;
    private GraphicsBuffer _vertexBuffer;
    private GraphicsBuffer _indexBuffer;

    public RenderSystem(
        Scene scene, 
        Lazy<IGraphicsDevice> graphicsDeviceProvider,
        CommandRecorder commandRecorder)
    {
        _scene = scene;
        _graphicsDeviceProvider = graphicsDeviceProvider;
        _commandRecorder = commandRecorder;
    }
    
    public void Run(float deltaTime)
    {
        //_commandRecorder ??= _graphicsDevice.CreateCommandRecorder();

        _commandRecorder.Reset();
        _commandRecorder.BeginRenderPass(0, 0);
        _commandRecorder.SetViewport(0, 0, 1680, 720, -1.0f, 1.0f);
        _commandRecorder.BindPipeline(_pipeline.Handle);
        _commandRecorder.BindVertexBuffer(_vertexBuffer.Handle);
        _commandRecorder.Draw(3);
        _commandRecorder.EndRenderPass();
        
        _graphicsDevice.Submit(_commandRecorder);
        _graphicsDevice.RenderFrame(deltaTime);
    }

    public void NotifyResolutionChange(int newWidth, int newHeight)
    {
        //TODO(deccer) let the graphicsdevice know resolution has changed during Run()
    }

    public void Initialize()
    {
        _graphicsDevice = _graphicsDeviceProvider.Value;
        
        var vertexShaderSource = """
                                 #version 460 core
                                 
                                 layout(location = 0) in vec3 aPos;
                                 layout(location = 1) in vec4 aColor;
                                 out vec4 fColor;
                                 
                                 void main() { 
                                     gl_Position = vec4(aPos, 1.0); 
                                     fColor = aColor; 
                                 }
                                 """;
        var fragmentShaderSource = """
                                   #version 460 core
                                   
                                   in vec4 fColor;
                                   out vec4 FragColor;
                                   
                                   void main() { 
                                       FragColor = fColor; 
                                   }
                                   """;
        _pipeline = _graphicsDevice.CreateGraphicsPipeline(vertexShaderSource, fragmentShaderSource);
        
        var vertices = new Vertex[]
        {
            new(-0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f, 1.0f), // Bottom Left, Red
            new( 0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f, 1.0f), // Bottom Right, Green
            new( 0.0f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f, 1.0f)  // Top, Blue
        };

        _vertexBuffer = _graphicsDevice.CreateBuffer(vertices);
    }
}