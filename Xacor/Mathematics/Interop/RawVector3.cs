using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a float3 (3 floats).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("X: {X}, Y: {Y}, Z: {Z}")]
    public struct RawVector3
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawVector3"/> struct.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public RawVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public float X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public float Y;

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        public float Z;
    }
}
