using SharpDX.DirectInput;

namespace Xacor.Input.DirectInput
{
    internal class DirectInputKeyboard : IInputSource
    {
        private readonly Keyboard _nativeKeyboard;

        public DirectInputKeyboard(SharpDX.DirectInput.DirectInput nativeDirectInput)
        {
            _nativeKeyboard = new Keyboard(nativeDirectInput);
        }

        public void Dispose()
        {
            _nativeKeyboard?.Dispose();
        }
    }
}