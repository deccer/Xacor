using System.Collections.Generic;
using System.Linq;
using SharpDX.Direct3D11;
using Xacor.Graphics.Api.D3D;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11InputLayout : IInputLayout
    {
        private readonly InputLayout _inputLayout;

        public D3D11InputLayout(D3D11GraphicsDevice graphicsDevice, byte[] shaderBytecode, IEnumerable<D3D11VertexAttribute> attributes)
        {
            var inputElements = attributes
                .Select(attribute => new InputElement(attribute.Name, attribute.SemanticIndex, attribute.Format.ToSharpDX(), attribute.Offset, attribute.Binding))
                .ToArray();

            _inputLayout = new InputLayout(graphicsDevice, shaderBytecode, inputElements);
        }

        public static implicit operator InputLayout(D3D11InputLayout inputLayout)
        {
            return inputLayout._inputLayout;
        }

        public void Dispose()
        {
            _inputLayout?.Dispose();
        }
    }
}