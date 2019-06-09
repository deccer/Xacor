using System;

namespace Xacor.Platform
{
    public interface IGameLoop
    {
        void Run(IGameWindow gameWindow, Action tickCallback);
    }
}