using System;
using System.Collections.Generic;

namespace Xacor.Graphics.DX11
{
    public class DX11GraphicsFactory : IGraphicsFactory
    {
        private readonly DX11GraphicsDevice _graphicsDevice;

        public DX11GraphicsFactory(DeviceType deviceType)
        {
            _graphicsDevice = new DX11GraphicsDevice(deviceType);
        }

        public ICommandList CreateCommandList()
        {
            return new DX11CommandList(_graphicsDevice);
        }

        public IShader CreateShaderFromFile(string filePath)
        {
            return new DX11Shader(_graphicsDevice, this);
        }

        public ISwapChain CreateSwapchain(SwapChainInfo swapChainInfo)
        {
            return new DX11SwapChain(_graphicsDevice, swapChainInfo);
        }

        internal DX11InputLayout CreateInputLayout(VertexType vertexType, byte[] shaderByteCode)
        {
            var attributes = new List<VertexAttribute>();

            switch (vertexType)
            {
                case VertexType.Position:
                    attributes.Add(new VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    break;
                case VertexType.PositionColor:
                    attributes.Add(new VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("COLOR", 0, 0, 12, Format.R32G32B32A32Float));
                    break;
                case VertexType.PositionTexture:
                    attributes.Add(new VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("TEXTURE", 0, 0, 12, Format.R32G32Float));
                    break;
                case VertexType.PositionTextureNormalTangent:
                    attributes.Add(new VertexAttribute("POSITION", 0, 0, 0, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("TEXTURE", 0, 0, 12, Format.R32G32Float));
                    attributes.Add(new VertexAttribute("NORMAL", 0, 0, 20, Format.R32G32B32Float));
                    attributes.Add(new VertexAttribute("TANGENT", 0, 0, 32, Format.R32G32B32Float));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(vertexType), vertexType, null);
            }

            return new DX11InputLayout(_graphicsDevice, shaderByteCode, attributes);
        }
    }
}