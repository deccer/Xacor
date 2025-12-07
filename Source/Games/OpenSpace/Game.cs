using Xacor.Ecs;
using Xacor.Game;

namespace OpenSpace;

internal class Game : IGame
{
    private readonly Scene _scene;
    private bool _isDisposed;

    public Game(Scene scene)
    {
        _scene = scene;
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }
        
        _isDisposed = true;
    }

    public bool Initialize()
    {
        var playerEntity = _scene.CreateEntity();
        ref var position = ref _scene.Add<PositionComponent>(playerEntity);
        position.X = 100;
        position.Y = 100;
        
        ref var velocity = ref _scene.Add<VelocityComponent>(playerEntity);
        velocity.SpeedX = 3;
        velocity.SpeedY = 3;
        
        return true;
    }

    public void Shutdown()
    {
    }

    public void FixedUpdate(float deltaTime)
    {
    }

    public void VariableUpdate(float deltaTime)
    {
    }

    public string GetTitle()
    {
        return "OpenSpace";
    }
}