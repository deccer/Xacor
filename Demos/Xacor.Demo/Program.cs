using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Xacor.Graphics.Api;
using Xacor.Graphics.Api.GL46;
using Xacor.Graphics.Api.D3D11;
using Xacor.Input;
using Xacor.Input.DirectInput;
using Xacor.Platform;
using Xacor.Platform.Windows;

namespace Xacor.Demo
{
    internal static class Program
    {
        private static IServiceProvider CreateCompositionRoot()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.RollingFile("logs\\{date}.log")
                .CreateLogger();

            var services = new ServiceCollection();
            services.AddSingleton(Log.Logger);
            //services.AddSingleton<IProfiler>();
            
            var inputMappings = new List<InputMapping>
            {
                new KeyboardInputMapping("MoveForward", InputButton.W, InputButton.Mouse1),
                new KeyboardInputMapping("MoveBackward", InputButton.S, InputButton.Mouse2),
                new KeyboardInputMapping("SlideLeft", InputButton.A),
                new KeyboardInputMapping("SlideRight", InputButton.D),

                new MouseInputMapping("Horizontal", Axis.Horizontal),
                new MouseInputMapping("Vertical", Axis.Vertical),
            };

            var graphicsOptions = new GraphicsOptions(
                RenderApi.OpenGL,
                new Size(1680, 800),
                new Size(672, 320),
                WindowState.Windowed,
                false);

            services.AddSingleton(inputMappings);
            services.AddSingleton<InputOptions>();
            services.AddSingleton(graphicsOptions);
            #if DEBUG
            services.AddSingleton(new HardwareOptions(true, false));
            #else
            services.AddSingleton(new HardwareOptions(true, false));
            #endif
            services.AddSingleton<Options>();
            services.AddSingleton<IGamePlatformFactory, Win32GamePlatformFactory>();
            switch (graphicsOptions.RenderApi)
            {
                case RenderApi.D3D11:
                    services.AddSingleton<IGraphicsFactory, D3D11GraphicsFactory>();
                    break;
                case RenderApi.OpenGL:
                    services.AddSingleton<IGraphicsFactory, GL46GraphicsFactory>();
                    break;
            }
            services.AddSingleton<InputMapper>();
            services.AddSingleton<IInputFactory, DirectInputInputFactory>();
            services.AddSingleton<DemoGame>();
            return services.BuildServiceProvider();
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var compositionRoot = CreateCompositionRoot();
            using var game = compositionRoot.GetService<DemoGame>();

            game.Run();
        }
    }
}