using System;
using System.Runtime.InteropServices;

namespace Xacor.Platform.Windows.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeMessage
    {
        public IntPtr handle;
        public uint msg;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public RawPoint p;
    }
}