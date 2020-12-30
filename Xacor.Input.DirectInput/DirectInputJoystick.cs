using System;
using SharpDX.DirectInput;

namespace Xacor.Input.DirectInput
{
    public class DirectInputJoystick : IInputSource
    {
        private static readonly (Axis, float)[] EmptyAxisData = new (Axis, float)[0];
        private readonly SharpDX.DirectInput.DirectInput _nativeDirectInput;
        private readonly Joystick _joystick;
        private JoystickState _previousState;

        public DirectInputJoystick(SharpDX.DirectInput.DirectInput nativeDirectInput, Guid deviceGuid)
        {
            _nativeDirectInput = nativeDirectInput;
            _joystick = new Joystick(_nativeDirectInput, deviceGuid);
        }

        public void Dispose()
        {
            _nativeDirectInput?.Dispose();
        }

        public InputData CollectInputData()
        {
            var currentState = _joystick.GetCurrentState();

            var inputData = new InputData
            {
                AxisData = EmptyAxisData
            };
            _previousState = currentState;

            return inputData;
        }
    }
}
