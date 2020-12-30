using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xacor.Platform.Windows.Native
{
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("X: {X}, Y: {Y}")]
    public struct RawPoint
    {
        public RawPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;

        public int Y;
    }
}
