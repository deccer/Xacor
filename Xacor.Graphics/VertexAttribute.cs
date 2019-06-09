namespace Xacor.Graphics
{
    public readonly struct VertexAttribute
    {
        public readonly string Name;

        public readonly int SemanticIndex;

        public readonly int Binding;

        public readonly int Offset;

        public readonly Format Format;

        public VertexAttribute(string name, int semanticIndex, int binding, int offset, Format format)
        {
            Name = name;
            SemanticIndex = semanticIndex;
            Binding = binding;
            Offset = offset;
            Format = format;
        }
    }
}