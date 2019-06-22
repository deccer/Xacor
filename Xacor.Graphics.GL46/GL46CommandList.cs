using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using GLCommand = Xacor.Graphics.GL.GLCommand;

namespace Xacor.Graphics.GL46
{
    internal class GL46CommandList : ICommandList
    {
        private readonly List<GLCommand> _commandList;
        private PrimitiveType _currentPrimitiveType = PrimitiveType.Triangles;
        private int _currentInputLayout;

        public void Begin(string passName, IPipeline pipeline)
        {
            var command = new GLCommand
            {
                Type = CommandType.Begin,
                Name = passName
            };

            _commandList.Add(command);

            if (pipeline != null)
            {
                SetViewport(pipeline.Viewport);
                SetProgramPipeline((GL46Pipeline)pipeline);
                SetInputLayout(pipeline.InputLayout);
            }
        }

        public void ClearRenderTarget(TextureView renderTarget, Vector4 clearColor)
        {
            var command = new GLCommand
            {
                Type = CommandType.ClearRenderTarget,
                ClearColor = new Color4(clearColor.X, clearColor.Y, clearColor.Z, clearColor.W)
            };

            _commandList.Add(command);
        }

        public void ClearDepthStencil(TextureView depthStencil, float clearDepth, byte stencilDepth)
        {
            var command = new GLCommand
            {
                Type = CommandType.ClearDepthStencil,
                ClearStencil = stencilDepth,
                ClearDepth = clearDepth
            };

            _commandList.Add(command);
        }

        public void Draw(int vertexCount)
        {
            var command = new GLCommand
            {
                Type = CommandType.Draw,
                DrawVertexCount = vertexCount
            };

            _commandList.Add(command);
        }

        public void DrawIndexed(int indexCount, int indexOffset, int vertexOffset)
        {
            var command = new GLCommand
            {
                Type = CommandType.DrawIndexed,
                DrawIndexCount = indexCount,
                DrawIndexOffset = indexOffset,
                DrawVertexOffset = vertexOffset,
            };
            _commandList.Add(command);
        }

        public void End()
        {
            var command = new GLCommand
            {
                Type = CommandType.End
            };

            _commandList.Add(command);
        }

        public GL46CommandList()
        {
            _commandList = new List<GLCommand>();
        }

        public void SetBlendState(IBlendState blendState)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetBlendState,
            };

