using System.Collections.Generic;
using System.Linq;
using SharpDX.Direct3D11;
using Xacor.Graphics.Api.DX;
using D3D11InputLayout = SharpDX.Direct3D11.InputLayout;

namespace Xacor.Graphics.Api.DX11
{
    internal class DX11InputLayout : IInputLayout
    {
        private readonly D3D11InputLayout _inputLayout;

        public DX11InputLayout(DX11GraphicsDevice graphicsDevice, byte[] shaderBytecode, IEnumerable<DX11VertexAttribute> attributes)
        {
            var inputElements = attributes
                .Select(attribute => new InputElement(attribute.Name, attribute.SemanticIndex, attribute.Format.ToSharpDX(), attribute.Offset, attribute.Binding))
                .ToArray();

            _inputLayout = new D3D11InputLayout(graphicsDevice, shaderBytecode, inputElements);
        }

        public static implicit operator D3D11InputLayout(DX11InputLayout inputLayout)
        {
            return inputLayout._inputLayout;
        }

        public void Dispose()
        {
            _inputLayout?.Dispose();
        }
    }
}