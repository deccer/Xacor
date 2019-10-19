using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a RectangleF (4 floats).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("Left: {Left}, Top: {Top}, Right: {Right}, Bottom: {Bottom}")]
    public struct RawRectangleF
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawRectangleF"/> struct.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        public RawRectangleF(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// The left position.
        /// </summary>
        public float Left;

        /// <summary>
        /// The top position.
        /// </summary>
        public float Top;

        /// <summary>
        /// The right position
        /// </summary>
        public float Right;

        /// <summary>
        /// The bottom position.
        /// </summary>
        public float Bottom;
    }
}
