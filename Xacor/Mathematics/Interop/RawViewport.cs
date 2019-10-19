using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a ViewPort (4 ints + 2 floats).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("X: {X}, Y: {Y}, Width: {Width}, Height: {Height}, MinDepth: {MinDepth}, MaxDepth: {MaxDepth}")]
    public struct RawViewport
    {
        /// <summary>
        /// Position of the pixel coordinate of the upper-left corner of the viewport.
        /// </summary>
        public int X;

        /// <summary>
        /// Position of the pixel coordinate of the upper-left corner of the viewport.
        /// </summary>
        public int Y;

        /// <summary>
        /// Width dimension of the viewport.
        /// </summary>
        public int Width;

        /// <summary>
        /// Height dimension of the viewport.
        /// </summary>
        public int Height;

        /// <summary>
        /// Gets or sets the minimum depth of the clip volume.
        /// </summary>
        public float MinDepth;

        /// <summary>
        /// Gets or sets the maximum depth of the clip volume.
        /// </summary>
        public float MaxDepth;
    }
}
