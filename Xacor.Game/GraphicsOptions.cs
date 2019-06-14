using System.Drawing;

namespace Xacor.Game
{
    public class GraphicsOptions
    {
        public Size Resolution { get; }

        public GraphicsOptions(Size resolution)
        {
            Resolution = resolution;
        }
    }
}