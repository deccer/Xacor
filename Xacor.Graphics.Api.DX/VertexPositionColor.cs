using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Xacor.Graphics.Api.DX
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPositionColor : IEquatable<VertexPositionColor>
    {
        public Vector3 Position { get; set; }

        public Vector4 Color { get; set; }

        public VertexPositionColor(Vector3 position, Vector4 color)
        {
            Position = position;
            Color = color;
        }

        public bool Equals(VertexPositionColor other)
        {
            return Position.Equals(other.Position) && Color.Equals(other.Color);
        }

        public override bool Equals(object obj)
        {
            return obj is VertexPositionColor other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Position.GetHashCode() * 397) ^ Color.GetHashCode();
            }
        }
    }
}