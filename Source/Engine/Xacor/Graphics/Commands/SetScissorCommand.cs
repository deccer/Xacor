using System.Runtime.InteropServices;

namespace Xacor.Graphics.Commands;

[StructLayout(LayoutKind.Sequential)]
public struct SetScissorCommand
{
    public int X, Y, Width, Height;
}