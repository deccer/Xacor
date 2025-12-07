using Xacor.Graphics;

namespace Xacor.Ecs;

public class RenderSystem
{
    private readonly Scene _scene;
    private readonly IGraphicsDeviceProvider _graphicsDeviceProvider;
    private IGraphicsDevice? _graphicsDevice;

    public RenderSystem(
        Scene scene, 
        IGraphicsDeviceProvider graphicsDeviceProvider)
    {
        _scene = scene;
        _graphicsDeviceProvider = graphicsDeviceProvider;
    }
    
    public void Run(float deltaTime)
    {
        if (_graphicsDevice == null)
        {
            _graphicsDevice = _graphicsDeviceProvider.GetGraphicsDevice();
        }

        var recording = new CommandRecorder();
        recording.BeginRenderPass(0, 0);
        recording.EndRenderPass();
        
        _graphicsDevice.Submit(recording);
        _graphicsDevice.RenderFrame(deltaTime);
    }

    public void NotifyResolutionChange(int newWidth, int newHeight)
    {
        //TODO(deccer) let the graphicsdevice know resolution has changed during Run()
    }
}