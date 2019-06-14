using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SharpDX.Mathematics.Interop;
using Xacor.Game;
using Xacor.Graphics;
using Xacor.Graphics.DX;
using Xacor.Platform;

namespace Xacor.Demo
{
    struct InputBuffer
    {
        public Matrix4x4 ModelMatrix;
        public Matrix4x4 ViewMatrix;
        public Matrix4x4 ProjectionMatrix;
    }

    internal class DemoGame : GameBase
    {
        private readonly GameOptions _gameOptions;

        private ICommandList _commandList;
        private IPipeline _defaultPipeline;
        private Shader _simpleVertexShader;
        private Shader _simplePixelShader;

        private Viewport _viewport;
        private IBlendState _defaultBlendState;
        private IDepthStencilState _defaultDepthStencilState;
        private IRasterizerState _defaultRasterizerState;

        private IVertexBuffer _simpleVertexBuffer;
        private IConstantBuffer _simpleConstantBuffer;

        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _projectionMatrix;
        private InputBuffer _mvp;

        public DemoGame(GameOptions gameOptions, IGamePlatformFactory gamePlatformFactory, IGraphicsFactory graphicsFactory)
            : base(gamePlatformFactory, graphicsFactory)
        {
            _gameOptions = gameOptions;
        }

        protected override void Draw()
        {
            _commandList.Begin("Test", _defaultPipeline);
            _commandList.SetRenderTarget(BackBufferView, BackBufferDepthStencilView);
            _commandList.ClearRenderTarget(BackBufferView, new Vector4(0.0863f, 0.0353f, 0.0706f, 1.0f));
            _commandList.ClearDepthStencil(BackBufferDepthStencilView, 1.0f, 1);
            _commandList.SetVertexBuffer(_simpleVertexBuffer);
            _commandList.SetConstantBuffer(_simpleConstantBuffer, BufferScope.VertexShader);
            _commandList.Draw(3);
            _commandList.End();

            _commandList.Submit();
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.Title = "Xacor.Demo";

            _viewMatrix = Matrix4x4.CreateLookAt(new Vector3(0, 0, -2f), Vector3.Zero, Vector3.UnitY);
            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 4.0f, _gameOptions.Graphics.Resolution.Width / (float)_gameOptions.Graphics.Resolution.Height, 0.1f, 4096f);
            _mvp = new InputBuffer
            {
                ModelMatrix = Matrix4x4.Identity,
                ProjectionMatrix = _projectionMatrix,
                ViewMatrix = _viewMatrix
            };

            _viewport = new Viewport(0, 0, _gameOptions.Graphics.Resolution.Width, _gameOptions.Graphics.Resolution.Height, 0.1f, 4096f);

            var macros = new[] { ("TEST", "1") };
            _simpleVertexShader = GraphicsFactory.CreateShaderFromFile(ShaderStage.Vertex, "Assets/Shaders/_Simple.vs.hlsl", VertexType.PositionColor, Enumerable.Empty<(string, string)>());
            _simplePixelShader = GraphicsFactory.CreateShaderFromFile(ShaderStage.Pixel, "Assets/Shaders/_Simple.ps.hlsl", VertexType.PositionColor, macros);
            _defaultBlendState = GraphicsFactory.CreateBlendState(true, Blend.One, Blend.One, BlendOperation.Add, Blend.One, Blend.One, BlendOperation.Add);
            _defaultDepthStencilState = GraphicsFactory.CreateDepthStencilState();
            _defaultRasterizerState =
                GraphicsFactory.CreateRasterizerState(CullMode.Back, FillMode.Solid, true, false, false, false);

            _defaultPipeline = GraphicsFactory.CreatePipeline(_simpleVertexShader, _simplePixelShader, _simpleVertexShader.InputLayout,
                _defaultBlendState, _defaultDepthStencilState, _defaultRasterizerState, _viewport, PrimitiveTopology.TriangleList);

            var vertices = new List<VertexPositionColor>
            {
                new VertexPositionColor(new Vector3(0.0f, -0.5f, 0.0f), new Vector4(0.7412f, 0.1059f, 0.4235f, 1.0f)),
                new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.0f), new Vector4(0.0863f, 0.0353f, 0.0706f, 1.0f)),
                new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.0f), new Vector4(0.9725f, 0.9451f, 0.5373f, 1.0f)),
            };
            _simpleVertexBuffer = GraphicsFactory.CreateVertexBuffer(vertices.ToArray());

            _commandList = GraphicsFactory.CreateCommandList();
            _simpleConstantBuffer = GraphicsFactory.CreateConstantBuffer(_mvp);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}