using System;
using System.Drawing;
using System.Windows.Forms;

namespace Xacor.Platform.Windows
{
    internal class Win32GameWindow : IGameWindow
    {
        private const string WindowTitle = "Xacor";

        private readonly GraphicsOptions _graphicsOptions;
        private readonly MainView _mainView;

        public IntPtr Handle { get; }

        public int Height => _mainView.ClientSize.Height;

        public string Title
        {
            get => _mainView.Text;
            set => _mainView.Text = value;
        }

        public int Width => _mainView.ClientSize.Width;

        public bool IsOpen { get; private set; } = true;

        public void Close()
        {
            _mainView.Close();
        }

        public void Dispose()
        {
            _mainView.Dispose();
        }

        internal Win32GameWindow(GraphicsOptions graphicsOptions)
        {
            _graphicsOptions = graphicsOptions ?? throw new ArgumentNullException(nameof(graphicsOptions));

            _mainView = new MainView
            {
                Text = WindowTitle,
                StartPosition = FormStartPosition.CenterScreen,
                ClientSize = new Size(_graphicsOptions.Resolution.Width, _graphicsOptions.Resolution.Height),
                FormBorderStyle = _graphicsOptions.WindowState == WindowState.Windowed ? FormBorderStyle.FixedSingle : FormBorderStyle.None
            };

            _mainView.Closed += (_, __) => { IsOpen = false; };

            Handle = _mainView.Handle;
        }

        public void Show()
        {
            _mainView.Show();
        }
    }
}