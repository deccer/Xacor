using System;

namespace Xacor.Math
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public static readonly Vector2 One = new Vector2(1.0f);
        public static readonly Vector2 Zero = new Vector2(0.0f);

        public readonly float X;
        public readonly float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2(float f)
        {
            X = f;
            Y = f;
        }

        public bool Equals(Vector2 other)
        {
            return X.Equals(other.X)
                   && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }
    }
}