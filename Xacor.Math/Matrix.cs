namespace Xacor.Math
{
    public readonly struct Matrix
    {
        public static readonly Matrix Identity;

        static Matrix()
        {
            Identity = new Matrix(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
        }

        private const float Tolerance = 0.0001f;

        private readonly float _m00;
        private readonly float _m01;
        private readonly float _m02;
        private readonly float _m03;
        private readonly float _m10;
        private readonly float _m11;
        private readonly float _m12;
        private readonly float _m13;
        private readonly float _m20;
        private readonly float _m21;
        private readonly float _m22;
        private readonly float _m23;
        private readonly float _m30;
        private readonly float _m31;
        private readonly float _m32;
        private readonly float _m33;

        public static Matrix operator *(Matrix lhs, Matrix rhs)
        {
            return new Matrix(
                lhs._m00 * rhs._m00 + lhs._m01 * rhs._m10 + lhs._m02 * rhs._m20 + lhs._m03 * rhs._m30,
                lhs._m00 * rhs._m01 + lhs._m01 * rhs._m11 + lhs._m02 * rhs._m21 + lhs._m03 * rhs._m31,
                lhs._m00 * rhs._m02 + lhs._m01 * rhs._m12 + lhs._m02 * rhs._m22 + lhs._m03 * rhs._m32,
                lhs._m00 * rhs._m03 + lhs._m01 * rhs._m13 + lhs._m02 * rhs._m23 + lhs._m03 * rhs._m33,
                lhs._m10 * rhs._m00 + lhs._m11 * rhs._m10 + lhs._m12 * rhs._m20 + lhs._m13 * rhs._m30,
                lhs._m10 * rhs._m01 + lhs._m11 * rhs._m11 + lhs._m12 * rhs._m21 + lhs._m13 * rhs._m31,
                lhs._m10 * rhs._m02 + lhs._m11 * rhs._m12 + lhs._m12 * rhs._m22 + lhs._m13 * rhs._m32,
                lhs._m10 * rhs._m03 + lhs._m11 * rhs._m13 + lhs._m12 * rhs._m23 + lhs._m13 * rhs._m33,
                lhs._m20 * rhs._m00 + lhs._m21 * rhs._m10 + lhs._m22 * rhs._m20 + lhs._m23 * rhs._m30,
                lhs._m20 * rhs._m01 + lhs._m21 * rhs._m11 + lhs._m22 * rhs._m21 + lhs._m23 * rhs._m31,
                lhs._m20 * rhs._m02 + lhs._m21 * rhs._m12 + lhs._m22 * rhs._m22 + lhs._m23 * rhs._m32,
                lhs._m20 * rhs._m03 + lhs._m21 * rhs._m13 + lhs._m22 * rhs._m23 + lhs._m23 * rhs._m33,
                lhs._m30 * rhs._m00 + lhs._m31 * rhs._m10 + lhs._m32 * rhs._m20 + lhs._m33 * rhs._m30,
                lhs._m30 * rhs._m01 + lhs._m31 * rhs._m11 + lhs._m32 * rhs._m21 + lhs._m33 * rhs._m31,
                lhs._m30 * rhs._m02 + lhs._m31 * rhs._m12 + lhs._m32 * rhs._m22 + lhs._m33 * rhs._m32,
                lhs._m30 * rhs._m03 + lhs._m31 * rhs._m13 + lhs._m32 * rhs._m23 + lhs._m33 * rhs._m33);
        }

        public static Matrix CreateRotation(Quaternion orientation)
        {
            var num9 = orientation.X * orientation.X;
            var num8 = orientation.Y * orientation.Y;
            var num7 = orientation.Z * orientation.Z;
            var num6 = orientation.X * orientation.Y;
            var num5 = orientation.Z * orientation.W;
            var num4 = orientation.Z * orientation.X;
            var num3 = orientation.Y * orientation.W;
            var num2 = orientation.Y * orientation.Z;
            var num = orientation.X * orientation.W;

            return new Matrix(
                1.0f - (2.0f * (num8 + num7)),
                2.0f * (num6 + num5),
                2.0f * (num4 - num3),
                0.0f,
                2.0f * (num6 - num5),
                1.0f - (2.0f * (num7 + num9)),
                2.0f * (num2 + num),
                0.0f,
                2.0f * (num4 + num3),
                2.0f * (num2 - num),
                1.0f - (2.0f * (num8 + num9)),
                0.0f,
                0.0f,
                0.0f,
                0.0f,
                1.0f
            );
        }

        public static Matrix CreateTranslation(Vector3 position)
        {
            var matrix = new Matrix(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                position.X, position.Y, position.Z, 1.0f);
            return matrix;
        }

        public static Matrix CreateTranslation(float x, float y, float z)
        {
            var matrix = new Matrix(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                x, y, z, 1.0f);
            return matrix;
        }

        public static Matrix CreateScale(float scale)
        {
            return CreateScale(scale, scale, scale);
        }

        public static Matrix CreateScale(Vector3 scale)
        {
            return CreateScale(scale.X, scale.Y, scale.Z);
        }

        public static Matrix CreateScale(float scaleX, float scaleY, float scaleZ)
        {
            return new Matrix(
                scaleX, 0, 0, 0,
                0, scaleY, 0, 0,
                0, 0, scaleZ, 0,
                0, 0, 0, 1);
        }

        public static Matrix CreateLookAtLH(Vector3 cameraPosition, Vector3 target, Vector3 up)
        {
            var zAxis = Vector3.Normalize(target - cameraPosition);
            var xAxis = Vector3.Normalize(Vector3.Cross(up, zAxis));
            var yAxis = Vector3.Cross(zAxis, xAxis);

            return new Matrix(
                xAxis.X, yAxis.X, zAxis.X, 0.0f,
                xAxis.Y, yAxis.Y, zAxis.Y, 0.0f,
                xAxis.Z, yAxis.Z, zAxis.Z, 0.0f,
                -Vector3.Dot(xAxis, cameraPosition), -Vector3.Dot(yAxis, cameraPosition), -Vector3.Dot(zAxis, cameraPosition), 1.0f);
        }

        public static Matrix CreateOrthographicLH(float width, float height, float zNear, float zFar)
        {
            return new Matrix(
                2.0f / width, 0, 0, 0,
                0, 2.0f / height, 0, 0,
                0, 0, 1 - (zFar - zNear), 0,
                0, 0, zNear / (zNear - zFar), 1);
        }

        public static Matrix CreateOrthographicOffCenterLH(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            return new Matrix(
                2.0f / (right - left), 0, 0, 0,
                0, 2.0f / (top - bottom), 0, 0,
                0, 0, 1 - (zFar - zNear), 0,
                (left + right) / (left - right), (top + bottom) / (bottom - top), zNear / (zNear - zFar), 1.0f);
        }

        public static Matrix CreatePerspectiveFieldOfViewLH(float fieldOfView, float aspectRatio, float zNear, float zFar)
        {
            var yScale = 1.0f / (float)System.Math.Tan(fieldOfView / 2.0f);
            var xScale = yScale / aspectRatio;

            return new Matrix(
                xScale, 0, 0, 0,
                0, yScale, 0, 0,
                0, 0, zFar / (zFar - zNear), 1.0f,
                0, 0, -zNear * zFar / (zFar - zNear), 0.0f);
        }

        public Vector3 Transform(Vector3 vector)
        {
            var x = vector.X * _m00 + vector.Y * _m10 + vector.Z * _m20 + _m30;
            var y = vector.X * _m01 + vector.Y * _m11 + vector.Z * _m21 + _m31;
            var z = vector.X * _m02 + vector.Y * _m12 + vector.Z * _m22 + _m32;
            var w = 1 / (vector.X * _m03 + vector.Y * _m13 + vector.Z * _m23 + _m33);

            return new Vector3(x * w, y * w, z * w);
        }

        public static Matrix Invert(Matrix matrix)
        {
            var v0 = matrix._m20 * matrix._m31 - matrix._m21 * matrix._m30;
            var v1 = matrix._m20 * matrix._m32 - matrix._m22 * matrix._m30;
            var v2 = matrix._m20 * matrix._m33 - matrix._m23 * matrix._m30;
            var v3 = matrix._m21 * matrix._m32 - matrix._m22 * matrix._m31;
            var v4 = matrix._m21 * matrix._m33 - matrix._m23 * matrix._m31;
            var v5 = matrix._m22 * matrix._m33 - matrix._m23 * matrix._m32;

            var i00 = (v5 * matrix._m11 - v4 * matrix._m12 + v3 * matrix._m13);
            var i10 = -(v5 * matrix._m10 - v2 * matrix._m12 + v1 * matrix._m13);
            var i20 = (v4 * matrix._m10 - v2 * matrix._m11 + v0 * matrix._m13);
            var i30 = -(v3 * matrix._m10 - v1 * matrix._m11 + v0 * matrix._m12);

            var invDet = 1.0f / (i00 * matrix._m00 + i10 * matrix._m01 + i20 * matrix._m02 + i30 * matrix._m03);

            i00 *= invDet;
            i10 *= invDet;
            i20 *= invDet;
            i30 *= invDet;

            var i01 = -(v5 * matrix._m01 - v4 * matrix._m02 + v3 * matrix._m03) * invDet;
            var i11 = (v5 * matrix._m00 - v2 * matrix._m02 + v1 * matrix._m03) * invDet;
            var i21 = -(v4 * matrix._m00 - v2 * matrix._m01 + v0 * matrix._m03) * invDet;
            var i31 = (v3 * matrix._m00 - v1 * matrix._m01 + v0 * matrix._m02) * invDet;

            v0 = matrix._m10 * matrix._m31 - matrix._m11 * matrix._m30;
            v1 = matrix._m10 * matrix._m32 - matrix._m12 * matrix._m30;
            v2 = matrix._m10 * matrix._m33 - matrix._m13 * matrix._m30;
            v3 = matrix._m11 * matrix._m32 - matrix._m12 * matrix._m31;
            v4 = matrix._m11 * matrix._m33 - matrix._m13 * matrix._m31;
            v5 = matrix._m12 * matrix._m33 - matrix._m13 * matrix._m32;

            var i02 = (v5 * matrix._m01 - v4 * matrix._m02 + v3 * matrix._m03) * invDet;
            var i12 = -(v5 * matrix._m00 - v2 * matrix._m02 + v1 * matrix._m03) * invDet;
            var i22 = (v4 * matrix._m00 - v2 * matrix._m01 + v0 * matrix._m03) * invDet;
            var i32 = -(v3 * matrix._m00 - v1 * matrix._m01 + v0 * matrix._m02) * invDet;

            v0 = matrix._m21 * matrix._m10 - matrix._m20 * matrix._m11;
            v1 = matrix._m22 * matrix._m10 - matrix._m20 * matrix._m12;
            v2 = matrix._m23 * matrix._m10 - matrix._m20 * matrix._m13;
            v3 = matrix._m22 * matrix._m11 - matrix._m21 * matrix._m12;
            v4 = matrix._m23 * matrix._m11 - matrix._m21 * matrix._m13;
            v5 = matrix._m23 * matrix._m12 - matrix._m22 * matrix._m13;

            var i03 = -(v5 * matrix._m01 - v4 * matrix._m02 + v3 * matrix._m03) * invDet;
            var i13 = (v5 * matrix._m00 - v2 * matrix._m02 + v1 * matrix._m03) * invDet;
            var i23 = -(v4 * matrix._m00 - v2 * matrix._m01 + v0 * matrix._m03) * invDet;
            var i33 = (v3 * matrix._m00 - v1 * matrix._m01 + v0 * matrix._m02) * invDet;

            return new Matrix(
                i00, i01, i02, i03,
                i10, i11, i12, i13,
                i20, i21, i22, i23,
                i30, i31, i32, i33);
        }

        private Quaternion GetRotation()
        {
            var scale = GetScale();
            if (System.Math.Abs(scale.X) < Tolerance || System.Math.Abs(scale.Y) < Tolerance || System.Math.Abs(scale.Z) < Tolerance)
            {
                return new Quaternion(0, 0, 0, 1.0f);
            }

            var normalized = new Matrix(
                _m00 / scale.X, _m01 / scale.X, _m02 / scale.X, 1.0f,
                _m10 / scale.Y, _m11 / scale.Y, _m12 / scale.Y, 1.0f,
                _m20 / scale.Z, _m21 / scale.Z, _m22 / scale.Z, 1.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
            return RotationMatrixToQuaternion(normalized);
        }

        private Vector3 GetScale()
        {
            var xs = System.Math.Sign(_m00 * _m01 * _m02 * _m03) < 0 ? -1 : 1;
            var ys = System.Math.Sign(_m10 * _m11 * _m12 * _m13) < 0 ? -1 : 1;
            var zs = System.Math.Sign(_m20 * _m21 * _m22 * _m23) < 0 ? -1 : 1;

            return new Vector3(
                xs * (float)System.Math.Sqrt(_m00 * _m00 + _m01 * _m01 + _m02 * _m02),
                ys * (float)System.Math.Sqrt(_m10 * _m10 + _m11 * _m11 + _m12 * _m12),
                zs * (float)System.Math.Sqrt(_m20 * _m20 + _m21 * _m21 + _m22 * _m22)
            );
        }

        public Vector3 GetTranslation()
        {
            return new Vector3(_m30, _m31, _m32);
        }

        public void Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            translation = GetTranslation();
            rotation = GetRotation();
            scale = GetScale();
        }

        private static Quaternion RotationMatrixToQuaternion(Matrix rotationMatrix)
        {
            float sqrt;
            float half;
            var scale = rotationMatrix._m00 + rotationMatrix._m11 + rotationMatrix._m22;

            if (scale > 0.0f)
            {
                sqrt = (float)System.Math.Sqrt(scale + 1.0f);
                half = 0.5f / sqrt;

                return new Quaternion(
                    (rotationMatrix._m12 - rotationMatrix._m21) * half,
                    (rotationMatrix._m20 - rotationMatrix._m02) * half,
                    (rotationMatrix._m01 - rotationMatrix._m10) * half,
                    sqrt);
            }

            if (rotationMatrix._m00 >= rotationMatrix._m11 && rotationMatrix._m00 >= rotationMatrix._m22)
            {
                sqrt = (float)System.Math.Sqrt(1.0f + rotationMatrix._m00 - rotationMatrix._m11 - rotationMatrix._m22);
                half = 0.5f / sqrt;

                return new Quaternion(
                    0.5f * sqrt,
                    (rotationMatrix._m01 - rotationMatrix._m10) * half,
                    (rotationMatrix._m02 - rotationMatrix._m20) * half,
                    (rotationMatrix._m12 - rotationMatrix._m21) * half);
            }

            if (rotationMatrix._m11 > rotationMatrix._m22)
            {
                sqrt = (float)System.Math.Sqrt(1.0f + rotationMatrix._m11 - rotationMatrix._m00 - rotationMatrix._m22);
                half = 0.5f / sqrt;

                return new Quaternion(
                    (rotationMatrix._m10 - rotationMatrix._m01) * half,
                    0.5f * sqrt,
                    (rotationMatrix._m21 - rotationMatrix._m12) * half,
                    (rotationMatrix._m20 - rotationMatrix._m02) * half);
            }

            sqrt = (float)System.Math.Sqrt(1.0f + rotationMatrix._m22 - rotationMatrix._m00 - rotationMatrix._m11);
            half = 0.5f / sqrt;

            return new Quaternion(
                (rotationMatrix._m20 - rotationMatrix._m02) * half,
                (rotationMatrix._m21 - rotationMatrix._m12) * half,
                0.5f * sqrt,
                (rotationMatrix._m01 - rotationMatrix._m10) * half);
        }

        public Matrix(Vector3 scale, Quaternion rotation, Vector3 translation)
        {
            var rotationMatrix = CreateRotation(rotation);
            _m00 = scale.X * rotationMatrix._m00;
            _m01 = scale.X * rotationMatrix._m01;
            _m02 = scale.X * rotationMatrix._m02;
            _m03 = 0.0f;

            _m10 = scale.Y * rotationMatrix._m10;
            _m11 = scale.Y * rotationMatrix._m11;
            _m12 = scale.Y * rotationMatrix._m12;
            _m13 = 0.0f;

            _m20 = scale.Z * rotationMatrix._m20;
            _m21 = scale.Z * rotationMatrix._m21;
            _m22 = scale.Z * rotationMatrix._m22;
            _m23 = 0.0f;

            _m30 = translation.X;
            _m31 = translation.Y;
            _m32 = translation.Z;
            _m33 = 1.0f;
        }

        private Matrix(float m00, float m01, float m02, float m03,
            float m10, float m11, float m12, float m13,
            float m20, float m21, float m22, float m23,
            float m30, float m31, float m32, float m33)
        {
            _m00 = m00;
            _m01 = m01;
            _m02 = m02;
            _m03 = m03;
            _m10 = m10;
            _m11 = m11;
            _m12 = m12;
            _m13 = m13;
            _m20 = m20;
            _m21 = m21;
            _m22 = m22;
            _m23 = m23;
            _m30 = m30;
            _m31 = m31;
            _m32 = m32;
            _m33 = m33;
        }
    }
}