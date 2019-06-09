using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Xacor.Platform.Windows.Native;

namespace Xacor.Platform.Windows
{
    internal class Win32GameLoop : IGameLoop
    {
        public void Run(IGameWindow gameWindow, Action tickCallback)
        {
            gameWindow.Show();

            while (NextFrame(gameWindow))
            {
                tickCallback();
            }
        }

        private static bool NextFrame(IGameWindow gameWindow)
        {
            if (gameWindow.IsOpen)
            {
                while (Win32Native.PeekMessage(out _, IntPtr.Zero, 0, 0, 0) != 0)
                {
                    if (Win32Native.GetMessage(out var msg, IntPtr.Zero, 0, 0) == -1)
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                            "An error happened in rendering loop while processing windows messages. Error: {0}",
                            Marshal.GetLastWin32Error()));
                    }

                    var message = new Message
                    {
                        HWnd = msg.handle,
                        LParam = msg.lParam,
                        Msg = (int)msg.msg,
                        WParam = msg.wParam
                    };

                    if (!Application.FilterMessage(ref message))
                    {
                        Win32Native.TranslateMessage(ref msg);
                        Win32Native.DispatchMessage(ref msg);
                    }
                }
            }

            return gameWindow.IsOpen;
        }
    }
}