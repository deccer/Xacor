namespace Xacor.Graphics.Api
{
    public readonly struct ShaderResource
    {
        public readonly string Name;

        public readonly ResourceType Type;

        public readonly int Slot;

        public readonly ShaderStage Stage;

        public ShaderResource(string name, ResourceType type, int slot, ShaderStage stage)
        {
            Name = name;
            Type = type;
            Slot = slot;
            Stage = stage;
        }
    }
}
