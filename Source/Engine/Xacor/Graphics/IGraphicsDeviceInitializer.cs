using Silk.NET.Windowing;

namespace Xacor.Graphics;

public interface IGraphicsDeviceInitializer
{
    void InitializeGraphicsDevice(IWindow window);
}