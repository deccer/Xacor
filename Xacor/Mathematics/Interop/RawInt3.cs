using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a Int3 (3 ints).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("X: {X}, Y: {Y}, Z: {Z}")]
    public struct RawInt3
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawInt3"/> struct.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public RawInt3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public int Y;

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        public int Z;
    }
}
