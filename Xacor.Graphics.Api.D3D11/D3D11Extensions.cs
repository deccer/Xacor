using System;
using SharpDX.Direct3D11;

namespace Xacor.Graphics.Api.D3D11
{
    internal static class D3D11Extensions
    {
        // ReSharper disable once InconsistentNaming

        public static SharpDX.Direct3D11.CullMode ToSharpDX(this CullMode cullMode)
        {
            return cullMode switch
            {
                CullMode.Front => SharpDX.Direct3D11.CullMode.Front,
                CullMode.Back => SharpDX.Direct3D11.CullMode.Back,
                CullMode.None => SharpDX.Direct3D11.CullMode.None,
                _ => throw new ArgumentOutOfRangeException(nameof(cullMode), cullMode, null),
            };
        }

        // ReSharper disable once InconsistentNaming

        public static SharpDX.Direct3D11.FillMode ToSharpDX(this FillMode fillMode)
        {
            return fillMode switch
            {
                FillMode.Solid => SharpDX.Direct3D11.FillMode.Solid,
                FillMode.Wireframe => SharpDX.Direct3D11.FillMode.Wireframe,
                _ => throw new ArgumentOutOfRangeException(nameof(fillMode), fillMode, null),
            };
        }

        public static SharpDX.Direct3D11.BlendOperation ToSharpDX(this BlendOperation blendOperation)
        {
            return blendOperation switch
            {
                BlendOperation.Add => SharpDX.Direct3D11.BlendOperation.Add,
                BlendOperation.Subtract => SharpDX.Direct3D11.BlendOperation.Subtract,
                BlendOperation.Min => SharpDX.Direct3D11.BlendOperation.Minimum,
                BlendOperation.Max => SharpDX.Direct3D11.BlendOperation.Maximum,
                _ => throw new ArgumentOutOfRangeException(nameof(blendOperation), blendOperation, null),
            };
        }

        public static BlendOption ToSharpDX(this Blend blend)
        {
            return blend switch
            {
                Blend.Zero => BlendOption.Zero,
                Blend.One => BlendOption.One,
                Blend.SourceColor => BlendOption.SourceColor,
                Blend.InverseSourceColor => BlendOption.InverseSourceColor,
                Blend.SourceAlpha => BlendOption.SourceAlpha,
                Blend.InverseSourceAlpha => BlendOption.InverseSourceAlpha,
                _ => throw new ArgumentOutOfRangeException(nameof(blend), blend, null),
            };
        }

        public static SharpDX.Direct3D11.Filter ToSharpDX(this Filter filter)
        {
            return filter switch
            {
                Filter.Nearest => SharpDX.Direct3D11.Filter.MinMagMipPoint,
                Filter.Linear => SharpDX.Direct3D11.Filter.MinMagMipLinear,
                Filter.Anisotropic => SharpDX.Direct3D11.Filter.Anisotropic,
                _ => throw new ArgumentOutOfRangeException(nameof(filter), filter, null),
            };
        }

        public static Comparison ToSharpDX(this ComparisonFunction comparisonFunction)
        {
            return comparisonFunction switch
            {
                ComparisonFunction.Never => Comparison.Never,
                ComparisonFunction.Less => Comparison.Less,
                ComparisonFunction.Equal => Comparison.Equal,
                ComparisonFunction.LessEqual => Comparison.LessEqual,
                ComparisonFunction.Greater => Comparison.Greater,
                ComparisonFunction.NotEqual => Comparison.NotEqual,
                ComparisonFunction.GreaterEqual => Comparison.GreaterEqual,
                ComparisonFunction.Always => Comparison.Always,
                _ => throw new ArgumentOutOfRangeException(nameof(comparisonFunction), comparisonFunction, null),
            };
        }

        public static SharpDX.Direct3D11.TextureAddressMode ToSharpDX(this TextureAddressMode textureAddressMode)
        {
            return textureAddressMode switch
            {
                TextureAddressMode.Border => SharpDX.Direct3D11.TextureAddressMode.Border,
                TextureAddressMode.Clamp => SharpDX.Direct3D11.TextureAddressMode.Clamp,
                TextureAddressMode.Mirror => SharpDX.Direct3D11.TextureAddressMode.Mirror,
                TextureAddressMode.Wrap => SharpDX.Direct3D11.TextureAddressMode.Wrap,
                _ => throw new ArgumentOutOfRangeException(nameof(textureAddressMode), textureAddressMode, null),
            };
        }
    }
}