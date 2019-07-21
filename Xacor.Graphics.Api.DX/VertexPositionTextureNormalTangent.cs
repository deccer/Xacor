using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Xacor.Graphics.Api.DX
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPositionTextureNormalTangent : IEquatable<VertexPositionTextureNormalTangent>
    {
        public Vector3 Position { get; set; }

        public Vector2 Uv;

        public Vector3 Normal;

        public Vector3 Tangent;

        public VertexPositionTextureNormalTangent(Vector3 position, Vector2 uv, Vector3 normal, Vector3 tangent)
        {
            Position = position;
            Uv = uv;
            Normal = normal;
            Tangent = tangent;
        }

        public bool Equals(VertexPositionTextureNormalTangent other)
        {
            return Uv.Equals(other.Uv) && Normal.Equals(other.Normal) && Tangent.Equals(other.Tangent) && Position.Equals(other.Position);
        }

        public override bool Equals(object obj)
        {
            return obj is VertexPositionTextureNormalTangent other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Uv.GetHashCode();
                hashCode = (hashCode * 397) ^ Normal.GetHashCode();
                hashCode = (hashCode * 397) ^ Tangent.GetHashCode();
                hashCode = (hashCode * 397) ^ Position.GetHashCode();
                return hashCode;
            }
        }
    }
}