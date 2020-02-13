namespace Xacor.Graphics.Materials
{
    public interface IMaterial
    {
        string Name { get; }

        void Apply();
    }
}