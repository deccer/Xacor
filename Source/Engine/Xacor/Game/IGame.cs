using System;

namespace Xacor.Game;

public interface IGame : IDisposable
{
    bool Initialize();
    
    void Shutdown();
    
    void FixedUpdate(float deltaTime);
    
    void VariableUpdate(float deltaTime);

    string GetTitle();
}