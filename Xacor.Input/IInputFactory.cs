using System;

namespace Xacor.Input
{
    public interface IInputFactory : IDisposable
    {
        IInputSource CreateInputSource(InputType inputType);

        void Initialize(IntPtr windowHandle);
    }
}