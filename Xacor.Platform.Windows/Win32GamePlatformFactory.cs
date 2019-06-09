namespace Xacor.Platform.Windows
{
    public class Win32GamePlatformFactory : IGamePlatformFactory
    {
        public IGameWindow CreateGameWindow(string caption)
        {
            return new Win32GameWindow(caption);
        }

        public IGameLoop CreateGameLoop()
        {
            return new Win32GameLoop();
        }
    }
}