using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Xacor.Graphics.Api
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPosition  : IEquatable<VertexPosition>
    {
        public Vector3 Position { get; set; }

        public VertexPosition(Vector3 position)
        {
            Position = position;
        }

        public bool Equals(VertexPosition other)
        {
            return Position.Equals(other.Position);
        }

        public override bool Equals(object obj)
        {
            return obj is VertexPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}