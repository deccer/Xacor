namespace Xacor.Graphics.Api.DX11
{
    public readonly struct DX11VertexAttribute
    {
        public readonly string Name;

        public readonly int SemanticIndex;

        public readonly int Binding;

        public readonly int Offset;

        public readonly Format Format;

        public DX11VertexAttribute(string name, int semanticIndex, int binding, int offset, Format format)
        {
            Name = name;
            SemanticIndex = semanticIndex;
            Binding = binding;
            Offset = offset;
            Format = format;
        }
    }
}