using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Xacor.Game;
using Xacor.Graphics;
using Xacor.Graphics.DX;
using Xacor.Platform;

namespace Xacor.Demo
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct InputBuffer
    {
        public Matrix4x4 ModelViewProjectionMatrix;
        public Matrix4x4 Padding1;
        public Matrix4x4 Padding2;
    }

    internal class DemoGame : GameBase
    {
        private readonly Options _options;

        private ICommandList _commandList;
        private IPipeline _simplePipeline;
        private Shader _simpleVertexShader;
        private Shader _simplePixelShader;
        private IPipeline _texturedPipeline;
        private Shader _texturedVertexShader;
        private Shader _texturedPixelShader;

        private Viewport _viewport;
        private IBlendState _defaultBlendState;
        private IDepthStencilState _defaultDepthStencilState;
        private IRasterizerState _defaultRasterizerState;

        private IVertexBuffer _simpleVertexBuffer;
        private IVertexBuffer _texturedVertexBuffer;
        private IConstantBuffer _leftConstantBuffer;
        private IConstantBuffer _rightConstantBuffer;

        private readonly ITextureFactory _textureFactory;
        private ITexture _simpleTexture;
        private ISampler _simpleSampler;

        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _projectionMatrix;
        private InputBuffer _leftMvp;
        private InputBuffer _rightMvp;

        public DemoGame(Options options, IGamePlatformFactory gamePlatformFactory, IGraphicsFactory graphicsFactory)
            : base(options, gamePlatformFactory, graphicsFactory)
        {
            _options = options;
            _textureFactory = graphicsFactory.CreateTextureFactory();
        }

        protected override void Draw()
        {
            _commandList.Begin("Clear", _simplePipeline);
            _commandList.SetRenderTarget(BackBufferView, BackBufferDepthStencilView);
            _commandList.ClearRenderTarget(BackBufferView, new Vector4(0.0863f, 0.0353f, 0.0706f, 1.0f));
            _commandList.ClearDepthStencil(BackBufferDepthStencilView, 1.0f, 1);

            _commandList.SetVertexBuffer(_simpleVertexBuffer);
            _commandList.SetConstantBuffer(_leftConstantBuffer, BufferScope.VertexShader);
            _commandList.Draw(3);
            //_commandList.End();

            //_commandList.Begin("SimpleTextured", _simplePipeline);
            _commandList.SetVertexBuffer(_simpleVertexBuffer);
            _commandList.SetConstantBuffer(_rightConstantBuffer, BufferScope.VertexShader);
            //_commandList.SetSampler(_simpleSampler);
            //_commandList.SetTexture(_simpleTexture.View);
            _commandList.Draw(3);
            _commandList.End();

            _commandList.Submit();
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.Title = "Xacor.Demo";

            _viewMatrix = Matrix4x4.CreateLookAt(new Vector3(0, 0, 4f), Vector3.Zero, Vector3.UnitY);
            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 4.0f, _options.Graphics.Resolution.Width / (float)_options.Graphics.Resolution.Height, 0.1f, 4096f);
            _leftMvp = new InputBuffer
            {
                ModelViewProjectionMatrix = Matrix4x4.CreateTranslation(-1.5f, 1, -0.5f) * _viewMatrix * _projectionMatrix
            };

            _rightMvp = new InputBuffer
            {
                ModelViewProjectionMatrix = Matrix4x4.CreateTranslation(1.5f, -1, 0.5f) * _viewMatrix * _projectionMatrix
            };

            _viewport = new Viewport(0, 0, _options.Graphics.Resolution.Width, _options.Graphics.Resolution.Height, 0.1f, 4096f);
            _defaultBlendState = GraphicsFactory.CreateBlendState(true, Blend.SourceAlpha, Blend.InverseSourceAlpha, BlendOperation.Add, Blend.One, Blend.Zero, BlendOperation.Add);
            _defaultDepthStencilState = GraphicsFactory.CreateDepthStencilState();
            _defaultRasterizerState = GraphicsFactory.CreateRasterizerState(CullMode.None, FillMode.Solid, true, false, false, false);

            var macros = new[] { ("TEST", "0") };
            _simpleVertexShader = GraphicsFactory.CreateShaderFromFile(ShaderStage.Vertex, "Assets/Shaders/_Simple.vs", VertexType.PositionColor, Enumerable.Empty<(string, string)>());
            _simplePixelShader = GraphicsFactory.CreateShaderFromFile(ShaderStage.Pixel, "Assets/Shaders/_Simple.ps", VertexType.PositionColor, macros);

            _texturedVertexShader = GraphicsFactory.CreateShaderFromFile(ShaderStage.Vertex, "Assets/Shaders/_SimpleTextured.vs", VertexType.PositionTexture, Enumerable.Empty<(string, string)>());
            _texturedPixelShader = GraphicsFactory.CreateShaderFromFile(ShaderStage.Pixel, "Assets/Shaders/_SimpleTextured.ps", VertexType.PositionTexture, macros);

            _simplePipeline = GraphicsFactory.CreatePipeline(_simpleVertexShader, _simplePixelShader, _simpleVertexShader.InputLayout, _defaultBlendState, _defaultDepthStencilState, _defaultRasterizerState, _viewport, PrimitiveTopology.TriangleList);
            _texturedPipeline = GraphicsFactory.CreatePipeline(_texturedVertexShader, _texturedPixelShader, _texturedVertexShader.InputLayout, _defaultBlendState, _defaultDepthStencilState, _defaultRasterizerState, _viewport, PrimitiveTopology.TriangleList);

            var verticesColored = new List<VertexPositionColor>
            {
                new VertexPositionColor(new Vector3(0.0f, -0.5f, 0.0f), new Vector4(0.7412f, 0.1059f, 0.4235f, 1.0f)),
                new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.0f), new Vector4(0.0863f, 0.0353f, 0.0706f, 1.0f)),
                new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.0f), new Vector4(0.9725f, 0.9451f, 0.5373f, 1.0f)),
            };
            _simpleVertexBuffer = GraphicsFactory.CreateVertexBuffer(verticesColored.ToArray());

            var verticesTextured = new List<VertexPositionTexture>
            {
                new VertexPositionTexture(new Vector3(0.0f, -0.5f, 0.0f), new Vector2(0.5f, 0.0f)),
                new VertexPositionTexture(new Vector3(0.5f, 0.5f, 0.0f), new Vector2(1.0f, 1.0f)),
                new VertexPositionTexture(new Vector3(-0.5f, 0.5f, 0.0f), new Vector2(0.0f, 1.0f)),
            };
            _texturedVertexBuffer = GraphicsFactory.CreateVertexBuffer(verticesTextured.ToArray());

            _commandList = GraphicsFactory.CreateCommandList();
            _leftConstantBuffer = GraphicsFactory.CreateConstantBuffer(_leftMvp);
            _rightConstantBuffer = GraphicsFactory.CreateConstantBuffer(_rightMvp);

            _simpleTexture = _textureFactory.CreateTextureFromFile("Assets/Textures/T_Default_D0.png", false);
            _simpleSampler = GraphicsFactory.CreateSampler(TextureAddressMode.Clamp, TextureAddressMode.Clamp, Filter.Nearest, ComparisonFunction.Always);
        }

        private float _counter = 0.0f;

        protected override void Update(double deltaTime)
        {
            _counter += (float)deltaTime;

            _leftMvp.ModelViewProjectionMatrix = Matrix4x4.CreateTranslation(0, 0, (float)Math.Sin(_counter)) * _viewMatrix * _projectionMatrix;
            _leftConstantBuffer.UpdateBuffer(_leftMvp);

            _rightMvp.ModelViewProjectionMatrix = Matrix4x4.CreateTranslation((float)Math.Cos(_counter), (float)Math.Cos(_counter), 0) * _viewMatrix * _projectionMatrix;
            //_rightMvp.ModelViewProjectionMatrix = _projectionMatrix * _viewMatrix * Matrix4x4.CreateTranslation((float)Math.Cos(_counter), (float)Math.Cos(_counter), 0);
            _rightConstantBuffer.UpdateBuffer(_rightMvp);
        }
    }
}