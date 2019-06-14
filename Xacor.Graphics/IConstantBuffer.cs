using System;

namespace Xacor.Graphics
{
    public interface IConstantBuffer : IDisposable
    {
        void UpdateBuffer<T>(T constants) where T : struct;
    }
}