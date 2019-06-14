namespace Xacor.Game
{
    public class GameOptions
    {
        public GraphicsOptions Graphics { get; }

        public GameOptions(GraphicsOptions graphicsOptions)
        {
            Graphics = graphicsOptions;
        }
    }
}