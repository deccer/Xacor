namespace Xacor.Platform
{
    public interface IGamePlatformFactory
    {
        IGameWindow CreateGameWindow(GraphicsOptions graphicsOptions);

        IGameLoop CreateGameLoop();
    }
}
