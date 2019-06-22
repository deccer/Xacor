using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Xacor.Graphics.Api.GL46
{
    public static class GL46Extensions
    {
        public static ShaderType ToOpenTK(this ShaderStage shaderStage)
        {
            switch (shaderStage)
            {
                case ShaderStage.Vertex:
                    return ShaderType.VertexShader;
                case ShaderStage.Pixel:
                    return ShaderType.FragmentShader;
                case ShaderStage.Compute:
                    return ShaderType.ComputeShader;
                default:
                    throw new ArgumentOutOfRangeException(nameof(shaderStage), shaderStage, null);
            }
        }

        public static Rectangle ToOpenTK(this Viewport viewport)
        {
            return new Rectangle((int)viewport.X, (int)viewport.Y, (int)viewport.Width, (int)viewport.Height);
        }

        public static SizedInternalFormat ToOpenTK(this Format format)
        {
            switch (format)
            {
                case Format.R8UNorm:
                    return SizedInternalFormat.R8ui;
                case Format.R8G8B8A8UNorm:
                    return SizedInternalFormat.Rgba16ui;
                case Format.R16UInt:
                    return SizedInternalFormat.R16ui;
                case Format.R32UInt:
                    return SizedInternalFormat.R32ui;
                case Format.R16G16Float:
                    return SizedInternalFormat.Rg16f;
                case Format.R16G16B16A16Float:
                    return SizedInternalFormat.Rgba16f;
                case Format.R32Float:
                    return SizedInternalFormat.R32f;
                case Format.R32G32Float:
                    return SizedInternalFormat.Rg32f;
                case Format.R32G32B32Float:
                    return SizedInternalFormat.Rgba32f;
                case Format.R32G32B32A32Float:
                    return SizedInternalFormat.Rgba32f;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }

        public static TextureMagFilter MagFilterToOpenTK(this Filter filter)
        {
            switch (filter)
            {
                case Filter.Nearest:
                    return TextureMagFilter.Nearest;
                case Filter.Linear:
                    return TextureMagFilter.Linear;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, null);
            }
        }

        public static TextureMinFilter MinFilterToOpenTK(this Filter filter)
        {
            switch (filter)
            {
                case Filter.Nearest:
                    return TextureMinFilter.Nearest;
                case Filter.Linear:
                    return TextureMinFilter.Linear;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filter), filter, null);
            }
        }

        public static TextureWrapMode ToOpenTK(this TextureAddressMode textureAddressMode)
        {
            switch (textureAddressMode)
            {
                case TextureAddressMode.Border:
                    return TextureWrapMode.ClampToBorder;
                case TextureAddressMode.Clamp:
                    return TextureWrapMode.ClampToEdge;
                case TextureAddressMode.Mirror:
                    return TextureWrapMode.MirroredRepeat;
                case TextureAddressMode.Wrap:
                    return TextureWrapMode.Repeat;
                default:
                    throw new ArgumentOutOfRangeException(nameof(textureAddressMode), textureAddressMode, null);
            }
        }

        public static PrimitiveType ToOpenTK(this PrimitiveTopology primitiveTopology)
        {
            switch (primitiveTopology)
            {
                case PrimitiveTopology.TriangleList:
                    return PrimitiveType.Triangles;
                case PrimitiveTopology.LineList:
                    return PrimitiveType.LineLoop;
                case PrimitiveTopology.NotAssigned:
                    return PrimitiveType.Triangles;
                default:
                    throw new ArgumentOutOfRangeException(nameof(primitiveTopology), primitiveTopology, null);
            }
        }
    }
}