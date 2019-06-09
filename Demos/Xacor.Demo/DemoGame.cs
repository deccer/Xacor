using Xacor.Game;
using Xacor.Graphics;
using Xacor.Platform;

namespace Xacor.Demo
{
    internal class DemoGame : GameBase
    {
        public DemoGame(IGamePlatformFactory gamePlatformFactory, IGraphicsFactory graphicsFactory)
            : base(gamePlatformFactory, graphicsFactory)
        {

        }

        protected override void Draw()
        {
            
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.Title = "Xacor.Demo";
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}