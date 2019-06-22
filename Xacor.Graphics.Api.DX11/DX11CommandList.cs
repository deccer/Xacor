using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using SharpDX.Direct3D11;
using SharpDX.Mathematics.Interop;
using Xacor.Graphics.Api.DX;

namespace Xacor.Graphics.Api.DX11
{
    internal class DX11CommandList : ICommandList
    {
        private readonly DX11GraphicsDevice _graphicsDevice;
        private readonly List<DX11Command> _commandList;

        public void Begin(string passName, IPipeline pipeline)
        {
            var command = new DX11Command
            {
                Type = CommandType.Begin,
                PassName = passName
            };
            _commandList.Add(command);

            if (pipeline != null)
            {
                SetBlendState(pipeline.BlendState);
                SetDepthStencilState(pipeline.DepthStencilState);
                SetRasterizerState(pipeline.RasterizerState);
                SetInputLayout(pipeline.InputLayout);
                SetVertexShader(pipeline.VertexShader);
                SetPixelShader(pipeline.PixelShader);
                SetPrimitiveTopology(pipeline.PrimitiveTopology);
                SetViewport(pipeline.Viewport);
            }
        }

        public void ClearRenderTarget(TextureView renderTarget, Vector4 clearColor)
        {
            var command = new DX11Command
            {
                Type = CommandType.ClearRenderTarget,
                RenderTarget = (DX11TextureView)renderTarget,
                RenderTargetClearColor = new RawColor4(clearColor.X, clearColor.Y, clearColor.Z, clearColor.W)
            };
            _commandList.Add(command);
        }

        public void ClearDepthStencil(TextureView depthStencilView, float depthClear, byte depthClearStencil)
        {
            var command = new DX11Command
            {
                Type = CommandType.ClearDepthStencil,
                RenderTargetDepthStencil = (DX11TextureView)depthStencilView,
                DepthClear = depthClear,
                DepthClearStencil = depthClearStencil
            };
            _commandList.Add(command);
        }

        public void Draw(int vertexCount)
        {
            var command = new DX11Command
            {
                Type = CommandType.Draw,
                VertexCount = vertexCount
            };
            _commandList.Add(command);
        }

        public void DrawIndexed(int indexCount, int indexOffset, int vertexOffset)
        {
            var command = new DX11Command
            {
                Type = CommandType.DrawIndexed,
                IndexCount = indexCount,
                IndexOffset = indexOffset,
                VertexOffset = vertexOffset
            };
            _commandList.Add(command);
        }

        public DX11CommandList(DX11GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _commandList = new List<DX11Command>(32);
        }

        public void End()
        {
            var command = new DX11Command
            {
                Type = CommandType.End
            };
            _commandList.Add(command);
        }

