using Silk.NET.Input.Sdl;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Sdl;
using Xacor.Ecs;
using Xacor.Game;
using Xacor.Graphics;

namespace Xacor;

internal sealed class Application : IApplication
{
    private readonly IWindow _window;
    private readonly IGame _game;
    private readonly RenderSystem _renderSystem;
    private readonly IGraphicsDeviceInitializer _graphicsDeviceInitializer;

    public Application(
        IGame game, 
        RenderSystem renderSystem,
        IGraphicsDeviceInitializer graphicsDeviceInitializer)
    {
        SdlWindowing.RegisterPlatform();
        SdlInput.RegisterPlatform();

        _game = game;
        _renderSystem = renderSystem;
        _graphicsDeviceInitializer = graphicsDeviceInitializer;

        
        var windowOptions = WindowOptions.Default;
        windowOptions.IsContextControlDisabled = true;
        
        //TODO(deccer): get those options from configuration
        windowOptions.API = new GraphicsAPI(
            ContextAPI.OpenGL,
            ContextProfile.Core,
            ContextFlags.Debug,
            new APIVersion(4, 6));
        windowOptions.Title = game.GetTitle();
        windowOptions.Size = new Vector2D<int>(1680, 720);
        windowOptions.VSync = true;

        _window = Window.Create(windowOptions);
        _window.Load += OnWindowLoad;
        _window.Update += OnWindowUpdate;
        _window.Render += OnWindowRender;
        _window.FramebufferResize += OnWindowFramebufferResize;

        var primaryMonitor = Monitor.GetMainMonitor(_window);
        _window.Center(primaryMonitor);
    }

    private void OnWindowLoad()
    {
        _graphicsDeviceInitializer.InitializeGraphicsDevice(_window);
    }

    private void OnWindowFramebufferResize(Vector2D<int> newFramebufferSize)
    {
        _renderSystem.NotifyResolutionChange(newFramebufferSize.X, newFramebufferSize.Y);
    }

    private void OnWindowRender(double deltaTime)
    {
        _renderSystem.Run(1 / 60.0f);
        
        _window.SwapBuffers();
    }

    private void OnWindowUpdate(double deltaTime)
    {
        _game.VariableUpdate(1 / 60.0f);
        _game.FixedUpdate(1 / 60.0f);
    }

    public void Dispose()
    {
        _game.Dispose();
    }

    public void Run()
    {
        if (!_game.Initialize())
        {
            return;
        }

        _window.Run();

        _game.Shutdown();
    }
}