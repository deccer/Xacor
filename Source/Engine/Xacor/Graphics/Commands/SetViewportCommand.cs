using System.Runtime.InteropServices;

namespace Xacor.Graphics.Commands;

[StructLayout(LayoutKind.Sequential)]
public struct SetViewportCommand
{
    public float X, Y, Width, Height, MinDepth, MaxDepth;
}