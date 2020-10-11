namespace Xacor.Graphics.Materials
{
    public class MaterialFactory : IMaterialFactory
    {
        public IMaterial CreateMaterial(string name)
        {
            return new Material
            {
                Name = name
            };
        }
    }
}