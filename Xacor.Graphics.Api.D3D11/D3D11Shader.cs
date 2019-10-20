using System;
using System.IO;
using System.Linq;
using System.Text;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace Xacor.Graphics.Api.D3D11
{
    internal class D3D11Shader : Shader
    {
        public readonly struct VirtualShaderFile : IEquatable<VirtualShaderFile>
        {
            public readonly string Name;
            public readonly string Text;

            public VirtualShaderFile(string name, string text)
            {
                Name = name;
                Text = text;
            }

            public bool Equals(VirtualShaderFile other)
            {
                return string.Equals(Name, other.Name) && string.Equals(Text, other.Text);
            }

            public override bool Equals(object obj)
            {
                return obj is VirtualShaderFile other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                }
            }
        }

        private class IncludeHandler : Include
        {
            private readonly VirtualShaderFile[] _virtualShaderFiles;

            public void Dispose() { }
            public IDisposable Shadow { get; set; }

            public IncludeHandler(VirtualShaderFile[] virtualShaderFiles)
            {
                _virtualShaderFiles = virtualShaderFiles;
            }

            public Stream Open(IncludeType type, string fileName, Stream parentStream)
            {
                var filePath = Path.Combine("Assets/Shaders/", fileName);

                VirtualShaderFile? virtualShaderFile = null;
                if (_virtualShaderFiles != null)
                {
                    virtualShaderFile = _virtualShaderFiles.FirstOrDefault(vsf => vsf.Name.ToLower().Equals(fileName.ToLower()));
                }

                if (virtualShaderFile == null)
                {
                    if (File.Exists(filePath))
                    {
                        return File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    }

                    throw new FileNotFoundException($"Include file '{filePath}' could not be found.");
                }
                return new MemoryStream(Encoding.ASCII.GetBytes(virtualShaderFile.Value.Text));
            }

            public void Close(Stream stream)
            {
                stream?.Close();
            }
        }

        private readonly D3D11GraphicsDevice _graphicsDevice;
        private readonly D3D11GraphicsFactory _graphicsFactory;
        private ShaderBytecode _shaderBytecode;
        private DeviceChild _shaderObject;

        public D3D11Shader(D3D11GraphicsDevice graphicsDevice, D3D11GraphicsFactory graphicsFactory)
        {
            _graphicsDevice = graphicsDevice;
            _graphicsFactory = graphicsFactory;
        }

        protected override void CompileInternal(ShaderStage shaderStage, string filePath, VertexType vertexType)
        {
            var macros = Macros.Select(macro => new ShaderMacro(macro.Key, macro.Value)).ToArray();

            using var includeHandler = new IncludeHandler(null);

            _shaderBytecode = LoadBytecode(shaderStage, filePath, includeHandler, macros);

            if (shaderStage == ShaderStage.Vertex && vertexType != VertexType.Unknown)
            {
                InputLayout = _graphicsFactory.CreateInputLayout(vertexType, _shaderBytecode);
            }

            switch (shaderStage)
            {
                case ShaderStage.Vertex:
                    _shaderObject = new VertexShader(_graphicsDevice, _shaderBytecode);
                    break;
                case ShaderStage.Pixel:
                    _shaderObject = new PixelShader(_graphicsDevice, _shaderBytecode);
                    break;
                case ShaderStage.Compute:
                    _shaderObject = new ComputeShader(_graphicsDevice, _shaderBytecode);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(shaderStage), shaderStage, null);
            }
        }

        public static implicit operator DeviceChild(D3D11Shader shader)
        {
            return shader._shaderObject;
        }

        private static ShaderBytecode LoadBytecode(ShaderStage shaderStage, string filePath, Include includeHandler, ShaderMacro[] macros)
        {
            var shaderFlags = ShaderFlags.None;
#if DEBUG
            shaderFlags |= ShaderFlags.Debug | ShaderFlags.PreferFlowControl | ShaderFlags.SkipOptimization;
#endif
            var compilationResult = ShaderBytecode.CompileFromFile(filePath, "Main", ShaderStageToProfile(shaderStage), shaderFlags, EffectFlags.None, macros, includeHandler);
            if (compilationResult.HasErrors)
            {
                throw new Exception(compilationResult.Message);
            }

            return compilationResult.Bytecode;
        }

        private static string ShaderStageToProfile(ShaderStage shaderStage)
        {
            switch (shaderStage)
            {
                case ShaderStage.Vertex:
                    return "vs_5_0";
                case ShaderStage.Pixel:
                    return "ps_5_0";
                case ShaderStage.Compute:
                    return "cs_5_0";
                default:
                    throw new ArgumentOutOfRangeException(nameof(shaderStage), shaderStage, null);
            }
        }
    }
}