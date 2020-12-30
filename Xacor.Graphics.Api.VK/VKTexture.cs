namespace Xacor.Graphics.Api.VK
{
    internal class VKTexture : ITexture
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public TextureView View { get; }

        public void SetData<T>(T[] data) where T : struct
        {
            throw new System.NotImplementedException();
        }
    }
}