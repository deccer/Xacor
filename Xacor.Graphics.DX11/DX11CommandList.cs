using System;
using System.Collections.Generic;
using SharpDX.Direct3D11;
using SharpDX.Mathematics.Interop;
using Xacor.Graphics.DX;

namespace Xacor.Graphics.DX11
{
    internal class DX11CommandList : ICommandList
    {
        private readonly DX11GraphicsDevice _graphicsDevice;
        private readonly List<DX11Command> _commandList;

        public void Begin(string passName, IPipeline pipeline)
        {
            throw new NotImplementedException();
        }

        public void ClearRenderTarget(Texture renderTarget)
        {
            var command = new DX11Command();
            command.Type = DX11CommandType.ClearRenderTarget;
            command.RenderTargetClear = (DX11TextureView)renderTarget.View;
            command.RenderTargetClearColor = new RawColor4(0.2f, 0.0f, 0.0f, 1.0f);
            _commandList.Add(command);
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
                        _graphicsDevice.NativeDeviceContext.OutputMerger.DepthStencilState = command.DepthStencilState;
                        break;
                    case DX11CommandType.SetRasterizerState:
                        _graphicsDevice.NativeDeviceContext.Rasterizer.State = command.RasterizerState;
                        break;
                    case DX11CommandType.SetBlendState:
                        _graphicsDevice.NativeDeviceContext.OutputMerger.BlendFactor = new RawColor4(0, 0, 0, 0);
                        _graphicsDevice.NativeDeviceContext.OutputMerger.BlendState = command.BlendState;
                        break;
                    case DX11CommandType.SetVertexBuffer:
                        _graphicsDevice.NativeDeviceContext.InputAssembler.SetVertexBuffers(0, command.VertexBufferBinding);
                        break;
                    case DX11CommandType.SetIndexBuffer:
                        _graphicsDevice.NativeDeviceContext.InputAssembler.SetIndexBuffer(command.IndexBuffer, command.IndexBufferFormat.ToSharpDX(), command.IndexOffset);
                        break;
                    case DX11CommandType.SetVertexShader:
                        _graphicsDevice.NativeDeviceContext.VertexShader.Set(command.VertexShader);
                        break;
                    case DX11CommandType.SetPixelShader:
                        _graphicsDevice.NativeDeviceContext.PixelShader.Set(command.PixelShader);
                        break;
                    case DX11CommandType.SetComputeShader:
                        _graphicsDevice.NativeDeviceContext.ComputeShader.Set(command.ComputeShader);
                        break;
                    case DX11CommandType.SetConstantBuffers:
                        for (var i = command.ConstantBuffersStartSlot; i < command.ConstantBuffers.Count; ++i)
                        {
                            switch (command.ConstantBufferScope)
                            {
                                case BufferScope.VertexShader:
                                    _graphicsDevice.NativeDeviceContext.VertexShader.SetConstantBuffer(i, command.ConstantBuffers[i]);
                                    break;
                                case BufferScope.PixelShader:
                                    _graphicsDevice.NativeDeviceContext.PixelShader.SetConstantBuffer(i, command.ConstantBuffers[i]);
                                    break;
                                case BufferScope.Global:
                                    _graphicsDevice.NativeDeviceContext.VertexShader.SetConstantBuffer(i, command.ConstantBuffers[i]);
                                    _graphicsDevice.NativeDeviceContext.PixelShader.SetConstantBuffer(i, command.ConstantBuffers[i]);
                                    break;
                                case BufferScope.NotAssigned:
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }

                        break;
                    case DX11CommandType.SetSamplers:
                        _graphicsDevice.NativeDeviceContext.PixelShader.SetSamplers(command.SamplersStartSlot, command.SamplerCount, command.Samplers.ToArray());
                        break;
                    case DX11CommandType.SetTextures:
                        _graphicsDevice.NativeDeviceContext.PixelShader.SetShaderResources(command.TexturesStartSlot, command.TextureCount, command.Textures.ToArray());
                        break;
                    case DX11CommandType.SetRenderTargets:
                        _graphicsDevice.NativeDeviceContext.OutputMerger.SetRenderTargets(command.RenderTargetDepthStencil, command.RenderTargets.ToArray());
                        break;
                    case DX11CommandType.ClearRenderTarget:
                        _graphicsDevice.NativeDeviceContext.ClearRenderTargetView(command.RenderTargetClear, command.RenderTargetClearColor);
                        break;
                    case DX11CommandType.ClearDepthStencil:
                        var depthStencilClearFlags = DepthStencilClearFlags.Depth;
                        _graphicsDevice.NativeDeviceContext.ClearDepthStencilView(command.RenderTargetDepthStencil, depthStencilClearFlags, command.DepthClear, command.DepthClearStencil);
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
