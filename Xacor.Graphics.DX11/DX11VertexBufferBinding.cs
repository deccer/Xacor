using D3D11Buffer= SharpDX.Direct3D11.Buffer;

namespace Xacor.Graphics.DX11
{
    internal class DX11VertexBufferBinding : VertexBufferBinding
    {
        private readonly SharpDX.Direct3D11.VertexBufferBinding _nativeVertexBufferBinding;

        public DX11VertexBufferBinding(D3D11Buffer buffer, int stride, int offset)
        {
            _nativeVertexBufferBinding = new SharpDX.Direct3D11.VertexBufferBinding(buffer, stride, offset);
        }

        public static implicit operator SharpDX.Direct3D11.VertexBufferBinding(DX11VertexBufferBinding vertexBufferBinding)
        {
            return vertexBufferBinding._nativeVertexBufferBinding;
        }
    }
}