using System.Numerics;
using System.Runtime.InteropServices;
using SharpDX.Mathematics.Interop;

namespace Xacor.Graphics.DX
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPositionColor
    {
        public Vector3 Position { get; set; }

        public RawColor4 Color { get; set; }

        public VertexPositionColor(Vector3 position, RawColor4 color)
        {
            Position = position;
            Color = color;
        }
    }
}