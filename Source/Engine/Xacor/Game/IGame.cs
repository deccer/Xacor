using System;

namespace Xacor.Game;

public interface IGame : IDisposable
{
    bool Initialize();
    
    void Shutdown();
    
    void FixedUpdate();
    
    void VariableUpdate();

    string GetTitle();
}