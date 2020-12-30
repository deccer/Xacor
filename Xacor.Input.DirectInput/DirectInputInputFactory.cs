using System;
using System.Linq;
using SharpDX.DirectInput;

namespace Xacor.Input.DirectInput
{
    public class DirectInputInputFactory : IInputFactory
    {
        private readonly SharpDX.DirectInput.DirectInput _nativeDirectInput;

        public IInputSource CreateInputSource(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Keyboard:
                    return new DirectInputKeyboard(_nativeDirectInput);
                case InputType.MouseMovement:
                    return new DirectInputMouse(_nativeDirectInput);
                case InputType.Joystick:
                    var gameDevice = _nativeDirectInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly).FirstOrDefault();
                    if (gameDevice != null)
                    {
                        return new DirectInputJoystick(_nativeDirectInput, gameDevice.ProductGuid);
                    }
                    throw new Exception("No attached joystick found.");
                default:
                    throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
            }
        }

        public void Dispose()
        {
            _nativeDirectInput?.Dispose();
        }

        public DirectInputInputFactory()
        {
            _nativeDirectInput = new SharpDX.DirectInput.DirectInput();
        }
    }
}
