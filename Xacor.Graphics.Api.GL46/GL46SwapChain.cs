using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform;

namespace Xacor.Graphics.Api.GL46
{
    internal class GL46SwapChain : ISwapChain
    {
        private static readonly DebugProcArb _debugDelegate = ReceiveMessage;

        private static GCHandle? _debugDelegateHandle;
        private readonly IGraphicsContext _nativeContext;
        private readonly IWindowInfo _windowInfo;

        public TextureView TextureView { get; }

        public TextureView DepthStencilView { get; }
        
        private static void ReceiveMessage(DebugSource debugSource, DebugType type, int id, DebugSeverity severity, int messageLength, IntPtr messagePtr, IntPtr customObj)
        {
            var msg = Marshal.PtrToStringAnsi(messagePtr, messageLength);
            Debug.WriteLine("Source {0}; Type {1}; id {2}; Severity {3}; msg: '{4}'", debugSource, type, id, severity, msg);
        }
        
        [Conditional("DEBUG")]
        private static void TurnOnDebugging()
        {
            OpenTK.Graphics.OpenGL4.GL.Enable(EnableCap.DebugOutput);
            OpenTK.Graphics.OpenGL4.GL.Enable(EnableCap.DebugOutputSynchronous);
            _debugDelegateHandle = GCHandle.Alloc(_debugDelegate);
            OpenTK.Graphics.OpenGL4.GL.Arb.DebugMessageCallback(_debugDelegate, IntPtr.Zero);
        }

        public void Dispose()
        {
            _debugDelegateHandle?.Free();
            _nativeContext?.Dispose();
        }

        public GL46SwapChain(SwapChainDescriptor swapChainDescriptor)
        {
            var options = new ToolkitOptions();
            options.Backend = PlatformBackend.PreferNative;
            Toolkit.Init(options);

            _windowInfo = Utilities.CreateWindowsWindowInfo(swapChainDescriptor.WindowHandle);
            var graphicsContextFlags = GraphicsContextFlags.ForwardCompatible;
#if DEBUG
            graphicsContextFlags |= GraphicsContextFlags.Debug;
#endif
            _nativeContext = new GraphicsContext(GraphicsMode.Default, _windowInfo, null, 4, 6, graphicsContextFlags);
            _nativeContext.LoadAll();
            TurnOnDebugging();
            _nativeContext.MakeCurrent(_windowInfo);
            _nativeContext.SwapInterval = swapChainDescriptor.VSync ? 1 : 0;

            OpenTK.Graphics.OpenGL4.GL.Enable(EnableCap.CullFace);
            OpenTK.Graphics.OpenGL4.GL.CullFace(CullFaceMode.Back);
            OpenTK.Graphics.OpenGL4.GL.FrontFace(FrontFaceDirection.Ccw);

            OpenTK.Graphics.OpenGL4.GL.Enable(EnableCap.DepthTest);
            OpenTK.Graphics.OpenGL4.GL.DepthFunc(DepthFunction.Less);
        }

        public void Present()
        {
            _nativeContext.SwapBuffers();
        }
    }
}