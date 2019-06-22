using System.Numerics;
using System.Runtime.InteropServices;

namespace Xacor.Graphics.Api.DX
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPositionTexture
    {
        public Vector3 Position { get; set; }

        public Vector2 Uv { get; set; }

        public VertexPositionTexture(Vector3 position, Vector2 uv)
        {
            Position = position;
            Uv = uv;
        }
    }
}