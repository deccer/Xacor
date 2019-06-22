using System.Numerics;
using System.Runtime.InteropServices;

namespace Xacor.Graphics.Api.DX
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexPositionTextureNormalTangent
    {
        public Vector3 Position { get; set; }

        public Vector2 Uv;

        public Vector3 Normal;

        public Vector3 Tangent;

        public VertexPositionTextureNormalTangent(Vector3 position, Vector2 uv, Vector3 normal, Vector3 tangent)
        {
            Position = position;
            Uv = uv;
            Normal = normal;
            Tangent = tangent;
        }
    }
}