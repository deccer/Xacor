using System;
using SharpDX.Direct3D;
using Xacor.Mathematics;
using Xacor.Mathematics.Interop;
using Rectangle = System.Drawing.Rectangle;
using RectangleF = System.Drawing.RectangleF;

namespace Xacor.Graphics.Api.D3D
{
    internal static class DXExtensions
    {
        public static DriverType ToSharpDX(this DeviceType deviceType)
        {
            return deviceType switch
            {
                DeviceType.Hardware => DriverType.Hardware,
                DeviceType.Reference => DriverType.Reference,
                _ => DriverType.Unknown
            };
        }

        public static SharpDX.DXGI.SwapEffect ToSharpDX(this SwapEffect swapEffect)
        {
            return swapEffect switch
            {
                SwapEffect.Discard => SharpDX.DXGI.SwapEffect.Discard,
                SwapEffect.Sequential => SharpDX.DXGI.SwapEffect.Sequential,
                SwapEffect.FlipSequential => SharpDX.DXGI.SwapEffect.FlipSequential,
                SwapEffect.FlipDiscard => SharpDX.DXGI.SwapEffect.FlipDiscard,
                _ => throw new ArgumentOutOfRangeException(nameof(swapEffect), swapEffect, null),
            };
        }

        // ReSharper disable once InconsistentNaming
        public static SharpDX.DXGI.Format ToSharpDX(this Format format)
        {
            return format switch
            {
                Format.D24UnormS8UInt => SharpDX.DXGI.Format.D24_UNorm_S8_UInt,
                Format.D32Float => SharpDX.DXGI.Format.D32_Float,
                Format.R8UNorm => SharpDX.DXGI.Format.R8_UNorm,
                Format.R8G8B8A8UNorm => SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                Format.R16UInt => SharpDX.DXGI.Format.R16_UInt,
                Format.R16G16Float => SharpDX.DXGI.Format.R16G16_Float,
                Format.R16G16B16A16Float => SharpDX.DXGI.Format.R16G16B16A16_Float,
                Format.R32UInt => SharpDX.DXGI.Format.R32_UInt,
                Format.R32Float => SharpDX.DXGI.Format.R32_Float,
                Format.R32G32Float => SharpDX.DXGI.Format.R32G32_Float,
                Format.R32G32B32Float => SharpDX.DXGI.Format.R32G32B32_Float,
                Format.R32G32B32A32Float => SharpDX.DXGI.Format.R32G32B32A32_Float,
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, "Format not supported."),
            };
        }

        public static SharpDX.Direct3D.PrimitiveTopology ToSharpDX(this PrimitiveTopology primitiveTopology)
        {
            return primitiveTopology switch
            {
                PrimitiveTopology.TriangleList => SharpDX.Direct3D.PrimitiveTopology.TriangleList,
                PrimitiveTopology.LineList => SharpDX.Direct3D.PrimitiveTopology.LineList,
                PrimitiveTopology.NotAssigned => SharpDX.Direct3D.PrimitiveTopology.Undefined,
                _ => throw new ArgumentOutOfRangeException(nameof(primitiveTopology), primitiveTopology, null),
            };
        }

        public static RawViewportF ToSharpDX(this Viewport viewport)
        {
            RawViewportF nativeViewport;
            nativeViewport.X = viewport.X;
            nativeViewport.Y = viewport.Y;
            nativeViewport.Width = viewport.Width;
            nativeViewport.Height = viewport.Height;
            nativeViewport.MaxDepth = viewport.MaxDepth;
            nativeViewport.MinDepth = viewport.MinDepth;
            return nativeViewport;
        }

        public static RawRectangleF ToSharpDX(this RectangleF rectangle)
        {
            RawRectangleF nativeRectangle;
            nativeRectangle.Left = rectangle.Left;
            nativeRectangle.Right = rectangle.Right;
            nativeRectangle.Bottom = rectangle.Bottom;
            nativeRectangle.Top = rectangle.Top;
            return nativeRectangle;
        }

        public static RawRectangle ToSharpDX(this Rectangle rectangle)
        {
            RawRectangle nativeRectangle;
            nativeRectangle.Left = rectangle.Left;
            nativeRectangle.Right = rectangle.Right;
            nativeRectangle.Bottom = rectangle.Bottom;
            nativeRectangle.Top = rectangle.Top;
            return nativeRectangle;
        }
    }
}