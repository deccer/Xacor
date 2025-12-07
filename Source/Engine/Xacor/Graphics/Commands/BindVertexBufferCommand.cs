using System.Runtime.InteropServices;

namespace Xacor.Graphics.Commands;

[StructLayout(LayoutKind.Sequential)]
public struct BindVertexBufferCommand
{
    public uint BufferHandle;
    public uint Binding;
    public ulong Offset;
}