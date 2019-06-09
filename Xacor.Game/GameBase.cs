using System;
using Xacor.Graphics;
using Xacor.Platform;

namespace Xacor.Game
{
    public class GameBase : IDisposable
    {
        private readonly IGamePlatformFactory _gamePlatformFactory;
        private readonly IGraphicsFactory _graphicsFactory;
        private readonly IGameLoop _gameLoop;

        private ISwapChain _swapchain;

        public IGraphicsDevice GraphicsDevice { get; }

        public IGameWindow Window { get; private set; }

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

        protected GameBase(IGamePlatformFactory gamePlatformFactory, IGraphicsFactory graphicsFactory)
        {
            _gamePlatformFactory = gamePlatformFactory;
            _graphicsFactory = graphicsFactory;

            _gameLoop = _gamePlatformFactory.CreateGameLoop();
        }

        protected virtual void Initialize()
        {
            Window = _gamePlatformFactory.CreateGameWindow("Xacor");

            var swapChainInfo = CreateSwapChainInfo();
            _swapchain = _graphicsFactory.CreateSwapchain(swapChainInfo);
        }

        protected virtual void Update()
        {

        }

        public void Run()
        {
            Initialize();
            _gameLoop.Run(Window, Tick);
            Cleanup();
        }

        private SwapChainInfo CreateSwapChainInfo()
        {
            return new SwapChainInfo(Window.Handle, Window.Width, Window.Height, true, SwapEffect.FlipDiscard);
        }

        private void Tick()
        {
            Update();
            BeginDraw();
            Draw();
            EndDraw();

            _swapchain.Present();
        }
    }
}
