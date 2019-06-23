using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.WIC;
using Xacor.Graphics.Api.DX;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;

namespace Xacor.Graphics.Api.DX11
{
    internal class DX11TextureFactory : ITextureFactory
    {
        private struct MipLevelDataStream
        {
            public DataStream DataStream;
            public int Stride;
        }

        private readonly DX11GraphicsDevice _graphicsDevice;
        private readonly ImagingFactory _imagingFactory = new ImagingFactory();

        public ITexture CreateRenderTarget(int width, int height, Format format)
        {
            const TextureViewType type = TextureViewType.RenderTarget | TextureViewType.ShaderResource;
            var resource = Create2DTexture(width, height, format, type);
            return new DX11Texture(_graphicsDevice, resource, width, height, 0, false, 1, type);
        }

        public ITexture CreateTextureFromFile(string filePath, bool createMipMaps)
        {
            var (resource, mipLevels, width, height) = CreateResourceFromFile(_graphicsDevice, new[] { filePath }, createMipMaps, false);
            return new DX11Texture(_graphicsDevice, resource, width, height, 0, false, mipLevels, TextureViewType.ShaderResource);
        }

        public void Dispose()
        {
            _imagingFactory?.Dispose();
        }

        public DX11TextureFactory(DX11GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        private static BindFlags TextureViewTypeToBindFlags(TextureViewType type)
        {
            var bindFlags = BindFlags.None;
            if ((type & TextureViewType.DepthStencil) > 0)
            {
                bindFlags |= BindFlags.DepthStencil;
            }

            if ((type & TextureViewType.RenderTarget) > 0)
            {
                bindFlags |= BindFlags.RenderTarget;
            }

            if ((type & TextureViewType.ShaderResource) > 0)
            {
                bindFlags |= BindFlags.ShaderResource;
            }

            return bindFlags;
        }

        private Resource Create1DTexture(in int width, Format format, TextureViewType type)
        {
            var texture1dDescription = new Texture1DDescription
            {
                BindFlags = TextureViewTypeToBindFlags(type),
                CpuAccessFlags = CpuAccessFlags.None,
                Format = format.ToSharpDX(),
                Width = width,
                ArraySize = 0,
                MipLevels = (int)Math.Log2(width),
                OptionFlags = ResourceOptionFlags.None,
                Usage = ResourceUsage.Default
            };
            return new Texture1D(_graphicsDevice, texture1dDescription);
        }

        private Resource Create2DTexture(in int width, in int height, Format format, TextureViewType type)
        {
            var texture2dDescription = new Texture2DDescription
            {
                BindFlags = TextureViewTypeToBindFlags(type),
                CpuAccessFlags = CpuAccessFlags.None,
                Format = format.ToSharpDX(),
                Width = width,
                Height = height,
                ArraySize = 0,
                MipLevels = (int)Math.Log2(width),
                OptionFlags = ResourceOptionFlags.None,
                Usage = ResourceUsage.Default
            };
            return new Texture2D(_graphicsDevice, texture2dDescription);
        }

        private Resource Create3DTexture(in int width, in int height, in int depth, Format format, TextureViewType type)
        {
            var texture3dDescription = new Texture3DDescription
            {
                BindFlags = TextureViewTypeToBindFlags(type),
                CpuAccessFlags = CpuAccessFlags.None,
                Format = format.ToSharpDX(),
                Width = width,
                Height = height,
                Depth = depth,
                MipLevels = 0,
                OptionFlags = ResourceOptionFlags.None,
                Usage = ResourceUsage.Default
            };
            return new Texture3D(_graphicsDevice, texture3dDescription);
        }

        private (Texture2D Resource, int MipLevels, int width, int height) CreateResourceFromFile(Device device, IReadOnlyCollection<string> filePaths, bool createMipMaps, bool isTextureCube)
        {
            var mipLevels = 1;
            var width = 0;
            var height = 0;

            var dataStreams = new List<MipLevelDataStream>();

            foreach (var filePath in filePaths)
            {
                using var bitmapDecoder = new BitmapDecoder(_imagingFactory, filePath, DecodeOptions.CacheOnDemand);

                mipLevels = createMipMaps ? bitmapDecoder.FrameCount : 1;
                var dataStreamsPerFile = new MipLevelDataStream[mipLevels];

                using (var formatConverter = new FormatConverter(_imagingFactory))
                using (var flipRotator = new BitmapFlipRotator(_imagingFactory))
                {
                    for (var mipLevel = 0; mipLevel < mipLevels; mipLevel++)
                    {
                        using var frame = bitmapDecoder.GetFrame(mipLevel);

                        formatConverter.Initialize(frame, PixelFormat.Format32bppPRGBA, BitmapDitherType.DualSpiral8x8, null, 0.0, BitmapPaletteType.Custom);
                        flipRotator.Initialize(formatConverter, BitmapTransformOptions.FlipVertical);
                        if (mipLevel == 0)
                        {
                            width = formatConverter.Size.Width;
                            height = formatConverter.Size.Height;
                        }

                        var stride = formatConverter.Size.Width * 4;
                        var buffer = new DataStream(formatConverter.Size.Height * stride, true, true);
                        flipRotator.CopyPixels(stride, buffer);

                        dataStreamsPerFile[mipLevel] = new MipLevelDataStream
                        {
                            DataStream = buffer,
                            Stride = stride
                        };
                    }
                }

                dataStreams.AddRange(dataStreamsPerFile);
            }

            var textureDescription = new Texture2DDescription
            {
                Width = width,
                Height = height,
                ArraySize = isTextureCube ? 6 : 1,
                BindFlags = BindFlags.ShaderResource,
                Usage = ResourceUsage.Default,
                CpuAccessFlags = CpuAccessFlags.Write,
                Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                MipLevels = mipLevels,
                OptionFlags = isTextureCube ? ResourceOptionFlags.TextureCube : ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0)
            };

            var texture = new Texture2D(device, textureDescription, dataStreams.Select(dataStream => new DataRectangle(dataStream.DataStream.DataPointer, dataStream.Stride)).ToArray());
            texture.DebugName = $"T_{filePaths.First()}_{textureDescription.Width}x{textureDescription.Height}";
            foreach (var mipLevelDataStream in dataStreams)
            {
                mipLevelDataStream.DataStream.Dispose();
            }
            return (texture, mipLevels, width, height);
        }
    }
}