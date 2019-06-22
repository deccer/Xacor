using System;
using System.Diagnostics;
using Xacor.Graphics;
using Xacor.Graphics.Api;
using Xacor.Platform;

namespace Xacor.Game
{
    public class GameBase : IDisposable
    {
        private readonly Options _options;
        private readonly IGamePlatformFactory _gamePlatformFactory;
        private readonly IGameLoop _gameLoop;

        private readonly Stopwatch _stopWatch = Stopwatch.StartNew();
        private double _lastUpdate;

        private ISwapChain _swapchain;

        protected IGraphicsDevice GraphicsDevice { get; }

        protected IGraphicsFactory GraphicsFactory { get; }

        protected IGameWindow Window { get; private set; }

        protected TextureView BackBufferView { get; private set; }

        protected TextureView BackBufferDepthStencilView { get; private set; }

        protected virtual void BeginDraw()
        {

        }

        protected virtual void Cleanup()
        {

        }

        public void Dispose()
        {
        }

        protected virtual void Draw()
        {

        }

        protected virtual void EndDraw()
        {

        }

        protected GameBase(Options options, IGamePlatformFactory gamePlatformFactory, IGraphicsFactory graphicsFactory)
        {
            _options = options;
            _gamePlatformFactory = gamePlatformFactory;
            GraphicsFactory = graphicsFactory;

            _gameLoop = _gamePlatformFactory.CreateGameLoop();
        }

        protected virtual void Initialize()
        {
            Window = _gamePlatformFactory.CreateGameWindow(_options.Graphics);

            var swapChainInfo = CreateSwapChainInfo();
            _swapchain = GraphicsFactory.CreateSwapchain(swapChainInfo);
            BackBufferView = _swapchain.TextureView;
            BackBufferDepthStencilView = _swapchain.DepthStencilView;
        }

        protected virtual void Update(double deltaTime)
        {

        }

        public void Run()
        {
            Initialize();
            _stopWatch.Restart();
            _lastUpdate = _stopWatch.ElapsedMilliseconds / 1000.0;
            _gameLoop.Run(Window, Tick);
            _stopWatch.Stop();
            Cleanup();
        }

        private SwapChainInfo CreateSwapChainInfo()
        {
            return new SwapChainInfo(Window.Handle, Window.Width, Window.Height, _options.Graphics.WindowState != WindowState.Fullscreen, SwapEffect.FlipDiscard);
        }

        private void Tick()
        {
            var now = _stopWatch.ElapsedMilliseconds / 1000.0;
            var deltaTime = now - _lastUpdate;
            Update(deltaTime);
            _lastUpdate = now;
            BeginDraw();
            Draw();
            EndDraw();

            _swapchain.Present();
        }
    }
}