using System;

namespace Xacor.Graphics
{
    public interface IIndexBuffer : IDisposable
    {
        bool Is16Bit { get; }
    }
}