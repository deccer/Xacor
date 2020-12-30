namespace Xacor
{
    public sealed class Options
    {
        public HardwareOptions HardwareOptions { get; }

        public GraphicsOptions Graphics { get; }

        public InputOptions Input { get; }

        public Options(HardwareOptions hardwareOptions, GraphicsOptions graphicsOptions, InputOptions inputOptions)
        {
            HardwareOptions = hardwareOptions;
            Graphics = graphicsOptions;
            Input = inputOptions;
        }
    }
}
