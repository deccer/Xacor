namespace Xacor.Graphics.Api.GL33
{
    internal class GL33TextureView : TextureView
    {
        private readonly int _nativeTexture;

        public static implicit operator int(GL33TextureView textureView)
        {
            return textureView._nativeTexture;
        }

        public GL33TextureView(GL33Texture texture)
        {
            _nativeTexture = texture;
        }
    }
}