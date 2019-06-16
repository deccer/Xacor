namespace Xacor.Graphics.GL46
{
    internal class GL46TextureView : TextureView
    {
        private readonly int _nativeTexture;

        public static implicit operator int(GL46TextureView textureView)
        {
            return textureView._nativeTexture;
        }

        public GL46TextureView(GL46Texture texture)
        {
            _nativeTexture = texture;
        }
    }
}