using System;

namespace Xacor.Input
{
    public interface IInputSource : IDisposable
    {
        InputData CollectInputData();
    }
}
