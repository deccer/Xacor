using System;
using System.Runtime.InteropServices;
using Xacor.Mathematics;

namespace Xacor.Graphics.Api
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct VertexPositionTexture : IEquatable<VertexPositionTexture>
    {
        public readonly Vector3 Position;

        public readonly Vector2 Uv;

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