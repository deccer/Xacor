namespace Xacor.Graphics.Materials
{
    public interface IMaterialFactory
    {
        IMaterial CreateMaterial(string name);
    }
}