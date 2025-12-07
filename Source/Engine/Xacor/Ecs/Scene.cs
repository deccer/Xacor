using System;
using System.Collections.Generic;

namespace Xacor.Ecs;

//TODO(deccer) remove this when you use Arch
public class Scene
{
    private readonly List<int> _recycledIds = [];
    private readonly Dictionary<Type, IComponentPool> _pools = new();
    private int _nextEntityId = 0;

    public Entity CreateEntity()
    {
        var id = _recycledIds.Count > 0 
            ? _recycledIds[0] 
            : _nextEntityId++;
        
        if (_recycledIds.Count > 0)
        {
            _recycledIds.RemoveAt(0);
        }
        return new Entity(id);
    }

    public void DestroyEntity(Entity entity)
    {
        foreach (var pool in _pools.Values)
        {
            pool.Remove(entity.Id);
        }
        _recycledIds.Add(entity.Id);
    }

    public ref T Add<T>(Entity entity) where T : struct
    {
        var pool = GetPool<T>();
        return ref pool.Add(entity.Id);
    }

    public ref T Get<T>(Entity entity) where T : struct
    {
        return ref GetPool<T>().Get(entity.Id);
    }

    private ComponentPool<T> GetPool<T>() where T : struct
    {
        var type = typeof(T);
        
        // ReSharper disable once InvertIf
        if (!_pools.TryGetValue(type, out var pool))
        {
            pool = new ComponentPool<T>();
            _pools[type] = pool;
        }
        return (ComponentPool<T>)pool;
    }

    public IEnumerable<Entity> GetEntitiesWithComponents<T1, T2>() 
        where T1 : struct 
        where T2 : struct
    {
        // TODO(deccer) replace all the entity bs with Arch perhaps
        var p1 = GetPool<T1>();
        var p2 = GetPool<T2>();

        for (var i = 0; i < _nextEntityId; i++)
        {
            if (p1.Has(i) && p2.Has(i))
            {
                yield return new Entity(i);
            }
        }
    }
}