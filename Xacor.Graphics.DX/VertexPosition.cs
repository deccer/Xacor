using System.Numerics;
using System.Runtime.InteropServices;

namespace Xacor.Graphics.DX
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPosition
    {
        public Vector3 Position { get; set; }

        public VertexPosition(Vector3 position)
        {
            Position = position;
        }
    }
}