        public void SetBlendState(IBlendState blendState)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetBlendState,
                BlendState = (DX11BlendState)blendState
            };
            _commandList.Add(command);
        }

        public void SetConstantBuffer(IConstantBuffer buffer, BufferScope bufferScope)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetConstantBuffers,
                ConstantBufferScope = bufferScope,
                ConstantBuffer = (DX11ConstantBuffer)buffer
            };
            _commandList.Add(command);
        }

        public void SetDepthStencilState(IDepthStencilState depthStencilState)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetDepthStencilState,
                DepthStencilState = (DX11DepthStencilState)depthStencilState
            };
            _commandList.Add(command);
        }

        public void SetIndexBuffer(IIndexBuffer indexBuffer)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetIndexBuffer,
                IndexBuffer = (DX11IndexBuffer)indexBuffer
            };
            _commandList.Add(command);
        }

        public void SetInputLayout(IInputLayout inputLayout)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetInputLayout,
                InputLayout = (DX11InputLayout)inputLayout
            };
            _commandList.Add(command);
        }

        public void SetPixelShader(IShader pixelShader)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetPixelShader,
                PixelShader = (PixelShader)(DX11Shader)pixelShader
            };
            _commandList.Add(command);
        }

        public void SetRasterizerState(IRasterizerState rasterizerState)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetRasterizerState,
                RasterizerState = (DX11RasterizerState)rasterizerState
            };
            _commandList.Add(command);
        }

        public void SetRenderTarget(TextureView renderTarget, TextureView depthStencilView)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetRenderTarget,
                RenderTarget = (DX11TextureView)renderTarget,
                RenderTargetDepthStencil = (DX11TextureView)depthStencilView

            };
            _commandList.Add(command);
        }

        public void SetRenderTargets(TextureView[] renderTargets, TextureView depthStencilView)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetRenderTargets,
                RenderTargets = renderTargets.Select(rt => (RenderTargetView)(DX11TextureView)rt).ToArray()
            };
            _commandList.Add(command);
        }

        public void SetSampler(ISampler sampler)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetSamplers,
                Samplers = new[] { (SamplerState)(DX11Sampler)sampler },
                SamplerCount = 1,
                SamplersStartSlot = 0
            };
            _commandList.Add(command);
        }

        public void SetScissorRectangle(Rectangle rectangle)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetScissor,
                ScissorRectangle = rectangle.ToSharpDX()
            };
            _commandList.Add(command);
        }

        public void SetTexture(TextureView textureView)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetTextures,
                Textures = new[] { (ShaderResourceView)(DX11TextureView)textureView },
                TexturesStartSlot = 0,
                TextureCount = 1
            };
            _commandList.Add(command);
        }

        public void SetPrimitiveTopology(PrimitiveTopology primitiveTopology)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetPrimitiveTopology,
                PrimitiveTopology = primitiveTopology
            };
            _commandList.Add(command);
        }

        public void SetVertexBuffer(IVertexBuffer vertexBuffer)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetVertexBuffer,
                VertexBufferBinding = (DX11VertexBufferBinding)vertexBuffer.GetVertexBufferBinding()
            };
            _commandList.Add(command);
        }

        public void SetVertexShader(IShader vertexShader)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetVertexShader,
                VertexShader = (VertexShader)(DX11Shader)vertexShader
            };
            _commandList.Add(command);
        }

        public void SetViewport(Viewport viewport)
        {
            var command = new DX11Command
            {
                Type = CommandType.SetViewport,
                Viewport = viewport.ToSharpDX(),
            };
            _commandList.Add(command);
        }

        public void Submit()
        {
            foreach (var command in _commandList)
            {
                switch (command.Type)
                {
                    case CommandType.Begin:
                        break;
                    case CommandType.End:
                        break;
                    case CommandType.Draw:
                        _graphicsDevice.NativeDeviceContext.Draw(command.VertexCount, command.VertexOffset);
                        break;
                    case CommandType.DrawIndexed:
                        _graphicsDevice.NativeDeviceContext.DrawIndexed(command.IndexCount, command.IndexOffset, command.VertexOffset);
                        break;
                    case CommandType.SetViewport:
                        var viewport = new RawViewportF();
                        viewport.X = command.Viewport.X;
                        viewport.Y = command.Viewport.Y;
                        viewport.Width = command.Viewport.Width;
                        viewport.Height = command.Viewport.Height;
                        viewport.MinDepth = 0.0f;
                        viewport.MaxDepth = 1.0f;
                        _graphicsDevice.NativeDeviceContext.Rasterizer.SetViewport(viewport);
                        break;
                    case CommandType.SetScissor:
                        _graphicsDevice.NativeDeviceContext.Rasterizer.SetScissorRectangle(command.ScissorRectangle.Left, command.ScissorRectangle.Top, command.ScissorRectangle.Right, command.ScissorRectangle.Bottom);
                        break;
                    case CommandType.SetPrimitiveTopology:
                        _graphicsDevice.NativeDeviceContext.InputAssembler.PrimitiveTopology = command.PrimitiveTopology.ToSharpDX();
                        break;
                    case CommandType.SetInputLayout:
                        _graphicsDevice.NativeDeviceContext.InputAssembler.InputLayout = command.InputLayout;
                        break;
                    case CommandType.SetDepthStencilState:
                        _graphicsDevice.NativeDeviceContext.OutputMerger.DepthStencilState = command.DepthStencilState;
                        break;
                    case CommandType.SetRasterizerState:
                        _graphicsDevice.NativeDeviceContext.Rasterizer.State = command.RasterizerState;
                        break;
                    case CommandType.SetBlendState:
                        _graphicsDevice.NativeDeviceContext.OutputMerger.BlendFactor = new RawColor4(0, 0, 0, 0);
                        _graphicsDevice.NativeDeviceContext.OutputMerger.BlendState = command.BlendState;
                        break;
                    case CommandType.SetVertexBuffer:
                        _graphicsDevice.NativeDeviceContext.InputAssembler.SetVertexBuffers(0, command.VertexBufferBinding);
                        break;
                    case CommandType.SetIndexBuffer:
                        _graphicsDevice.NativeDeviceContext.InputAssembler.SetIndexBuffer(command.IndexBuffer, command.IndexBufferFormat.ToSharpDX(), command.IndexOffset);
                        break;
                    case CommandType.SetVertexShader:
                        _graphicsDevice.NativeDeviceContext.VertexShader.Set(command.VertexShader);
                        break;
                    case CommandType.SetPixelShader:
                        _graphicsDevice.NativeDeviceContext.PixelShader.Set(command.PixelShader);
                        break;
                    case CommandType.SetComputeShader:
                        _graphicsDevice.NativeDeviceContext.ComputeShader.Set(command.ComputeShader);
                        break;
                    case CommandType.SetConstantBuffers:
                        switch (command.ConstantBufferScope)
                        {
                            case BufferScope.VertexShader:
                                _graphicsDevice.NativeDeviceContext.VertexShader.SetConstantBuffer(command.ConstantBuffersStartSlot, command.ConstantBuffer);
                                break;
                            case BufferScope.PixelShader:
                                _graphicsDevice.NativeDeviceContext.PixelShader.SetConstantBuffer(command.ConstantBuffersStartSlot, command.ConstantBuffer);
                                break;
                            case BufferScope.Global:
                                _graphicsDevice.NativeDeviceContext.VertexShader.SetConstantBuffer(command.ConstantBuffersStartSlot, command.ConstantBuffer);
                                _graphicsDevice.NativeDeviceContext.PixelShader.SetConstantBuffer(command.ConstantBuffersStartSlot, command.ConstantBuffer);
                                break;
                            case BufferScope.NotAssigned:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case CommandType.SetSamplers:
                        _graphicsDevice.NativeDeviceContext.PixelShader.SetSamplers(command.SamplersStartSlot, command.SamplerCount, command.Samplers);
                        break;
                    case CommandType.SetTextures:
                        _graphicsDevice.NativeDeviceContext.PixelShader.SetShaderResources(command.TexturesStartSlot, command.TextureCount, command.Textures);
                        break;
                    case CommandType.SetRenderTarget:
                        _graphicsDevice.NativeDeviceContext.OutputMerger.SetRenderTargets(command.RenderTargetDepthStencil, command.RenderTarget);
                        break;
                    case CommandType.SetRenderTargets:
                        _graphicsDevice.NativeDeviceContext.OutputMerger.SetRenderTargets(command.RenderTargetDepthStencil, command.RenderTargets);
                        break;
                    case CommandType.ClearRenderTarget:
                        _graphicsDevice.NativeDeviceContext.ClearRenderTargetView(command.RenderTarget, command.RenderTargetClearColor);
                        break;
                    case CommandType.ClearDepthStencil:
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