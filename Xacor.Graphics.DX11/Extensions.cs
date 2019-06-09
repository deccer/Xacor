using System;
using SharpDX.Direct3D;

namespace Xacor.Graphics.DX11
{
    internal static class Extensions
    {
        public static DriverType ToSharpDX(this DeviceType deviceType)
        {
            switch (deviceType)
            {
                case DeviceType.Hardware:
                    return DriverType.Hardware;
                default:
                    return DriverType.Unknown;
            }
        }

        public static SharpDX.DXGI.SwapEffect ToSharpDX(this SwapEffect swapEffect)
        {
            switch (swapEffect)
            {
                case SwapEffect.Discard:
                    return SharpDX.DXGI.SwapEffect.Discard;
                case SwapEffect.Sequential:
                    return SharpDX.DXGI.SwapEffect.Sequential;
                case SwapEffect.FlipSequential:
                    return SharpDX.DXGI.SwapEffect.FlipSequential;
                case SwapEffect.FlipDiscard:
                    return SharpDX.DXGI.SwapEffect.FlipDiscard;
                default:
                    throw new ArgumentOutOfRangeException(nameof(swapEffect), swapEffect, null);
            }
        }

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

        // ReSharper disable once InconsistentNaming
        public static SharpDX.DXGI.Format ToSharpDX(this Format format)
        {
            switch (format)
            {
                case Format.D24UnormS8UInt:
                    return SharpDX.DXGI.Format.D24_UNorm_S8_UInt;
                case Format.D32Float:
                    return SharpDX.DXGI.Format.D32_Float;
                case Format.R8UNorm:
                    return SharpDX.DXGI.Format.R8_UNorm;
                case Format.R8G8B8A8UNorm:
                    return SharpDX.DXGI.Format.R8G8B8A8_UNorm;
                case Format.R16G16Float:
                    return SharpDX.DXGI.Format.R16G16_Float;
                case Format.R16G16B16A16Float:
                    return SharpDX.DXGI.Format.R16G16B16A16_Float;
                case Format.R32Float:
                    return SharpDX.DXGI.Format.R32_Float;
                case Format.R32G32Float:
                    return SharpDX.DXGI.Format.R32G32_Float;
                case Format.R32G32B32Float:
                    return SharpDX.DXGI.Format.R32G32B32_Float;
                case Format.R32G32B32A32Float:
                    return SharpDX.DXGI.Format.R32G32B32A32_Float;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, "Format not supported.");
            }
        }

        public static SharpDX.Direct3D.PrimitiveTopology ToSharpDX(this PrimitiveTopology primitiveTopology)
        {
            switch (primitiveTopology)
            {
                case PrimitiveTopology.TriangleList:
                    return SharpDX.Direct3D.PrimitiveTopology.TriangleList;
                case PrimitiveTopology.LineList:
                    return SharpDX.Direct3D.PrimitiveTopology.LineList;
                case PrimitiveTopology.NotAssigned:
                    return SharpDX.Direct3D.PrimitiveTopology.Undefined;
                default:
                    throw new ArgumentOutOfRangeException(nameof(primitiveTopology), primitiveTopology, null);
            }
        }
    }
}