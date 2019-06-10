using Xacor.Game;
using Xacor.Graphics;
using Xacor.Platform;

namespace Xacor.Demo
{
    internal class DemoGame : GameBase
    {
        private readonly IGraphicsFactory _graphicsFactory;
        private ICommandList _commandList;

        public DemoGame(IGamePlatformFactory gamePlatformFactory, IGraphicsFactory graphicsFactory)
            : base(gamePlatformFactory, graphicsFactory)
        {
            _graphicsFactory = graphicsFactory;
        }

        protected override void Draw()
        {
            
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.Title = "Xacor.Demo";

            _commandList = _graphicsFactory.CreateCommandList();
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}