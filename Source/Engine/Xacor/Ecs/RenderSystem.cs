using Xacor.Graphics;

namespace Xacor.Ecs;

public class RenderSystem
{
    private readonly Scene _scene;
    private readonly IGraphicsDevice _graphicsDevice;
    private readonly CommandRecorder _commandRecorder;

    private GraphicsPipeline? _pipeline;
    private GraphicsBuffer? _vertexBuffer;
    private GraphicsBuffer? _indexBuffer;

    public RenderSystem(
        Scene scene, 
        IGraphicsDevice graphicsDevice,
        CommandRecorder commandRecorder)
    {
        _scene = scene;
        _graphicsDevice = graphicsDevice;
        _commandRecorder = commandRecorder;

        _pipeline = null;
        _vertexBuffer = null;
        _indexBuffer = null;
    }
    
    public void Run(float deltaTime)
    {
        _commandRecorder.Reset();
        //#005C53
        _commandRecorder.BeginRenderPass(
            0, 
            0,
            0.0f,
            0.3607843137254902f,
            0.3254901960784314f);
        _commandRecorder.SetViewport(0, 0, 1680, 720, -1.0f, 1.0f);
        _commandRecorder.BindPipeline(_pipeline!.Handle);
        _commandRecorder.BindVertexBuffer(_vertexBuffer!.Handle);
        _commandRecorder.BindIndexBuffer(_indexBuffer!.Handle, IndexType.UInt32);
        _commandRecorder.DrawIndexed(3);
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
        // #042940
        var vertices = new TVertexPositionColor[]
        {
            new(-0.5f, -0.5f, 0.0f,  0.01568627450980392f, 0.1607843137254902f, 0.25098039215686274f, 1.0f), // Bottom Left, Red
            new( 0.5f, -0.5f, 0.0f,  0.6235294117647059f, 0.7568627450980392f, 0.19215686274509805f, 1.0f), // Bottom Right, Green
            new( 0.0f,  0.5f, 0.0f,  0.8392156862745098f, 0.8352941176470589f, 0.5568627450980392f, 1.0f)  // Top, Blue
        };

        _vertexBuffer = _graphicsDevice.CreateBuffer(vertices);
        _indexBuffer = _graphicsDevice.CreateBuffer([0u, 1u, 2u]);
    }
}