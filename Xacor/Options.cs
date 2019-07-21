namespace Xacor
{
    public class Options
    {
        public GraphicsOptions Graphics { get; }

        public InputOptions Input { get; }

        public Options(GraphicsOptions graphicsOptions, InputOptions inputOptions)
        {
            Graphics = graphicsOptions;
            Input = inputOptions;
        }
    }
}