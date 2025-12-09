using System;
using Silk.NET.Windowing;
using Xacor.Graphics.Gl;

namespace Xacor.Graphics;

internal sealed class GlGraphicsDeviceProvider : IGraphicsDeviceProvider, IGraphicsDeviceInitializer
{
    private IGraphicsDevice? _graphicsDevice;
    
    public void InitializeGraphicsDevice(IWindow window)
    {
        _graphicsDevice = new GlGraphicsDevice(window);
    }

    public IGraphicsDevice GetGraphicsDevice()
    {
        return _graphicsDevice ?? throw new InvalidOperationException("GraphicsDevice needs to be initialized first.");
    }
}