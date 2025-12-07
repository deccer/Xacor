using System.Runtime.InteropServices;

namespace Xacor.Graphics.Commands;

[StructLayout(LayoutKind.Sequential)]
public struct BindIndexBufferCommand
{
    public uint BufferHandle;
    public ulong Offset;
    public IndexType IndexType;
}