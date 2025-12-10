using Silk.NET.Windowing;

namespace Xacor;

internal class WindowHolder : IWindowGetter, IWindowSetter
{
    private IWindow? _window = null;
    
    public IWindow? GetWindow()
    {
        return _window;
    }

    public void SetWindow(IWindow window)
    {
        _window = window;
    }
}