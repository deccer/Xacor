using System;

namespace Xacor.Graphics.Api
{
    public interface IIndexBuffer : IDisposable
    {
        bool Is16Bit { get; }
    }
}
