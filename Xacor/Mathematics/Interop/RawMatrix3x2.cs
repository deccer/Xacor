using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a float3x2 (6 floats).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct RawMatrix3x2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawMatrix3x2"/> struct.
        /// </summary>
        /// <param name="m11">The m11 value.</param>
        /// <param name="m12">The m12 value.</param>
        /// <param name="m21">The m21 value.</param>
        /// <param name="m22">The m22 value.</param>
        /// <param name="m31">The m31 value.</param>
        /// <param name="m32">The m32 value.</param>
        public RawMatrix3x2(float m11, float m12, float m21, float m22, float m31, float m32)
        {
            M11 = m11;
            M12 = m12;
            M21 = m21;
            M22 = m22;
            M31 = m31;
            M32 = m32;
        }

        /// <summary>
        /// Element (1,1)
        /// </summary>
        public float M11;

        /// <summary>
        /// Element (1,2)
        /// </summary>
        public float M12;

        /// <summary>
        /// Element (2,1)
        /// </summary>
        public float M21;

        /// <summary>
        /// Element (2,2)
        /// </summary>
        public float M22;

        /// <summary>
        /// Element (3,1)
        /// </summary>
        public float M31;

        /// <summary>
        /// Element (3,2)
        /// </summary>
        public float M32;
    }
}
