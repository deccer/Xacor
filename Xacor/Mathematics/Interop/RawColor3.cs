using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a Color3 (RGB, 3 floats).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("R: {R}, G: {G}, B: {B}")]
    public struct RawColor3
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawColor3"/> struct.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        public RawColor3(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// The red component of the color.
        /// </summary>
        public float R;

        /// <summary>
        /// The green component of the color.
        /// </summary>
        public float G;

        /// <summary>
        /// The blue component of the color.
        /// </summary>
        public float B;
    }
}
