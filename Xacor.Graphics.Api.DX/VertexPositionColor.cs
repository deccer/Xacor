using System.Numerics;
using System.Runtime.InteropServices;

namespace Xacor.Graphics.Api.DX
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPositionColor
    {
        public Vector3 Position { get; set; }

        public Vector4 Color { get; set; }

        public VertexPositionColor(Vector3 position, Vector4 color)
        {
            Position = position;
            Color = color;
        }
    }
}