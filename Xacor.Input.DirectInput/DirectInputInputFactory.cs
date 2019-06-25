using System;

namespace Xacor.Input.DirectInput
{
    public class DirectInputInputFactory : IInputFactory
    {
        //private IntPtr _windowHandle;
        private SharpDX.DirectInput.DirectInput _nativeDirectInput;

        public void Cleanup()
        {
            _nativeDirectInput?.Dispose();
        }

        public IInputSource CreateInputSource(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Keyboard:
                    return new DirectInputKeyboard(_nativeDirectInput);
                case InputType.MouseMovement:
                    return new DirectInputMouse(_nativeDirectInput);
                case InputType.Joystick:
                    return new DirectInputJoystick(_nativeDirectInput);
                default:
                    throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
            }
        }

        public void Dispose()
        {
            _nativeDirectInput?.Dispose();
        }

        public void Initialize(IntPtr windowHandle)
        {
            //_windowHandle = windowHandle;
            _nativeDirectInput = new SharpDX.DirectInput.DirectInput();
        }
    }
}