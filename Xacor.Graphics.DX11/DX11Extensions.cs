using System;

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
    }
}