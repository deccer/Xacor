using System.Runtime.InteropServices;

namespace Xacor.Graphics.Commands;

[StructLayout(LayoutKind.Sequential)]
public struct DrawCommand
{
    public uint VertexCount;
    public uint InstanceCount;
    public uint FirstVertex;
    public uint FirstInstance;
}