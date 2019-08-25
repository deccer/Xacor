using System;

namespace Xacor.Math
{
    public readonly struct Quaternion : IEquatable<Quaternion>
    {
        public static readonly Quaternion Identity;

        static Quaternion()
        {
            Identity = new Quaternion(0, 0, 0, 1);
        }

        public readonly float X;
        public readonly float Y;
        public readonly float Z;
        public readonly float W;

        public static Quaternion FromAngleAxis(float angle, Vector3 axis)
        {
            var half = angle * 0.5f;
            var sin = (float)System.Math.Sin(half);
            var cos = (float)System.Math.Cos(half);
            return new Quaternion(axis.X * sin, axis.Y * sin, axis.Z * sin, cos);
        }

        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public bool Equals(Quaternion other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        }

        public override bool Equals(object obj)
        {
            return obj is Quaternion other && Equals(other);
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