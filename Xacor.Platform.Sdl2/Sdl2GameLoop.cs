using System;
using SDL2;

namespace Xacor.Platform.Sdl2
{
    internal sealed class Sdl2GameLoop : IGameLoop
    {
        public void Run(IGameWindow gameWindow, Action tickCallback)
        {
            while (gameWindow.IsOpen)
            {
                SDL.SDL_PollEvent(out var ev);
                if (ev.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    gameWindow.Close();
                }

                tickCallback();
            }
        }
    }
}