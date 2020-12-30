using System.Collections.Generic;

namespace Xacor
{
    public class InputOptions
    {
        public List<InputMapping> InputMappings { get; }

        public InputOptions(List<InputMapping> inputMappings)
        {
            InputMappings = inputMappings ?? new List<InputMapping>();
        }
    }
}
