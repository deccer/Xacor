using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Xacor.Game;
using Xacor.Graphics.Api;
using Xacor.Graphics.Meshes;
using Xacor.Input;
using Xacor.Mathematics;
using Xacor.Platform;

namespace Xacor.Demo
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct InputBuffer
    {
        public Matrix ModelViewProjectionMatrix;
        public Matrix Padding1;
        public Matrix Padding2;
    }

    internal class DemoGame : GameBase
    {
        private const string MoveForward = nameof(MoveForward);
        private const string MoveBackward = nameof(MoveBackward);
        private const string SlideLeft = nameof(SlideLeft);
        private const string SlideRight = nameof(SlideRight);

        private readonly Options _options;
        private readonly Size _windowResolution;
        private readonly Size _renderResolution;

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

        private Matrix _viewMatrix;
        private Matrix _projectionMatrix;
        private InputBuffer _leftMvp;
        private InputBuffer _rightMvp;

        private readonly Camera _camera;

        private float _counter;

        public DemoGame(
            Options options,
            IGamePlatformFactory gamePlatformFactory,
            IGraphicsFactory graphicsFactory,
            IInputFactory inputFactory)
            : base(options, gamePlatformFactory, graphicsFactory, inputFactory)
        {
            _options = options;
            _windowResolution = _options.Graphics.WindowResolution;
            _renderResolution = _options.Graphics.RenderResolution;
            _textureFactory = graphicsFactory.CreateTextureFactory();

            _camera = new Camera(new Vector3(0, 0, 5), _windowResolution.Width / (float)_windowResolution.Height);
        }

        protected override void Cleanup()
        {
            base.Cleanup();

            _defaultBlendState?.Dispose();
            _defaultDepthStencilState?.Dispose();
            _defaultRasterizerState?.Dispose();
            _leftConstantBuffer?.Dispose();
            _rightConstantBuffer?.Dispose();
            _simplePipeline?.Dispose();
            _simpleVertexBuffer?.Dispose();
            _simplePixelShader?.Dispose();
            _simpleSampler?.Dispose();
            _simpleVertexShader?.Dispose();
            _textureFactory?.Dispose();
            _texturedPipeline?.Dispose();
            _texturedPixelShader?.Dispose();
            _texturedVertexShader?.Dispose();
            _texturedVertexBuffer?.Dispose();
        }

        protected override void Draw()
        {
            _commandList.Submit();
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.Title = "Xacor.Demo";

            _viewMatrix = Matrix.LookAtRH(new Vector3(0, 0, 10f), Vector3.Zero, Vector3.UnitY);
            _projectionMatrix = Matrix.PerspectiveFovRH(MathF.PI / 3.0f, _renderResolution.Width / (float)_renderResolution.Height, 0.1f, 4096f);
            _leftMvp = new InputBuffer
            {
                ModelViewProjectionMatrix = Matrix.Translation(-1.5f, 1, -0.5f) * _viewMatrix * _projectionMatrix
            };

            _rightMvp = new InputBuffer
            {
                ModelViewProjectionMatrix = Matrix.Translation(1.5f, -1, 0.5f) * _viewMatrix * _projectionMatrix
            };

            _viewport = new Viewport(0, 0, _renderResolution.Width, _renderResolution.Height, 0.1f, 4096f);
            _defaultBlendState = GraphicsFactory.CreateBlendState(true, Blend.SourceAlpha, Blend.InverseSourceAlpha, BlendOperation.Add, Blend.One, Blend.Zero, BlendOperation.Add);
            _defaultDepthStencilState = GraphicsFactory.CreateDepthStencilState();
            _defaultRasterizerState = GraphicsFactory.CreateRasterizerState(CullMode.None, FillMode.Solid, true, false, false, false);

            var macros = new[] { ("TEST", "0") };
            _simpleVertexShader = GraphicsFactory.CreateShaderFromFile(ShaderStage.Vertex, "Assets/Shaders/_Simple.vs", VertexType.PositionColor, Enumerable.Empty<(string, string)>());
            _simplePixelShader = GraphicsFactory.CreateShaderFromFile(ShaderStage.Pixel, "Assets/Shaders/_Simple.ps", VertexType.PositionColor, macros);

            _texturedVertexShader = GraphicsFactory.CreateShaderFromFile(ShaderStage.Vertex, "Assets/Shaders/_SimpleTextured.vs", VertexType.PositionTexture, Enumerable.Empty<(string, string)>());
            _texturedPixelShader = GraphicsFactory.CreateShaderFromFile(ShaderStage.Pixel, "Assets/Shaders/_SimpleTextured.ps", VertexType.PositionTexture, macros);

            _simplePipeline = GraphicsFactory.CreatePipeline(
                _simpleVertexShader,
                _simplePixelShader,
                _simpleVertexShader.InputLayout,
                _defaultBlendState,
                _defaultDepthStencilState,
                _defaultRasterizerState,
                _viewport,
                PrimitiveTopology.TriangleList);
            _texturedPipeline = GraphicsFactory.CreatePipeline(
                _texturedVertexShader,
                _texturedPixelShader,
                _texturedVertexShader.InputLayout,
                _defaultBlendState,
                _defaultDepthStencilState,
                _defaultRasterizerState,
                _viewport,
                PrimitiveTopology.TriangleList);

            var verticesColored = new List<VertexPositionColor>
            {
                new (new Vector3(0.0f, -0.5f, 0.0f), new Vector4(0.7412f, 0.1059f, 0.4235f, 1.0f)),
                new (new Vector3(0.5f, 0.5f, 0.0f), new Vector4(0.0863f, 0.0353f, 0.0706f, 1.0f)),
                new (new Vector3(-0.5f, 0.5f, 0.0f), new Vector4(0.9725f, 0.9451f, 0.5373f, 1.0f)),
            };
            _simpleVertexBuffer = GraphicsFactory.CreateVertexBuffer(verticesColored.ToArray());

            var verticesTextured = new List<VertexPositionTexture>
            {
                new(new Vector3(0.0f, -0.5f, 0.0f), new Vector2(0.5f, 0.0f)),
                new(new Vector3(0.5f, 0.5f, 0.0f), new Vector2(1.0f, 1.0f)),
                new(new Vector3(-0.5f, 0.5f, 0.0f), new Vector2(0.0f, 1.0f)),
            };
            _texturedVertexBuffer = GraphicsFactory.CreateVertexBuffer(verticesTextured.ToArray());

            var meshFactory = new MeshFactory(GraphicsFactory);
            var cubeMesh = meshFactory.CreateUnitCubeMesh();

            _commandList = GraphicsFactory.CreateCommandList();
            _leftConstantBuffer = GraphicsFactory.CreateConstantBuffer(_leftMvp);
            _rightConstantBuffer = GraphicsFactory.CreateConstantBuffer(_rightMvp);

            _simpleTexture = _textureFactory.CreateTextureFromFile("Assets/Textures/T_Default_D0.png", false);
            _simpleSampler = GraphicsFactory.CreateSampler(TextureAddressMode.Clamp, TextureAddressMode.Clamp, Filter.Nearest, ComparisonFunction.Always);

            _commandList.Begin("Clear", _simplePipeline);
            _commandList.SetRenderTarget(BackBufferView, BackBufferDepthStencilView);
            _commandList.ClearRenderTarget(BackBufferView, new Vector4(0.0863f, 0.0353f, 0.0706f, 1.0f));
            _commandList.ClearDepthStencil(BackBufferDepthStencilView, 1.0f, 1);

            _commandList.SetVertexBuffer(cubeMesh.VertexBuffer);
            _commandList.SetConstantBuffer(_leftConstantBuffer, BufferScope.VertexShader);
            _commandList.Draw(36);
            _commandList.End();

            _commandList.Begin("SimpleTextured", _texturedPipeline);
            _commandList.SetVertexBuffer(_texturedVertexBuffer);
            _commandList.SetConstantBuffer(_rightConstantBuffer, BufferScope.VertexShader);
            _commandList.SetSampler(_simpleSampler);
            _commandList.SetTexture(_simpleTexture.View);
            _commandList.Draw(3);
            _commandList.End();
        }

        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            const float boost = 5.0f;
            if (Input.IsButtonDown(MoveForward))
            {
                _camera.Position += _camera.Front * deltaTime * boost;
            }
            if (Input.IsButtonDown(MoveBackward))
            {
                _camera.Position -= _camera.Front * deltaTime * boost;
            }

            var horizontalAxis = Input.GetAxis("Horizontal");
            // _camera.Position += _camera.Right * (float)deltaTime * horizontalAxis;

            if (Input.IsButtonDown(SlideLeft))
            {
                _camera.Position -= _camera.Right * deltaTime * boost;
            }
            if (Input.IsButtonDown(SlideRight))
            {
                _camera.Position += _camera.Right * deltaTime * boost;
            }

            _viewMatrix = _camera.GetViewMatrix();
            _projectionMatrix = _camera.GetProjectionMatrix();

            _counter += deltaTime;

            _leftMvp.ModelViewProjectionMatrix = Matrix.Scaling(1, 1 + (float)Math.Sin(_counter) * 0.5f, 1) *
                                                 Matrix.RotationY(_counter * 2f) *
                                                 Matrix.Translation(-4, 0, 0) *
                                                 Matrix.Translation(0.0f, (float)Math.Sin(_counter) * 4, 0.0f) *
                                                 _viewMatrix *
                                                 _projectionMatrix;
            _leftConstantBuffer.UpdateBuffer(_leftMvp);

            _rightMvp.ModelViewProjectionMatrix = Matrix.Translation((float)Math.Cos(_counter), (float)Math.Cos(_counter), 0) *
                                                  _viewMatrix *
                                                  _projectionMatrix;
            _rightConstantBuffer.UpdateBuffer(_rightMvp);
        }
    }
}
