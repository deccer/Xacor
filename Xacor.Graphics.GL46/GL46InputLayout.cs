using System.Collections.Generic;

namespace Xacor.Graphics.GL46
{
    internal class GL46InputLayout : IInputLayout
    {
        private readonly int _nativeArrayObject;

        public static implicit operator int(GL46InputLayout inputLayout)
        {
            return inputLayout._nativeArrayObject;
        }

        public void Dispose()
        {
            OpenTK.Graphics.OpenGL4.GL.DeleteVertexArray(_nativeArrayObject);
        }

        public GL46InputLayout(IEnumerable<VertexAttribute> attributes)
        {
            OpenTK.Graphics.OpenGL4.GL.CreateVertexArrays(1, out _nativeArrayObject);
            foreach (var attribute in attributes)
            {
                OpenTK.Graphics.OpenGL4.GL.EnableVertexArrayAttrib(_nativeArrayObject, attribute.SemanticIndex);
                OpenTK.Graphics.OpenGL4.GL.VertexArrayAttribFormat(_nativeArrayObject, attribute.SemanticIndex, attribute.Binding, attribute.Format.ToOpenTKVertexAttributeType(), false, attribute.Offset);
                OpenTK.Graphics.OpenGL4.GL.VertexArrayAttribBinding(_nativeArrayObject, attribute.SemanticIndex, 0);
            }
        }
    }
}