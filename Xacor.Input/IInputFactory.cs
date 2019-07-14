using System;

namespace Xacor.Input
{
    public interface IInputFactory : IDisposable
    {
        IInputSource CreateInputSource(InputType inputType);
    }
}