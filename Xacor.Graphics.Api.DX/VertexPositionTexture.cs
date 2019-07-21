using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Xacor.Graphics.Api.DX
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPositionTexture : IEquatable<VertexPositionTexture>
    {
        public Vector3 Position { get; set; }

        public Vector2 Uv { get; set; }

        public VertexPositionTexture(Vector3 position, Vector2 uv)
        {
            Position = position;
            Uv = uv;
        }

        public bool Equals(VertexPositionTexture other)
        {
            return Position.Equals(other.Position) && Uv.Equals(other.Uv);
        }

        public override bool Equals(object obj)
        {
            return obj is VertexPositionTexture other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Position.GetHashCode() * 397) ^ Uv.GetHashCode();
            }
        }
    }
}