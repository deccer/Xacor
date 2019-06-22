using OpenTK.Graphics.OpenGL4;

namespace Xacor.Graphics.Api.GL46
{
    internal readonly struct GLVertexAttribute
    {
        public readonly string Name;
        public readonly int Index;
        public readonly VertexAttribType Type;
        public readonly int Components;
        public readonly int Offset;

        public GLVertexAttribute(string name, int index, VertexAttribType type, int components, int offset)
        {
            Name = name;
            Index = index;
            Type = type;
            Components = components;
            Offset = offset;
        }
    }
}