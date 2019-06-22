using System.Collections.Generic;

namespace Xacor.Graphics.Api.GL46
{
    internal class GL46InputLayout : IInputLayout
    {
        private readonly IEnumerable<GLVertexAttribute> _attributes;
        private readonly int _nativeArrayObject;

        public static implicit operator int(GL46InputLayout inputLayout)
        {
            return inputLayout._nativeArrayObject;
        }

        public void Dispose()
        {
            OpenTK.Graphics.OpenGL4.GL.DeleteVertexArray(_nativeArrayObject);
        }

        public GL46InputLayout(IEnumerable<GLVertexAttribute> attributes)
        {
            _attributes = attributes;

            OpenTK.Graphics.OpenGL4.GL.CreateVertexArrays(1, out _nativeArrayObject);
            foreach (var attribute in _attributes)
            {
                OpenTK.Graphics.OpenGL4.GL.EnableVertexArrayAttrib(_nativeArrayObject, attribute.Index);
                OpenTK.Graphics.OpenGL4.GL.VertexArrayAttribFormat(_nativeArrayObject, attribute.Index, attribute.Components, attribute.Type, false, attribute.Offset);
                OpenTK.Graphics.OpenGL4.GL.VertexArrayAttribBinding(_nativeArrayObject, attribute.Index, 0);
            }
        }
    }
}