namespace Xacor.Graphics.Api.D3D11
{
    public readonly struct D3D11VertexAttribute
    {
        public readonly string Name;

        public readonly int SemanticIndex;

        public readonly int Binding;

        public readonly int Offset;

        public readonly Format Format;

        public D3D11VertexAttribute(string name, int semanticIndex, int binding, int offset, Format format)
        {
            Name = name;
            SemanticIndex = semanticIndex;
            Binding = binding;
            Offset = offset;
            Format = format;
        }
    }
}