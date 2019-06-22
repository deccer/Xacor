using System;

namespace Xacor.Graphics.Api
{
    public interface IConstantBuffer : IDisposable
    {
        void UpdateBuffer<T>(T constants) where T : struct;
    }
}