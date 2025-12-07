using System;

namespace Xacor;

public interface IApplication : IDisposable
{
    void Run();
}