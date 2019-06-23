using System;
using System.Drawing;
using System.Windows.Forms;
using DryIoc;
using Xacor.Graphics.Api;
using Xacor.Graphics.Api.GL46;
using Xacor.Graphics.Api.DX11;
using Xacor.Platform;
using Xacor.Platform.Windows;

namespace Xacor.Demo
{
    internal static class Program
    {
        private static IResolver CreateResolver()
        {
            var container = new Container(rules => rules.WithTrackingDisposableTransients());
            //container.Register<IProfiler>();
            container.RegisterInstance<GraphicsOptions>(new GraphicsOptions(new Size(1920, 1080), WindowState.Windowed));
            container.Register<Options>(Reuse.Singleton);
            container.Register<IGamePlatformFactory, Win32GamePlatformFactory>(Reuse.Singleton);
            container.RegisterInstance<DeviceType>(DeviceType.Hardware);
            //container.Register<IGraphicsFactory, DX11GraphicsFactory>(Reuse.Singleton);
            container.Register<IGraphicsFactory, GL46GraphicsFactory>(Reuse.Singleton);
            container.Register<DemoGame>(Reuse.Singleton);
            return container;
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var resolver = CreateResolver();

            using var game = resolver.Resolve<DemoGame>();

            game.Run();
        }
    }
}