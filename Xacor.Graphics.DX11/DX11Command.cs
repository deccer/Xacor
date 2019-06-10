using System.Collections.Generic;
using SharpDX.Direct3D11;
using SharpDX.Mathematics.Interop;

namespace Xacor.Graphics.DX11
{
    internal struct DX11Command
    {
        public DX11CommandType Type;
        public RawColor4 RenderTargetClearColor;
        public RenderTargetView RenderTargetClear;
        public List<RenderTargetView> RenderTargets;
        public DepthStencilView RenderTargetDepthStencil;

        public int TexturesStartSlot;
        public int TextureCount;
        public List<ShaderResourceView> Textures;

        public int SamplersStartSlot;
        public int SamplerCount;
        public List<SamplerState> Samplers;

        public int ConstantBuffersStartSlot;
        public int ConstantBufferCount;
        public BufferScope ConstantBufferScope;
        public List<Buffer> ConstantBuffers;

        public RasterizerState RasterizerState;
        public DepthStencilState DepthStencilState;
        public BlendState BlendState;
        public float DepthClear;
        public byte DepthClearStencil;

        public string PassName;
        public PrimitiveTopology PrimitiveTopology;

        public VertexBufferBinding VertexBufferBinding;
        public int VertexCount;
        public int VertexOffset;
        public Buffer IndexBuffer;
        public Format IndexBufferFormat;
        public int IndexCount;
        public int IndexOffset;

        public VertexShader VertexShader;
        public PixelShader PixelShader;
        public ComputeShader ComputeShader;

        public RawViewportF Viewport;
        public RawRectangle ScissorRectangle;

        public DX11InputLayout InputLayout;

        public void Clear()
        {
            RenderTargetClearColor = new RawColor4(0, 0, 0, 1.0f);
            RenderTargetDepthStencil = null;
            RenderTargetClear = null;
            RenderTargets = new List<RenderTargetView>();
            Textures = new List<ShaderResourceView>();
            Samplers = new List<SamplerState>();
            ConstantBuffers = new List<Buffer>();
            DepthStencilState = null;
            InputLayout = null;
            RasterizerState = null;
            BlendState = null;

            Type = DX11CommandType.Begin;
            TextureCount = 0;
            TexturesStartSlot = 0;
            SamplerCount = 0;
            SamplersStartSlot = 0;
            ConstantBufferCount = 0;
            ConstantBuffersStartSlot = 0;
            ConstantBufferScope = BufferScope.Global;
            DepthClear = 0.0f;
            DepthClearStencil = 0;
            PassName = "None";
            PrimitiveTopology = PrimitiveTopology.NotAssigned;
            VertexCount = 0;
            VertexOffset = 0;
            VertexShader = null;
            PixelShader = null;
            ComputeShader = null;
            IndexBuffer = null;
            IndexBufferFormat = Format.R16UInt;
            IndexCount = 0;
            IndexOffset = 0;
        }
    }
}