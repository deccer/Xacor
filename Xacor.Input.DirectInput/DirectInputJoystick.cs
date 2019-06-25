namespace Xacor.Input.DirectInput
{
    public class DirectInputJoystick : IInputSource
    {
        private readonly SharpDX.DirectInput.DirectInput _nativeDirectInput;

        public DirectInputJoystick(SharpDX.DirectInput.DirectInput nativeDirectInput)
        {
            _nativeDirectInput = nativeDirectInput;
        }

        public void Dispose()
        {
            _nativeDirectInput?.Dispose();
        }
    }
}