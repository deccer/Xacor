namespace Xacor.Input
{
    public interface IInputControls
    {
        float GetAxis(string name);

        bool IsButtonDown(string name);
    }
}
