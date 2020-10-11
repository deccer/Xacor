using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Xacor.Graphics.Api;
using Xacor.Graphics.Api.GL46;
using Xacor.Graphics.Api.D3D11;
using Xacor.Graphics.Api.GL33;
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
            var loggerConfiguration = new LoggerConfiguration();
            loggerConfiguration
                .WriteTo.Console()
                .WriteTo.RollingFile("logs\\{date}.log");

            var logger = loggerConfiguration.CreateLogger();
            var services = new ServiceCollection();
            services.AddSingleton<ILogger>(logger);
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


            services.AddSingleton(inputMappings);
            services.AddSingleton<InputOptions>();
            services.AddSingleton(new GraphicsOptions(new Size(1920, 1080), WindowState.Windowed, false));
            #if DEBUG
            services.AddSingleton(new HardwareOptions(true, true));
            #else
            services.AddSingleton(new HardwareOptions(true, false));
            #endif
            services.AddSingleton<Options>();
            services.AddSingleton<IGamePlatformFactory, Win32GamePlatformFactory>();
            //services.AddSingleton<IGraphicsFactory, D3D11GraphicsFactory>();
            services.AddSingleton<IGraphicsFactory, GL46GraphicsFactory>();
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