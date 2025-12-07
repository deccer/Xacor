namespace Xacor.Graphics;

//TODO(deccer) maybe we rename this to just Resource?
public abstract class GraphicsResource
{
    internal uint Handle { get; }

    protected GraphicsResource(uint handle) 
        => Handle = handle;
}