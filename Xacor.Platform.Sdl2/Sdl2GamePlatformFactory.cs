using Serilog;

namespace Xacor.Platform.Sdl2
{
    public sealed class Sdl2GamePlatformFactory : IGamePlatformFactory
    {
        private readonly ILogger _logger;

        public Sdl2GamePlatformFactory(ILogger logger)
        {
            _logger = logger.ForContext<Sdl2GamePlatformFactory>();
        }

        public IGameWindow CreateGameWindow(GraphicsOptions graphicsOptions)
        {
            return new Sdl2GameWindow(_logger, graphicsOptions);
        }

        public IGameLoop CreateGameLoop()
        {
            return new Sdl2GameLoop();
        }
    }
}