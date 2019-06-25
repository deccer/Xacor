using SharpDX.DirectInput;

namespace Xacor.Input.DirectInput
{
    internal class DirectInputMouse : IInputSource
    {
        private readonly Mouse _nativeMouse;

        public DirectInputMouse(SharpDX.DirectInput.DirectInput nativeDirectInput)
        {
            _nativeMouse = new Mouse(nativeDirectInput);
        }
    
        public void Dispose()
        {
            _nativeMouse?.Dispose();
        }
    }
}