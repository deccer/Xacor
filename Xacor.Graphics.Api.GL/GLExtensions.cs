using System;
using OpenTK.Graphics.OpenGL4;

namespace Xacor.Graphics.Api.GL
{
    public static class GLExtensions
    {
        public static TextureMagFilter MagFilterToOpenTK(this Filter filter)
        {
            return filter switch
            {
                Filter.Nearest => TextureMagFilter.Nearest,
                Filter.Linear => TextureMagFilter.Linear,
                _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, null),
            };
        }

        public static TextureMinFilter MinFilterToOpenTK(this Filter filter)
        {
            return filter switch
            {
                Filter.Nearest => TextureMinFilter.Nearest,
                Filter.Linear => TextureMinFilter.Linear,
                _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, null),
            };
        }

        public static TextureWrapMode ToOpenTK(this TextureAddressMode textureAddressMode)
        {
            return textureAddressMode switch
            {
                TextureAddressMode.Border => TextureWrapMode.ClampToBorder,
                TextureAddressMode.Clamp => TextureWrapMode.ClampToEdge,
                TextureAddressMode.Mirror => TextureWrapMode.MirroredRepeat,
                TextureAddressMode.Wrap => TextureWrapMode.Repeat,
                _ => throw new ArgumentOutOfRangeException(nameof(textureAddressMode), textureAddressMode, null),
            };
        }

        public static PrimitiveType ToOpenTK(this PrimitiveTopology primitiveTopology)
        {
            return primitiveTopology switch
            {
                PrimitiveTopology.TriangleList => PrimitiveType.Triangles,
                PrimitiveTopology.LineList => PrimitiveType.LineLoop,
                PrimitiveTopology.NotAssigned => PrimitiveType.Triangles,
                _ => throw new ArgumentOutOfRangeException(nameof(primitiveTopology), primitiveTopology, null),
            };
        }
    }
}