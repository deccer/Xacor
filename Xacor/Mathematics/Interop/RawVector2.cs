using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a float2 (2 floats).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("X: {X}, Y: {Y}")]
    public struct RawVector2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawVector2"/> struct.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The y.</param>
        public RawVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public float X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public float Y;
    }
}
