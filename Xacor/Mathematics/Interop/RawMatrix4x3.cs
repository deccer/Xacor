﻿using System.Runtime.InteropServices;

namespace Xacor.Mathematics.Interop
{
    /// <summary>
    /// Interop type for a float4x3 (12 floats).
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct RawMatrix4x3
    {
        /// <summary>
        /// Value at row 1 column 1.
        /// </summary>
        public float M11;

        /// <summary>
        /// Value at row 1 column 2.
        /// </summary>
        public float M12;

        /// <summary>
        /// Value at row 1 column 3.
        /// </summary>
        public float M13;

        /// <summary>
        /// Value at row 2 column 1.
        /// </summary>
        public float M21;

        /// <summary>
        /// Value at row 2 column 2.
        /// </summary>
        public float M22;

        /// <summary>
        /// Value at row 2 column 3.
        /// </summary>
        public float M23;

        /// <summary>
        /// Value at row 3 column 1.
        /// </summary>
        public float M31;

        /// <summary>
        /// Value at row 3 column 2.
        /// </summary>
        public float M32;

        /// <summary>
        /// Value at row 3 column 3.
        /// </summary>
        public float M33;

        /// <summary>
        /// Value at row 4 column 1.
        /// </summary>
        public float M41;

        /// <summary>
        /// Value at row 4 column 2.
        /// </summary>
        public float M42;

        /// <summary>
        /// Value at row 4 column 3.
        /// </summary>
        public float M43;
    }
}
