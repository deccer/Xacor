using System.Runtime.InteropServices;

namespace Xacor.Graphics.Commands;

[StructLayout(LayoutKind.Sequential)]
public struct DrawIndexedCommand
{
    public uint IndexCount;
    public uint InstanceCount;
    public uint FirstIndex;
    public int VertexOffset;
    public uint FirstInstance;
}