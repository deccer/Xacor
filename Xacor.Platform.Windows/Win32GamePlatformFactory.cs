namespace Xacor.Platform.Windows
{
    public class Win32GamePlatformFactory : IGamePlatformFactory
    {
        public IGameWindow CreateGameWindow()
        {
            return new Win32GameWindow();
        }

        public IGameLoop CreateGameLoop()
        {
            return new Win32GameLoop();
        }
    }
}