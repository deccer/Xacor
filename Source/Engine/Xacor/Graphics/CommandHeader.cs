using System.Runtime.InteropServices;

namespace Xacor.Graphics;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct CommandHeader
{
    public CommandType Type;
    public ushort PayloadSize;
}