            _commandList.Add(command);
        }

        public void SetConstantBuffer(IConstantBuffer buffer, BufferScope bufferScope)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetConstantBuffers,
                ConstantBuffer = (GL46ConstantBuffer)buffer
            };

            _commandList.Add(command);
        }

        public void SetDepthStencilState(IDepthStencilState depthStencilState)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetDepthStencilState,
            };

            _commandList.Add(command);
        }

        public void SetIndexBuffer(IIndexBuffer indexBuffer)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetIndexBuffer,
                IndexBuffer = (GL46IndexBuffer)indexBuffer
            };

            _commandList.Add(command);
        }

        public void SetInputLayout(IInputLayout inputLayout)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetInputLayout,
                InputLayout = (GL46InputLayout)inputLayout
            };

            _commandList.Add(command);
        }

        private void SetProgramPipeline(GL46Pipeline pipeline)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetPipeline,
                Pipeline = pipeline
            };

            _commandList.Add(command);
        }

        public void SetPixelShader(IShader pixelShader)
        {
            // unused, pipeline will bind the program
        }

        public void SetPrimitiveTopology(PrimitiveTopology primitiveTopology)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetPrimitiveTopology,
                PrimitiveType = primitiveTopology.ToOpenTK()
            };

            _commandList.Add(command);
        }

        public void SetRasterizerState(IRasterizerState rasterizerState)
        {

        }

        public void SetRenderTarget(TextureView renderTargets, TextureView depthStencilView)
        {

        }

        public void SetRenderTargets(TextureView[] renderTargets, TextureView depthStencilView)
        {

        }

        public void SetSampler(ISampler sampler)
        {

        }

        public void SetScissorRectangle(Rectangle rectangle)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetScissor,
                ScissorRectangle = new OpenTK.Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
            };

            _commandList.Add(command);
        }

        public void SetTexture(TextureView textureView)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetTextures,
                TextureView = ((GL46TextureView)textureView)
            };

            _commandList.Add(command);
        }

        public void SetVertexBuffer(IVertexBuffer vertexBuffer)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetVertexBuffer,
                InputLayout = _currentInputLayout,
                VertexBuffer = (GL46VertexBuffer)vertexBuffer,
                VertexStride = vertexBuffer.VertexStride
            };

            _commandList.Add(command);
        }

        public void SetVertexShader(IShader vertexShader)
        {
            // unused, pipeline will bind the program
        }

        public void SetViewport(Viewport viewport)
        {
            var command = new GLCommand
            {
                Type = CommandType.SetViewport,
                Viewport = viewport.ToOpenTK()
            };

            _commandList.Add(command);
        }

        public void Submit()
        {
            foreach (var command in _commandList)
            {
                //Debug.WriteLine(command.Type);
                switch (command.Type)
                {
                    case CommandType.Begin:
                        break;
                    case CommandType.End:
                        OpenTK.Graphics.OpenGL4.GL.BindVertexArray(0);
                        break;
                    case CommandType.Draw:
                        OpenTK.Graphics.OpenGL4.GL.DrawArrays(_currentPrimitiveType, 0, command.DrawVertexCount);
                        break;
                    case CommandType.DrawIndexed:
                        OpenTK.Graphics.OpenGL4.GL.DrawElements(_currentPrimitiveType, command.DrawIndexCount, DrawElementsType.UnsignedByte, command.DrawVertexCount);
                        break;
                    case CommandType.SetViewport:
                        OpenTK.Graphics.OpenGL4.GL.Viewport(command.Viewport);
                        break;
                    case CommandType.SetScissor:
                        OpenTK.Graphics.OpenGL4.GL.Scissor(command.ScissorRectangle.X, command.ScissorRectangle.Y, command.ScissorRectangle.Width, command.ScissorRectangle.Height);
                        break;
                    case CommandType.SetPrimitiveTopology:
                        _currentPrimitiveType = command.PrimitiveType;
                        break;
                    case CommandType.SetInputLayout:
                        OpenTK.Graphics.OpenGL4.GL.BindVertexArray(command.InputLayout);
                        _currentInputLayout = command.InputLayout;
                        break;
                    case CommandType.SetDepthStencilState:
                        break;
                    case CommandType.SetRasterizerState:
                        break;
                    case CommandType.SetBlendState:
                        break;
                    case CommandType.SetVertexBuffer:
                        OpenTK.Graphics.OpenGL4.GL.VertexArrayVertexBuffer(_currentInputLayout, 0, command.VertexBuffer, IntPtr.Zero, command.VertexStride);
                        break;
                    case CommandType.SetIndexBuffer:
                        OpenTK.Graphics.OpenGL4.GL.VertexArrayElementBuffer(_currentInputLayout, command.IndexBuffer);
                        break;
                    case CommandType.SetVertexShader:
                        break;
                    case CommandType.SetPixelShader:
                        break;
                    case CommandType.SetComputeShader:
                        break;
                    case CommandType.SetConstantBuffers:
                        OpenTK.Graphics.OpenGL4.GL.BindBufferBase(BufferRangeTarget.UniformBuffer, 0, command.ConstantBuffer);
                        break;
                    case CommandType.SetSamplers:
                        break;
                    case CommandType.SetTextures:
                        OpenTK.Graphics.OpenGL4.GL.BindTextureUnit(0, command.TextureView);
                        break;
                    case CommandType.SetRenderTarget:
                        break;
                    case CommandType.SetRenderTargets:
                        break;
                    case CommandType.ClearRenderTarget:
                        OpenTK.Graphics.OpenGL4.GL.ClearColor(command.ClearColor);
                        OpenTK.Graphics.OpenGL4.GL.Clear(ClearBufferMask.ColorBufferBit);
                        break;
                    case CommandType.ClearDepthStencil:
                        OpenTK.Graphics.OpenGL4.GL.ClearDepth(command.ClearDepth);
                        OpenTK.Graphics.OpenGL4.GL.ClearStencil(command.ClearStencil);
                        break;
                    case CommandType.SetPipeline:
                        OpenTK.Graphics.OpenGL4.GL.BindProgramPipeline(command.Pipeline);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _commandList.Clear();
        }
    }
}