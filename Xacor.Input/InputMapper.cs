using System;
using System.Collections.Generic;
using System.Linq;

namespace Xacor.Input
{
    public class InputMapper : IInputControls
    {
        private readonly IInputFactory _inputFactory;

        private readonly Dictionary<InputType, IInputSource> _inputSources;

        private readonly Dictionary<string, List<InputButton>> _buttonMap;
        private readonly Dictionary<InputButton, bool> _buttonValueMap;

        private readonly Dictionary<string, Axis> _axisMap;
        private readonly Dictionary<Axis, float> _axisValueMap;

        public void AddMap(Input input)
        {
            var inputType = input.InputType;
            if (!_inputSources.ContainsKey(inputType))
            {
                _inputSources.Add(inputType, _inputFactory.CreateInputSource(inputType));
            }

            if (input.Button != InputButton.Unassigned)
            {
                var buttons = new List<InputButton>(2)
                {
                    input.Button
                };

                if (input.AlternativeButton != InputButton.Unassigned)
                {
                    buttons.Add(input.AlternativeButton);
                }

                _buttonMap.Add(input.Name, buttons);
            }

            if (inputType == InputType.MouseMovement)
            {
                _axisMap.Add(input.Name, input.Axis);
            }
        }

        public float GetAxis(string name)
        {
            return _axisMap.TryGetValue(name, out var axis) ? _axisValueMap.TryGetValue(axis, out var value) ? value : 0.0f : 0.0f;
        }

        public bool IsButtonDown(string name)
        {
            return _buttonMap.TryGetValue(name, out var inputButtons) && inputButtons.Any(inputButton => _buttonValueMap.TryGetValue(inputButton, out var value) && value);
        }

        public InputMapper(IInputFactory inputFactory)
        {
            _inputFactory = inputFactory;
            _inputSources = new Dictionary<InputType, IInputSource>();

            _buttonMap = new Dictionary<string, List<InputButton>>(32);
            _buttonValueMap = new Dictionary<InputButton, bool>(32);

            _axisMap = new Dictionary<string, Axis>(32);
            _axisValueMap = new Dictionary<Axis, float>(32);
        }

        public void UpdateMaps()
        {
            // somehow collect input values from RawInput/DirectInput/Whatever
            // somehow link inputsources to maps

            foreach (var inputSource in _inputSources.Values)
            {
                var inputData = inputSource.CollectInputData();
                foreach (var (pressedKey, isPressed) in inputData.Keys)
                {
                    _buttonValueMap[pressedKey] = isPressed;
                }

                foreach (var (axis, axisValue) in inputData.AxisData)
                {
                    _axisValueMap[axis] = axisValue;
                }
            }
            
            // update _values
        }
    }
}