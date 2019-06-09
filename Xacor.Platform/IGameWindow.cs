using System;

namespace Xacor.Platform
{
    public interface IGameWindow : IDisposable
    {
        IntPtr Handle { get; }

        int Height { get; }

        string Title { get; set; }

        int Width { get; }

        bool IsOpen { get; }

        void Close();

        void Show();
    }
}