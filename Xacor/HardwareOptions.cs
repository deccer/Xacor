namespace Xacor
{
    public sealed class HardwareOptions
    {
        public HardwareOptions(bool useHardwareDevice, bool isDebug)
        {
            UseHardwareDevice = useHardwareDevice;
            IsDebug = isDebug;
        }

        public bool UseHardwareDevice { get; }

        public bool IsDebug { get; }
    }
}
