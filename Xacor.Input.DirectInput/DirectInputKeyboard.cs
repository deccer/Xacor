using System.Collections.Generic;
using System.Linq;
using SharpDX.DirectInput;

namespace Xacor.Input.DirectInput
{
    internal class DirectInputKeyboard : IInputSource
    {
        private static readonly (Axis, float)[] EmptyAxisData = new (Axis, float)[0];
        private static readonly Dictionary<Key, InputButton> _keyMap;

        private readonly Keyboard _nativeKeyboard;
        private KeyboardState _previousState;

        static DirectInputKeyboard()
        {
            _keyMap = new Dictionary<Key, InputButton>
            {
                {Key.W, InputButton.W},
                {Key.A, InputButton.A},
                {Key.S, InputButton.S},
                {Key.D, InputButton.D}
            };
        }

        public DirectInputKeyboard(SharpDX.DirectInput.DirectInput nativeDirectInput)
        {
            _nativeKeyboard = new Keyboard(nativeDirectInput);
            _nativeKeyboard.Acquire();
        }

        public void Dispose()
        {
            _nativeKeyboard.Unacquire();
            _nativeKeyboard.Dispose();
        }

        public InputData CollectInputData()
        {
            var currentState = _nativeKeyboard.GetCurrentState();

            var inputData = new InputData();
            inputData.AxisData = EmptyAxisData;
            inputData.Keys = currentState.AllKeys.Select(key => _keyMap.TryGetValue(key, out var inputButton) ? (inputButton, currentState.IsPressed(key)) : (InputButton.Unassigned, false)).ToArray();

            _previousState = currentState;

            return inputData;
        }
    }
}