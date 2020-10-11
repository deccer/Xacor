﻿using System;
using OpenTK.Graphics.OpenGL4;
using Xacor.Mathematics;
using Rectangle = System.Drawing.Rectangle;

namespace Xacor.Graphics.Api.GL46
{
    public static class GL46Extensions
    {
        public static ShaderType ToOpenTK(this ShaderStage shaderStage)
        {
            return shaderStage switch
            {
                ShaderStage.Vertex => ShaderType.VertexShader,
                ShaderStage.Pixel => ShaderType.FragmentShader,
                ShaderStage.Compute => ShaderType.ComputeShader,
                _ => throw new ArgumentOutOfRangeException(nameof(shaderStage), shaderStage, null),
            };
        }

        public static Rectangle ToOpenTK(this Viewport viewport)
        {
            return new Rectangle((int)viewport.X, (int)viewport.Y, (int)viewport.Width, (int)viewport.Height);
        }

        public static SizedInternalFormat ToOpenTK(this Format format)
        {
            return format switch
            {
                Format.R8UNorm => SizedInternalFormat.R8ui,
                Format.R8G8B8A8UNorm => SizedInternalFormat.Rgba8ui,
                Format.R16UInt => SizedInternalFormat.R16ui,
                Format.R32UInt => SizedInternalFormat.R32ui,
                Format.R16G16Float => SizedInternalFormat.Rg16f,
                Format.R16G16B16A16Float => SizedInternalFormat.Rgba16f,
                Format.R32Float => SizedInternalFormat.R32f,
                Format.R32G32Float => SizedInternalFormat.Rg32f,
                Format.R32G32B32Float => SizedInternalFormat.Rgba32f,
                Format.R32G32B32A32Float => SizedInternalFormat.Rgba32f,
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, null),
            };
        }
    }
}