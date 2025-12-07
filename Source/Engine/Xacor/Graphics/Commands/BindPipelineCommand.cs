using System.Runtime.InteropServices;

namespace Xacor.Graphics.Commands;

[StructLayout(LayoutKind.Sequential)]
public struct BindPipelineCommand
{
    public uint PipelineHandle;
}