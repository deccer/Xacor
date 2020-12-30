namespace Xacor.Platform.Windows
{
    public class Win32GamePlatformFactory : IGamePlatformFactory
    {
        public IGameWindow CreateGameWindow(GraphicsOptions graphicsOptions)
        {
            return new Win32GameWindow(graphicsOptions);
        }

        public IGameLoop CreateGameLoop()
        {
            return new Win32GameLoop();
        }
    }
}
