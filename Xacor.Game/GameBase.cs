using System;
using System.Dynamic;
using Xacor.Graphics;
using Xacor.Platform;

namespace Xacor.Game
{
    public class GameBase : IDisposable
    {
        private readonly IGamePlatformFactory _gamePlatformFactory;
        private readonly IGameLoop _gameLoop;

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

        protected GameBase(IGamePlatformFactory gamePlatformFactory, IGraphicsFactory graphicsFactory)
        {
            _gamePlatformFactory = gamePlatformFactory;
            GraphicsFactory = graphicsFactory;

            _gameLoop = _gamePlatformFactory.CreateGameLoop();
        }

        protected virtual void Initialize()
        {
            Window = _gamePlatformFactory.CreateGameWindow();

            var swapChainInfo = CreateSwapChainInfo();
            _swapchain = GraphicsFactory.CreateSwapchain(swapChainInfo);
            BackBufferView = _swapchain.TextureView;
            BackBufferDepthStencilView = _swapchain.DepthStencilView;
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