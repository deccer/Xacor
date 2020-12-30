using System.Collections.Generic;
using System.Linq;
using SharpDX.DirectInput;

namespace Xacor.Input.DirectInput
{
    internal class DirectInputMouse : IInputSource
    {
        private static readonly Dictionary<int, InputButton> _mouseButtonMap;

        private readonly Mouse _nativeMouse;
        private MouseState _previousMouseState;

        static DirectInputMouse()
        {
            _mouseButtonMap = new Dictionary<int, InputButton>
            {
                { 0, InputButton.Mouse1 },
                { 1, InputButton.Mouse2 },
                { 2, InputButton.Mouse3 },
                { 3, InputButton.Mouse4 }
            };
        }

        public DirectInputMouse(SharpDX.DirectInput.DirectInput nativeDirectInput)
        {
            _nativeMouse = new Mouse(nativeDirectInput);
            _nativeMouse.Acquire();
            _previousMouseState = _nativeMouse.GetCurrentState();
        }

        public void Dispose()
        {
            _nativeMouse.Unacquire();
            _nativeMouse.Dispose();
        }

        public InputData CollectInputData()
        {
            var mouseState = _nativeMouse.GetCurrentState();

            var axisData = new (Axis, float)[]
            {
                (Axis.Horizontal, mouseState.X - _previousMouseState.X),
                (Axis.Vertical, mouseState.Y - _previousMouseState.Y),
            };

            var inputData = new InputData
            {
                AxisData = axisData.ToArray(),
                Keys = mouseState.Buttons.Select((value, index) => _mouseButtonMap.TryGetValue(index, out var inputButton) ? (inputButton, value) : (InputButton.Unassigned, false)).ToArray()
            };

            _previousMouseState = mouseState;

            return inputData;
        }
    }
}
