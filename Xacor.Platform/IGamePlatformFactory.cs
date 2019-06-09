namespace Xacor.Platform
{
    public interface IGamePlatformFactory
    {
        IGameWindow CreateGameWindow(string caption);

        IGameLoop CreateGameLoop();
    }
}
