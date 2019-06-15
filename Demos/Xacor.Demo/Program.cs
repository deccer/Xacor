using System;
using System.Drawing;
using System.Windows.Forms;
using DryIoc;
using Xacor.Game;
using Xacor.Graphics;
using Xacor.Graphics.DX11;
using Xacor.Graphics.GL46;
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
            container.Register<Options>();
            container.Register<IGamePlatformFactory, Win32GamePlatformFactory>();
            container.RegisterInstance<DeviceType>(DeviceType.Hardware);
            container.Register<IGraphicsFactory, DX11GraphicsFactory>();
            container.Register<DemoGame>();
            return container;
        }

        [STAThread]
        public static /*async Task*/ void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var resolver = CreateResolver();

            using var game = resolver.Resolve<DemoGame>();

            game.Run();
        }
    }
}