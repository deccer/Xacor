using System.Diagnostics;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Xacor.Graphics.Api.GL
{
    [DebuggerDisplay("{Type}")]
    public struct GLCommand
    {
        public string Name;

        public CommandType Type;

        public Color4 ClearColor;

        public float ClearDepth;

        public byte ClearStencil;

        public Rectangle Viewport;

        public Rectangle ScissorRectangle;

        public int InputLayout;

        public int IndexBuffer;

        public int VertexBuffer;

        public int VertexStride;

        public int DrawVertexCount;

        public int DrawIndexCount;

        public int DrawIndexOffset;

        public int DrawVertexOffset;

        public int TextureView;

        public int ConstantBuffer;

        public int Pipeline;

        public PrimitiveType PrimitiveType;
    }
}