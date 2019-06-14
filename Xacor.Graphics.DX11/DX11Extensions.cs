using System;
using SharpDX.Direct3D11;

namespace Xacor.Graphics.DX11
{
    internal static class DX11Extensions
    {
        // ReSharper disable once InconsistentNaming

        public static SharpDX.Direct3D11.CullMode ToSharpDX(this CullMode cullMode)
        {
            switch (cullMode)
            {
                case CullMode.Front:
                    return SharpDX.Direct3D11.CullMode.Front;
                case CullMode.Back:
                    return SharpDX.Direct3D11.CullMode.Back;
                case CullMode.None:
                    return SharpDX.Direct3D11.CullMode.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cullMode), cullMode, null);
            }
        }

        // ReSharper disable once InconsistentNaming

        public static SharpDX.Direct3D11.FillMode ToSharpDX(this FillMode fillMode)
        {
            switch (fillMode)
            {
                case FillMode.Solid:
                    return SharpDX.Direct3D11.FillMode.Solid;
                case FillMode.Wireframe:
                    return SharpDX.Direct3D11.FillMode.Wireframe;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fillMode), fillMode, null);
            }
        }

        public static SharpDX.Direct3D11.BlendOperation ToSharpDX(this BlendOperation blendOperation)
        {
            switch (blendOperation)
            {
                case BlendOperation.Add:
                    return SharpDX.Direct3D11.BlendOperation.Add;
                case BlendOperation.Subtract:
                    return SharpDX.Direct3D11.BlendOperation.Subtract;
                case BlendOperation.Min:
                    return SharpDX.Direct3D11.BlendOperation.Minimum;
                case BlendOperation.Max:
                    return SharpDX.Direct3D11.BlendOperation.Maximum;
                default:
                    throw new ArgumentOutOfRangeException(nameof(blendOperation), blendOperation, null);
            }
        }

        public static BlendOption ToSharpDX(this Blend blend)
        {
            switch (blend)
            {
                case Blend.Zero:
                    return BlendOption.Zero;
                case Blend.One:
                    return BlendOption.One;
                case Blend.SourceColor:
                    return BlendOption.SourceColor;
                case Blend.InverseSourceColor:
                    return BlendOption.InverseSourceColor;
                case Blend.SourceAlpha:
                    return BlendOption.SourceAlpha;
                case Blend.InverseSourceAlpha:
                    return BlendOption.InverseSourceAlpha;
                default:
                    throw new ArgumentOutOfRangeException(nameof(blend), blend, null);
            }
        }
    }
}