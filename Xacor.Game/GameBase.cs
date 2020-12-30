using System;
using System.Diagnostics;
using Xacor.Graphics.Api;
using Xacor.Input;
using Xacor.Platform;

namespace Xacor.Game
{
    public class GameBase : IDisposable
    {
        private readonly Options _options;
        private readonly IGamePlatformFactory _gamePlatformFactory;
        private readonly IGameLoop _gameLoop;
        private readonly IInputFactory _inputFactory;
        private readonly InputMapper _inputMapper;

        private readonly Stopwatch _stopWatch = Stopwatch.StartNew();
        private float _lastUpdate;
        private int _framesToAverage = 512;
        private int _frameCounter;

        private float _averageDeltaTime;

        private ISwapChain _swapchain;

        protected IGraphicsDevice GraphicsDevice { get; }

        protected IGraphicsFactory GraphicsFactory { get; }

        protected IGameWindow Window { get; private set; }

        protected TextureView BackBufferView { get; private set; }

        protected TextureView BackBufferDepthStencilView { get; private set; }

        protected IInputControls Input => _inputMapper;

        protected void AddInputMap(Input.Input input)
        {
            _inputMapper.AddMap(input);
        }

        protected virtual void BeginDraw()
        {
        }

        protected virtual void Cleanup()
        {
        }

        public void Dispose()
        {
            _inputFactory?.Dispose();
        }

        protected virtual void Draw()
        {
        }

        protected virtual void EndDraw()
        {
        }

        protected GameBase(Options options, IGamePlatformFactory gamePlatformFactory, IGraphicsFactory graphicsFactory, IInputFactory inputFactory)
        {
            _options = options;
            _gamePlatformFactory = gamePlatformFactory;
            GraphicsFactory = graphicsFactory;
            _inputFactory = inputFactory;
            _inputMapper = new InputMapper(_inputFactory);

            _gameLoop = _gamePlatformFactory.CreateGameLoop();
        }

        protected virtual void Initialize()
        {
            Window = _gamePlatformFactory.CreateGameWindow(_options.Graphics);

            foreach (var inputMapping in _options.Input.InputMappings)
            {
                switch (inputMapping)
                {
                    case KeyboardInputMapping keyboardInputMapping:
                        _inputMapper.AddMap(Xacor.Input.Input.CreateKeyboardInput(inputMapping.Name, keyboardInputMapping.Key1, keyboardInputMapping.Key2));
                        break;
                    case MouseInputMapping mouseInputMapping:
                        _inputMapper.AddMap(Xacor.Input.Input.CreateMouseMovement(inputMapping.Name, mouseInputMapping.Axis));
                        break;
                }
            }

            var swapChainInfo = CreateSwapChainDescriptor();
            _swapchain = GraphicsFactory.CreateSwapchain(swapChainInfo);
            BackBufferView = _swapchain.TextureView;
            BackBufferDepthStencilView = _swapchain.DepthStencilView;
        }

        protected virtual void Update(float deltaTime)
        {
            _inputMapper.UpdateMaps();
        }

        public void Run()
        {
            Initialize();
            _stopWatch.Restart();
            _lastUpdate = _stopWatch.ElapsedMilliseconds / 1000.0f;
            _gameLoop.Run(Window, Tick);
            _stopWatch.Stop();
            Cleanup();
        }

        private SwapChainDescriptor CreateSwapChainDescriptor()
        {
            return new SwapChainDescriptor(
                Window.Handle,
                _options.Graphics.RenderResolution.Width,
                _options.Graphics.RenderResolution.Height,
                _options.Graphics.WindowState != WindowState.Fullscreen,
                _options.Graphics.VSync,
                SwapEffect.FlipDiscard);
        }

        private void Tick()
        {
            var now = _stopWatch.ElapsedMilliseconds / 1000.0f;
            var deltaTime = now - _lastUpdate;

            _averageDeltaTime += deltaTime;
            if (_frameCounter == _framesToAverage)
            {
                var averageFrameTime = _averageDeltaTime / _frameCounter;

                Window.Title = $"Xacor | {_options.Graphics.RenderApi} | delta.avg: {averageFrameTime * 1000.0f:000.00}ms | delta.abs: {deltaTime * 1000.0f:000.00}ms | fps: {1.0f / averageFrameTime:0000}";

                _averageDeltaTime = 0.0f;
                _frameCounter = 0;
            }

            Update(deltaTime);
            _lastUpdate = now;
            BeginDraw();
            Draw();
            EndDraw();

            _swapchain.Present();

            _frameCounter++;
        }
    }
}