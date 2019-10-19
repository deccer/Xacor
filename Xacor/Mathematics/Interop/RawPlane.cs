using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a Plane (4 floats).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [DebuggerDisplay("Normal: {Normal}, D: {D}")]
    public struct RawPlane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawPlane"/> struct.
        /// </summary>
        /// <param name="normal">The plane normal.</param>
        /// <param name="d">The plance distance.</param>
        public RawPlane(RawVector3 normal, float d)
        {
            Normal = normal;
            D = d;
        }

        /// <summary>
        /// The normal vector of the plane.
        /// </summary>
        public RawVector3 Normal;

        /// <summary>
        /// The distance of the plane along its normal from the origin.
        /// </summary>
        public float D;
    }
}
