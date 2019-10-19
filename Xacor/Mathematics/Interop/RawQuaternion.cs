using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a Quaternion (4 floats).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("X: {X}, Y: {Y}, Z: {Z}, W: {W}")]
    public struct RawQuaternion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawQuaternion"/> struct.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        public RawQuaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// The X component of the quaternion.
        /// </summary>
        public float X;

        /// <summary>
        /// The Y component of the quaternion.
        /// </summary>
        public float Y;

        /// <summary>
        /// The Z component of the quaternion.
        /// </summary>
        public float Z;

        /// <summary>
        /// The W component of the quaternion.
        /// </summary>
        public float W;
    }
}
