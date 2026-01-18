using Microsoft.Extensions.DependencyInjection;
using Silk.NET.Input.Sdl;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Sdl;
using Xacor.Ecs;
using Xacor.Game;

namespace Xacor;

internal sealed class Application : IApplication
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IWindow _window;
    private readonly IGame _game;
    private RenderSystem? _renderSystem;
    private IServiceScope? _scope;

    public Application(
        ApplicationSettings settings,
        IServiceScopeFactory serviceScopeFactory,
        IWindowSetter windowSetter,
        IGame game)
    {
        SdlWindowing.RegisterPlatform();
        SdlInput.RegisterPlatform();

        _serviceScopeFactory = serviceScopeFactory;
        _game = game;
        
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
        windowOptions.VSync = settings.VSync;
        windowOptions.UpdatesPerSecond = settings.UpdatesPerSecond;
        windowOptions.FramesPerSecond = settings.FramesPerSecond;

        _window = Window.Create(windowOptions);
        _window.Load += OnWindowLoad;
        _window.Update += OnWindowUpdate;
        _window.Render += OnWindowRender;
        _window.FramebufferResize += OnWindowFramebufferResize;

        var primaryMonitor = Monitor.GetMainMonitor(_window);
        _window.Center(primaryMonitor);
        windowSetter.SetWindow(_window);
    }

    private void OnWindowLoad()
    {
        _scope = _serviceScopeFactory.CreateScope();
        _renderSystem = _scope.ServiceProvider.GetRequiredService<RenderSystem>();
        _renderSystem.Initialize();
    }

    private void OnWindowFramebufferResize(Vector2D<int> newFramebufferSize)
    {
        _renderSystem?.NotifyResolutionChange(newFramebufferSize.X, newFramebufferSize.Y);
    }

    private void OnWindowRender(double deltaTime)
    {
        _renderSystem?.Run(1 / 60.0f);
        
        _window.SwapBuffers();
    }

    private void OnWindowUpdate(double deltaTime)
    {
        _game.VariableUpdate();
        _game.FixedUpdate();
    }

    public void Dispose()
    {
        _scope?.Dispose();
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