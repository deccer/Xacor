using System;

namespace Xacor.Ecs;

//TODO(deccer) remove this, when you use Arch
internal class ComponentPool<T> : IComponentPool
{
    private int[] _sparse = new int[1024]; // Map: EntityID -> Index
    private T[] _dense = new T[1024]; // Map: Index -> Component
    private int[] _denseToEntity = new int[1024]; // Map: Index -> EntityID
    private int _count;

    public ComponentPool()
    {
        Array.Fill(_sparse, -1);
    }

    public ref T Add(int entityId)
    {
        if (Has(entityId))
        {
            return ref _dense[_sparse[entityId]];
        }

        if (entityId >= _sparse.Length)
        {
            Array.Resize(ref _sparse, Math.Max(entityId + 1, _sparse.Length * 2));
        }
        
        if (_count >= _dense.Length)
        {
            Array.Resize(ref _dense, _dense.Length * 2);
            Array.Resize(ref _denseToEntity, _denseToEntity.Length * 2);
        }

        var index = _count;
        _dense[index] = default!;
        _denseToEntity[index] = entityId;
        
        _sparse[entityId] = index;
        _count++;

        return ref _dense[index];
    }

    public ref T Get(int entityId)
    {
        return ref _dense[_sparse[entityId]];
    }

    public bool Has(int entityId)
    {
        return entityId < _sparse.Length && _sparse[entityId] != -1;
    }

    public void Remove(int entityId)
    {
        if (!Has(entityId)) {
            return;
        }

        var indexToRemove = _sparse[entityId];
        var lastIndex = _count - 1;

        var lastComponent = _dense[lastIndex];
        var lastEntity = _denseToEntity[lastIndex];

        _dense[indexToRemove] = lastComponent;
        _denseToEntity[indexToRemove] = lastEntity;
        _sparse[lastEntity] = indexToRemove;
        _sparse[entityId] = -1;
        _count--;
    }
}