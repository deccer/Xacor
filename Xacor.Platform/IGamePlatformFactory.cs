namespace Xacor.Platform
{
    public interface IGamePlatformFactory
    {
        IGameWindow CreateGameWindow();

        IGameLoop CreateGameLoop();
    }
}
