using System;

namespace Xacor.Graphics
{
    public struct Viewport : IEquatable<Viewport>
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Width;
        public readonly float Height;
        public readonly float MinDepth;
        public readonly float MaxDepth;

        public bool Equals(Viewport other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Width.Equals(other.Width) && Height.Equals(other.Height) && MinDepth.Equals(other.MinDepth) && MaxDepth.Equals(other.MaxDepth);
        }

        public override bool Equals(object obj)
        {
            return obj is Viewport other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                hashCode = (hashCode * 397) ^ MinDepth.GetHashCode();
                hashCode = (hashCode * 397) ^ MaxDepth.GetHashCode();
                return hashCode;
            }
        }

        public Viewport(float x, float y, float width, float height, float minDepth, float maxDepth)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            MinDepth = minDepth;
            MaxDepth = maxDepth;
        }
    }
}