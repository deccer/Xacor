using System;
using SDL2;
using Serilog;

namespace Xacor.Platform.Sdl2
{
    internal sealed class Sdl2GameWindow : IGameWindow
    {
        private readonly ILogger _logger;
        private readonly IntPtr _nativeWindow;
        private readonly SDL.SDL_SysWMinfo _windowInfo;

        public Sdl2GameWindow(ILogger logger, GraphicsOptions graphicsOptions)
        {
            _logger = logger.ForContext<Sdl2GameWindow>();

            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) > 0)
            {
                _logger.Error("Unable to initialize SDL2");
            }

            _nativeWindow = SDL.SDL_CreateWindow("Xacor",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                graphicsOptions.WindowResolution.Width,
                graphicsOptions.WindowResolution.Height,
                RenderApiToSdlFlags(graphicsOptions.RenderApi) | GraphicsOptionsToSdlFlags(graphicsOptions));

            if (SDL.SDL_GetWindowWMInfo(_nativeWindow, ref _windowInfo) == SDL.SDL_bool.SDL_FALSE)
            {
                _logger.Error("Unable to grab native window handle");
            }

            IsOpen = true;
            Handle = _windowInfo.info.win.window;
        }

        public IntPtr Handle { get; }

        public int Height
        {
            get
            {
                SDL.SDL_GetWindowSize(_nativeWindow, out _, out var height);
                return height;
            }
        }

        public string Title
        {
            get => SDL.SDL_GetWindowTitle(_nativeWindow);
            set => SDL.SDL_SetWindowTitle(_nativeWindow, value);
        }

        public int Width
        {
            get
            {
                SDL.SDL_GetWindowSize(_nativeWindow, out var width, out _);
                return width;
            }
        }

        public bool IsOpen { get; private set; }

        public void Close()
        {
            IsOpen = false;
            SDL.SDL_DestroyWindow(_nativeWindow);
        }

        public void Dispose()
        {
            SDL.SDL_Quit();
        }

        public void Show()
        {
            SDL.SDL_ShowWindow(_nativeWindow);
            IsOpen = true;
        }

        private SDL.SDL_WindowFlags RenderApiToSdlFlags(RenderApi renderApi)
        {
            return renderApi switch
            {
                RenderApi.D3D11 => SDL.SDL_WindowFlags.SDL_WINDOW_FOREIGN,
                RenderApi.D3D12 => SDL.SDL_WindowFlags.SDL_WINDOW_FOREIGN,
                RenderApi.Vulkan => SDL.SDL_WindowFlags.SDL_WINDOW_VULKAN,
                RenderApi.OpenGL33 => SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL,
                RenderApi.OpenGL46 => SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL,
                _ => throw new ArgumentOutOfRangeException(nameof(renderApi), renderApi, null)
            };
        }

        private SDL.SDL_WindowFlags GraphicsOptionsToSdlFlags(GraphicsOptions graphicsOptions)
        {
            return graphicsOptions.WindowState == WindowState.Windowed
                ? SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
                : SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS | SDL.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED;
        }

    }
}