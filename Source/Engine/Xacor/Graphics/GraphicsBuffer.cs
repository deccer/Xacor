namespace Xacor.Graphics;

//TODO(deccer) maybe we rename this to just Buffer
public class GraphicsBuffer : GraphicsResource
{
    public uint Size { get; }

    public GraphicsBuffer(
        uint handle, 
        uint size) 
        : base(handle)
    {
        Size = size;
    }
}