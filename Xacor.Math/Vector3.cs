using System;

namespace Xacor.Math
{
    public readonly struct Vector3 : IEquatable<Vector3>
    {
        public static readonly Vector3 One = new Vector3(1.0f);
        public static readonly Vector3 Zero = new Vector3(0.0f);
        public static readonly Vector3 UnitX = new Vector3(1.0f, 0.0f, 0.0f);
        public static readonly Vector3 UnitY = new Vector3(0.0f, 1.0f, 0.0f);
        public static readonly Vector3 UnitZ = new Vector3(0.0f, 0.0f, 1.0f);

        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }

        public static Vector3 operator *(Vector3 v1, float scalar)
        {
            return new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
        }

        public static Vector3 operator *(float scalar, Vector3 v1)
        {
            return new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator +(Vector3 v1, float scalar)
        {
            return new Vector3(v1.X + scalar, v1.Y + scalar, v1.Z + scalar);
        }

        public static Vector3 operator +(float scalar, Vector3 v1)
        {
            return new Vector3(v1.X + scalar, v1.Y + scalar, v1.Z + scalar);
        }

        public static Vector3 operator -(Vector3 v1)
        {
            return new Vector3(-v1.X, -v1.Y, -v1.Z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3 operator -(Vector3 v1, float scalar)
        {
            return new Vector3(v1.X - scalar, v1.Y - scalar, v1.Z - scalar);
        }

        public static Vector3 operator -(float scalar, Vector3 v1)
        {
            return new Vector3(v1.X - scalar, v1.Y - scalar, v1.Z - scalar);
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            var x = v1.Y * v2.Z - v2.Y * v1.Z;
            var y = -(v1.X * v2.Z - v2.X * v1.Z);
            var z = v1.X * v2.Y - v2.X * v1.Y;
            return new Vector3(x, y, z);
        }

        public Vector3 Cross(Vector3 v2)
        {
            return Cross(this, v2);
        }

        public static float Dot(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public float Dot(Vector3 v2)
        {
            return X * v2.X + Y * v2.Y + Z * v2.Z;
        }

        public float Length()
        {
            return LengthSquared();
        }

        public float LengthSquared()
        {
            return (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public static Vector3 Normalize(ref Vector3 vector)
        {
            return vector.Normalized();
        }

        public static Vector3 Normalize(Vector3 vector)
        {
            return vector.Normalized();
        }

        public Vector3 Normalized()
        {
            var length = Length();
            var scale = 1.0f / length;
            return new Vector3(X * scale, Y * scale, Z * scale);
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(float f)
        {
            X = f;
            Y = f;
            Z = f;
        }

        public bool Equals(Vector3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }
    }
}
