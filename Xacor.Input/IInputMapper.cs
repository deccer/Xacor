using System.Collections.Generic;

namespace Xacor.Input
{
    public class InputMapper
    {
        private readonly List<Input> _inputs;
        private readonly List<IInputSource> _inputSources;
        private readonly Dictionary<string, float> _values;

        public void AddMap(Input input)
        {
            _inputs.Add(input);
        }

        public void AddInputSource(IInputSource inputSource)
        {

        }

        public float GetAxis(string name)
        {
            return _values.TryGetValue(name, out var value) ? value : 0.0f;
        }

        public InputMapper()
        {
            _inputSources = new List<IInputSource>();
            _inputs = new List<Input>
            {
                Input.CreateKeyboardInput("MoveForwardBackward", "w", "s"),
                Input.CreateKeyboardInput("SlideRightLeft", "a", "d"),
                Input.CreateMouseMovement("Mouse X", Axis.Horizontal),
                Input.CreateMouseMovement("Mouse Y", Axis.Vertical),
            };
            _values = new Dictionary<string, float>(16);
        }

        public void UpdateMaps()
        {
            // somehow collect input values from RawInput/DirectInput/Whatever
            // somehow link inputsources to maps
            foreach (var inputSource in _inputSources)
            {
                //inputSource.Collect();
            }
            
            // update _values
        }
    }
}