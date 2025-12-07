using System.Runtime.InteropServices;

namespace Xacor.Graphics.Commands;

[StructLayout(LayoutKind.Sequential)]
public struct SetPushConstantsCommand
{
    public uint UniformBufferHandle;
    public uint Offset;
    public uint SizeInBytes;
}