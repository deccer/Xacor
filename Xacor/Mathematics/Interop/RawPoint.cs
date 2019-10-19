using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a Point (2 ints).
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [DebuggerDisplay("X: {X}, Y: {Y}")]
    public struct RawPoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawPoint"/> struct.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The y.</param>
        public RawPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Left coordinate.
        /// </summary>
        public int X;

        /// <summary>
        /// Top coordinate.
        /// </summary>
        public int Y;
   }
}