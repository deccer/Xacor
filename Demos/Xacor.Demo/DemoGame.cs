using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Xacor.Game;
using Xacor.Graphics.Api;
using Xacor.Graphics.Api.DX;
using Xacor.Input;
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
        private const string MoveForward = nameof(MoveForward);
        private const string MoveBackward = nameof(MoveBackward);
        private const string SlideLeft = nameof(SlideLeft);
        private const string SlideRight = nameof(SlideRight);

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

        private IVertexBuffer _cubeVertexBuffer;
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

        private readonly Camera _camera;

        public DemoGame(Options options, IGamePlatformFactory gamePlatformFactory, IGraphicsFactory graphicsFactory, IInputFactory inputFactory)
            : base(options, gamePlatformFactory, graphicsFactory, inputFactory)
        {
            _options = options;
            _textureFactory = graphicsFactory.CreateTextureFactory();

            _camera = new Camera(new Vector3(0, 0, 5), _options.Graphics.Resolution.Width / (float)_options.Graphics.Resolution.Height);
        }

        protected override void Cleanup()
        {
            base.Cleanup();

            _cubeVertexBuffer?.Dispose();
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
            _commandList.Begin("Clear", _simplePipeline);
            _commandList.SetRenderTarget(BackBufferView, BackBufferDepthStencilView);
            _commandList.ClearRenderTarget(BackBufferView, new Vector4(0.0863f, 0.0353f, 0.0706f, 1.0f));
            _commandList.ClearDepthStencil(BackBufferDepthStencilView, 1.0f, 1);

            _commandList.SetVertexBuffer(_cubeVertexBuffer);
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

            _commandList.Submit();
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.Title = "Xacor.Demo";

            _viewMatrix = Matrix4x4.CreateLookAt(new Vector3(0, 0, 10f), Vector3.Zero, Vector3.UnitY);
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

            var cubeColor = new Vector4(112 / 256f, 53 / 256f, 63 / 256f, 1.0f);
            var cubeVertices = new List<VertexPositionColor>
            {
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Front 
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
                                                  
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor), // BACK 
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
                                                  
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor), // Top 
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
                                                  
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Bottom 
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
                                                  
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor), // Left 
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, -1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(-1.0f, 1.0f, -1.0f), cubeColor),
                                                  
                new VertexPositionColor(new Vector3(1.0f, -1.0f, -1.0f), cubeColor), // Right 
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, 1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, -1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, -1.0f), cubeColor),
                new VertexPositionColor(new Vector3(1.0f, 1.0f, 1.0f), cubeColor)
            };
            _cubeVertexBuffer = GraphicsFactory.CreateVertexBuffer(cubeVertices.ToArray());

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

        private float _counter;

        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (Input.IsButtonDown(MoveForward))
            {
                _camera.Position += _camera.Front * deltaTime;
            }
            if (Input.IsButtonDown(MoveBackward))
            {
                _camera.Position -= _camera.Front * deltaTime;
            }
            
            var horizontalAxis = Input.GetAxis("Horizontal");
            //_camera.Position += _camera.Right * (float)deltaTime * horizontalAxis;

            if (Input.IsButtonDown(SlideLeft))
            {
                _camera.Position -= _camera.Right * deltaTime;
            }
            if (Input.IsButtonDown(SlideRight))
            {
                _camera.Position += _camera.Right * deltaTime;
            }

            _viewMatrix = _camera.GetViewMatrix();
            _projectionMatrix = _camera.GetProjectionMatrix();

            _counter += deltaTime;

            _leftMvp.ModelViewProjectionMatrix = /*Matrix4x4.CreateScale(1, 1 + (float)System.Math.Sin(_counter) * 0.5f, 1) * */
                                                 Matrix4x4.CreateRotationY(_counter * 2f) *
                                                 Matrix4x4.CreateTranslation(-4, 0, 0) * _viewMatrix * _projectionMatrix;
            _leftConstantBuffer.UpdateBuffer(_leftMvp);

            _rightMvp.ModelViewProjectionMatrix = Matrix4x4.CreateTranslation((float)System.Math.Cos(_counter), (float)System.Math.Cos(_counter), 0) * _viewMatrix * _projectionMatrix;
            _rightConstantBuffer.UpdateBuffer(_rightMvp);
        }
    }
}