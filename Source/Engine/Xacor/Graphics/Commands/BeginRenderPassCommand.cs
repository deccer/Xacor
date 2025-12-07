using System.Runtime.InteropServices;

namespace Xacor.Graphics.Commands;

[StructLayout(LayoutKind.Sequential)]
public struct BeginRenderPassCommand
{
    public uint FramebufferHandle;
    public uint RenderPassHandle;
    public float ClearR, ClearG, ClearB, ClearA;
    public float ClearDepth;
    public uint ClearStencil;
}