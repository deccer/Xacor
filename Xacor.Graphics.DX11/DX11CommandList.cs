using System;
using System.Collections.Generic;
using System.Text;
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
        public List<ShaderResourceView> ConstantBuffers;

        public DepthStencilState DepthStencilState;
        public float DepthClear;

        public string PassName;
        public PrimitiveTopology PrimitiveTopology;
        public int VertexCount;
        public int VertexOffset;
        public int IndexCount;
        public int IndexOffset;

        public RawViewportF Viewport;
        public RawRectangle ScissorRectangle;

        public DX11InputLayout InputLayout;

        public void Clear()
        {

        }
    }

    internal class DX11CommandList : ICommandList
    {
        private readonly DX11GraphicsDevice _graphicsDevice;
        private readonly List<DX11Command> _commandList;

        public void Begin(string passName, IPipeline pipeline)
        {
            throw new NotImplementedException();
        }

        public void Draw(int vertexCount)
        {
            throw new NotImplementedException();
        }

        public void DrawIndexed(int indexCount, int indexOffset, int vertexOffset)
        {
            throw new NotImplementedException();
        }

        public DX11CommandList(DX11GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _commandList = new List<DX11Command>(32);
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public void SetBlendState(IBlendState blendState)
        {
            throw new NotImplementedException();
        }

        public void SetConstantBuffer(IConstantBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public void SetDepthStencilState(IDepthStencilState depthStencilState)
        {
            throw new NotImplementedException();
        }

        public void SetIndexBuffer(IIndexBuffer indexBuffer)
        {
            throw new NotImplementedException();
        }

        public void SetPixelShader(IShader pixelShader)
        {
            throw new NotImplementedException();
        }

        public void SetRasterizerState(IRasterizerState rasterizerState)
        {
            throw new NotImplementedException();
        }

        public void SetVertexBuffer(IVertexBuffer vertexBuffer)
        {
            throw new NotImplementedException();
        }

        public void SetVertexShader(IShader vertexShader)
        {
            throw new NotImplementedException();
        }

        public void Submit()
        {
            foreach (var command in _commandList)
            {
                switch (command.Type)
                {
                    case DX11CommandType.Begin:
                        break;
                    case DX11CommandType.End:
                        break;
                    case DX11CommandType.Draw:
                        _graphicsDevice.NativeDeviceContext.Draw(command.VertexCount, command.VertexOffset);
                        break;
                    case DX11CommandType.DrawIndexed:
                        _graphicsDevice.NativeDeviceContext.DrawIndexed(command.IndexCount, command.IndexOffset, command.VertexOffset);
                        break;
                    case DX11CommandType.SetViewport:
                        var viewport = new RawViewportF();
                        viewport.X = command.Viewport.X;
                        viewport.Y = command.Viewport.Y;
                        viewport.Width = command.Viewport.Width;
                        viewport.Height = command.Viewport.Height;
                        viewport.MinDepth = 0.0f;
                        viewport.MaxDepth = 1.0f;
                        _graphicsDevice.NativeDeviceContext.Rasterizer.SetViewport(viewport);
                        break;
                    case DX11CommandType.SetScissor:
                        _graphicsDevice.NativeDeviceContext.Rasterizer.SetScissorRectangle(command.ScissorRectangle.Left, command.ScissorRectangle.Top, command.ScissorRectangle.Right, command.ScissorRectangle.Bottom);
                        break;
                    case DX11CommandType.SetPrimitiveTopology:
                        _graphicsDevice.NativeDeviceContext.InputAssembler.PrimitiveTopology = command.PrimitiveTopology.ToSharpDX();
                        break;
                    case DX11CommandType.SetInputLayout:
                        _graphicsDevice.NativeDeviceContext.InputAssembler.InputLayout = command.InputLayout;
                        break;
                    case DX11CommandType.SetDepthStencilState:
                        break;
                    case DX11CommandType.SetRasterizerState:
                        break;
                    case DX11CommandType.SetBlendState:
                        break;
                    case DX11CommandType.SetVertexBuffer:
                        break;
                    case DX11CommandType.SetIndexBuffer:
                        break;
                    case DX11CommandType.SetVertexShader:
                        break;
                    case DX11CommandType.SetPixelShader:
                        break;
                    case DX11CommandType.SetComputeShader:
                        break;
                    case DX11CommandType.SetConstantBuffers:
                        break;
                    case DX11CommandType.SetSamplers:
                        break;
                    case DX11CommandType.SetTextures:
                        break;
                    case DX11CommandType.SetRenderTargets:
                        break;
                    case DX11CommandType.ClearRenderTarget:
                        break;
                    case DX11CommandType.ClearDepthStencil:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            Clear();
        }

        private void Clear()
        {
            _commandList.Clear();
        }
    }
}
