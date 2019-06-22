using System;

namespace Xacor.Graphics.Api
{
    public readonly struct SwapChainInfo
    {
        public readonly IntPtr WindowHandle;
        public readonly int Height;
        public readonly bool IsWindowed;
        public readonly int Width;
        public readonly SwapEffect SwapEffect;

        public SwapChainInfo(IntPtr windowHandle, int width, int height, bool isWindowed, SwapEffect swapEffect)
        {
            WindowHandle = windowHandle;
            Width = width;
            Height = height;
            IsWindowed = isWindowed;
            SwapEffect = swapEffect;
        }
    }
}