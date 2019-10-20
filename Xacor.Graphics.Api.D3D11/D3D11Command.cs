using System;
using SharpDX.Direct3D11;
using SharpDX.Mathematics.Interop;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Xacor.Graphics.Api.D3D11
{
    internal struct D3D11Command : IEquatable<D3D11Command>
    {
        public CommandType Type;
        public RawColor4 RenderTargetClearColor;
        public RenderTargetView RenderTarget;
        public RenderTargetView[] RenderTargets;
        public DepthStencilView RenderTargetDepthStencil;

        public int TexturesStartSlot;
        public int TextureCount;
        public ShaderResourceView[] Textures;

        public int SamplersStartSlot;
        public int SamplerCount;
        public SamplerState[] Samplers;

        public int ConstantBuffersStartSlot;
        public int ConstantBufferCount;
        public BufferScope ConstantBufferScope;
        public Buffer ConstantBuffer;

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

        public D3D11InputLayout InputLayout;

        public void Clear()
        {
            RenderTargetClearColor = new RawColor4(0, 0, 1, 1.0f);
            RenderTargetDepthStencil = null;
            RenderTarget = null;
            ConstantBuffer = null;
            DepthStencilState = null;
            InputLayout = null;
            RasterizerState = null;
            BlendState = null;

            Type = CommandType.Begin;
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

        public bool Equals(D3D11Command other)
        {
            return Type == other.Type && RenderTargetClearColor.Equals(other.RenderTargetClearColor) && Equals(RenderTarget, other.RenderTarget) && Equals(RenderTargets, other.RenderTargets) 
                   && Equals(RenderTargetDepthStencil, other.RenderTargetDepthStencil) && TexturesStartSlot == other.TexturesStartSlot && TextureCount == other.TextureCount 
                   && Equals(Textures, other.Textures) && SamplersStartSlot == other.SamplersStartSlot && SamplerCount == other.SamplerCount && Equals(Samplers, other.Samplers) 
                   && ConstantBuffersStartSlot == other.ConstantBuffersStartSlot && ConstantBufferCount == other.ConstantBufferCount && ConstantBufferScope == other.ConstantBufferScope 
                   && Equals(ConstantBuffer, other.ConstantBuffer) && Equals(RasterizerState, other.RasterizerState) && Equals(DepthStencilState, other.DepthStencilState) 
                   && Equals(BlendState, other.BlendState) && DepthClear.Equals(other.DepthClear) && DepthClearStencil == other.DepthClearStencil && string.Equals(PassName, other.PassName) 
                   && PrimitiveTopology == other.PrimitiveTopology && Equals(VertexBufferBinding, other.VertexBufferBinding) && VertexCount == other.VertexCount && VertexOffset == other.VertexOffset 
                   && Equals(IndexBuffer, other.IndexBuffer) && IndexBufferFormat == other.IndexBufferFormat && IndexCount == other.IndexCount && IndexOffset == other.IndexOffset 
                   && Equals(VertexShader, other.VertexShader) && Equals(PixelShader, other.PixelShader) && Equals(ComputeShader, other.ComputeShader) && Viewport.Equals(other.Viewport) 
                   && ScissorRectangle.Equals(other.ScissorRectangle) && Equals(InputLayout, other.InputLayout);
        }

        public override bool Equals(object obj)
        {
            return obj is D3D11Command other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Type;
                hashCode = (hashCode * 397) ^ RenderTargetClearColor.GetHashCode();
                hashCode = (hashCode * 397) ^ (RenderTarget != null ? RenderTarget.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (RenderTargets != null ? RenderTargets.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (RenderTargetDepthStencil != null ? RenderTargetDepthStencil.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ TexturesStartSlot;
                hashCode = (hashCode * 397) ^ TextureCount;
                hashCode = (hashCode * 397) ^ (Textures != null ? Textures.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ SamplersStartSlot;
                hashCode = (hashCode * 397) ^ SamplerCount;
                hashCode = (hashCode * 397) ^ (Samplers != null ? Samplers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ConstantBuffersStartSlot;
                hashCode = (hashCode * 397) ^ ConstantBufferCount;
                hashCode = (hashCode * 397) ^ (int) ConstantBufferScope;
                hashCode = (hashCode * 397) ^ (ConstantBuffer != null ? ConstantBuffer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (RasterizerState != null ? RasterizerState.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DepthStencilState != null ? DepthStencilState.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BlendState != null ? BlendState.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ DepthClear.GetHashCode();
                hashCode = (hashCode * 397) ^ DepthClearStencil.GetHashCode();
                hashCode = (hashCode * 397) ^ (PassName != null ? PassName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) PrimitiveTopology;
                hashCode = (hashCode * 397) ^ VertexBufferBinding.GetHashCode();
                hashCode = (hashCode * 397) ^ VertexCount;
                hashCode = (hashCode * 397) ^ VertexOffset;
                hashCode = (hashCode * 397) ^ (IndexBuffer != null ? IndexBuffer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) IndexBufferFormat;
                hashCode = (hashCode * 397) ^ IndexCount;
                hashCode = (hashCode * 397) ^ IndexOffset;
                hashCode = (hashCode * 397) ^ (VertexShader != null ? VertexShader.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PixelShader != null ? PixelShader.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ComputeShader != null ? ComputeShader.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Viewport.GetHashCode();
                hashCode = (hashCode * 397) ^ ScissorRectangle.GetHashCode();
                hashCode = (hashCode * 397) ^ (InputLayout != null ? InputLayout.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}