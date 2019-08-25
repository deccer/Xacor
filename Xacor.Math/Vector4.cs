using System;

namespace Xacor.Math
{
    public readonly struct Vector4 : IEquatable<Vector4>
    {
        public static readonly Vector3 One = new Vector3(1.0f);
        public static readonly Vector3 Zero = new Vector3(0.0f);
        public static readonly Vector3 UnitX = new Vector3(1.0f, 0.0f, 0.0f);
        public static readonly Vector3 UnitY = new Vector3(0.0f, 1.0f, 0.0f);
        public static readonly Vector3 UnitZ = new Vector3(0.0f, 0.0f, 1.0f);

        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float W;

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4(float f)
        {
            X = f;
            Y = f;
            Z = f;
            W = 1.0f;
        }

        public bool Equals(Vector4 other)
        {
            return X.Equals(other.X) 
                   && Y.Equals(other.Y) 
                   && Z.Equals(other.Z)
                   && W.Equals(other.W);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector4 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                return hashCode;
            }
        }
    }
}