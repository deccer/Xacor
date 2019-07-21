using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xacor.Collections;

namespace Xacor.Game.ECS
{
    public sealed class SystemManager
    {
        private readonly EntityWorld _entityWorld;

        private readonly IDictionary<Type, IList> _systems;

        private readonly SystemBitManager _systemBitManager;

        private readonly Bag<EntitySystem> _mergedBag;

        private IDictionary<int, SystemLayer> _updateLayers;

        private IDictionary<int, SystemLayer> _drawLayers;

        internal SystemManager(EntityWorld entityWorld)
        {
            _mergedBag = new Bag<EntitySystem>();
            _drawLayers = new SortedDictionary<int, SystemLayer>();
            _updateLayers = new SortedDictionary<int, SystemLayer>();

            _systemBitManager = new SystemBitManager();
            _systems = new Dictionary<Type, IList>();
            _entityWorld = entityWorld;
        }

        public Bag<EntitySystem> Systems => _mergedBag;

        public T SetSystem<T>(T system, GameLoopType gameLoopType, int layer = 0, ExecutionType executionType = ExecutionType.Synchronous) where T : EntitySystem
        {
            return (T)SetSystem(system.GetType(), system, gameLoopType, layer, executionType);
        }

        public List<T> GetSystems<T>() where T : EntitySystem
        {
            _systems.TryGetValue(typeof(T), out var system);

            return (List<T>)system;
        }

        public T GetSystem<T>() where T : EntitySystem
        {
            _systems.TryGetValue(typeof(T), out var systems);

            if (systems != null && systems.Count > 1)
            {
                throw new InvalidOperationException($"System list contains more than one element of type {typeof(T)}");
            }

            return (T)systems[0];
        }

        internal void InitializeAll(bool processAttributes, IEnumerable<Assembly> assembliesToScan = null)
        {
            if (processAttributes)
            {
                var types = assembliesToScan == null ? AttributesProcessor.Process(AttributesProcessor.SupportedAttributes) : AttributesProcessor.Process(AttributesProcessor.SupportedAttributes, assembliesToScan);

                foreach (var (type, value) in types)
                {
                    if (typeof(EntitySystem).IsAssignableFrom(type))
                    {
                        var entitySystemAttribute = (EntitySystemAttribute)value[0];
                        var instance = (EntitySystem)Activator.CreateInstance(type);
                        SetSystem(instance, entitySystemAttribute.GameLoopType, entitySystemAttribute.Layer, entitySystemAttribute.ExecutionType);
                    }
                    else if (typeof(IEntityTemplate).IsAssignableFrom(type))
                    {
                        var entityTemplateAttribute = (EntityTemplateAttribute)value[0];
                        var instance = (IEntityTemplate)Activator.CreateInstance(type);
                        _entityWorld.SetEntityTemplate(entityTemplateAttribute.Name, instance);
                    }
                    else if (typeof(ComponentPoolable).IsAssignableFrom(type))
                    {
                        CreatePool(type, value);
                    }
                }
            }

            for (int index = 0, j = _mergedBag.Count; index < j; ++index)
            {
                _mergedBag.Get(index).LoadContent();
            }
        }

        internal void TerminateAll()
        {
            for (var index = 0; index < Systems.Count; ++index)
            {
                var entitySystem = Systems.Get(index);
                entitySystem.UnloadContent();
            }

            Systems.Clear();
        }

        internal void Update()
        {
            Process(_updateLayers);
        }

        internal void Draw()
        {
            Process(_drawLayers);
        }

        private static void Process(IDictionary<int, SystemLayer> systemsToProcess)
        {
            foreach (int item in systemsToProcess.Keys)
            {
                if (systemsToProcess[item].Synchronous.Count > 0)
                {
                    ProcessBagSynchronous(systemsToProcess[item].Synchronous);
                }
                if (systemsToProcess[item].Asynchronous.Count > 0)
                {
                    ProcessBagAsynchronous(systemsToProcess[item].Asynchronous);
                }
            }
        }

        private static void SetSystem(ref IDictionary<int, SystemLayer> layers, EntitySystem system, int layer, ExecutionType executionType)
        {
            if (!layers.ContainsKey(layer))
            {
                layers[layer] = new SystemLayer();
            }

            var updateBag = layers[layer][executionType];

            if (!updateBag.Contains(system))
            {
                updateBag.Add(system);
            }
            //#if !FULLDOTNET
            //            layers = (from d in layers orderby d.Key ascending select d).ToDictionary(pair => pair.Key, pair => pair.Value);
            //#endif
        }

        private static ComponentPoolable CreateInstance(Type type)
        {
            return (ComponentPoolable)Activator.CreateInstance(type);
        }

        private static void ProcessBagSynchronous(Bag<EntitySystem> entitySystems)
        {
            for (int index = 0, j = entitySystems.Count; index < j; ++index)
            {
                entitySystems.Get(index).Process();
            }
        }

        private static void ProcessBagAsynchronous(IEnumerable<EntitySystem> entitySystems)
        {
            Parallel.ForEach(entitySystems, entitySystem => entitySystem.Process());
        }

        private void CreatePool(Type type, IEnumerable<Attribute> attributes)
        {
            ComponentPoolAttribute propertyComponentPool = null;

            foreach (var artemisComponentPool in attributes.OfType<ComponentPoolAttribute>())
            {
                propertyComponentPool = artemisComponentPool;
            }
            var methods = type.GetMethods();

            var methodInfos = from methodInfo in methods
                                                  let methodAttributes = methodInfo.GetCustomAttributes(false)
                                                  from attribute in methodAttributes.OfType<ComponentCreateAttribute>()
                                                  select methodInfo;

            Func<Type, ComponentPoolable> create = null;

            foreach (var methodInfo in methodInfos)
            {
                create = (Func<Type, ComponentPoolable>)Delegate.CreateDelegate(typeof(Func<Type, ComponentPoolable>), methodInfo);
            }

            if (create == null)
            {
                create = CreateInstance;
            }

            IComponentPool<ComponentPoolable> pool;

            if (propertyComponentPool == null)
            {
                throw new NullReferenceException("propertyComponentPool is null.");
            }

            if (!propertyComponentPool.IsSupportMultiThread)
            {
                pool = new ComponentPool<ComponentPoolable>(propertyComponentPool.InitialSize, propertyComponentPool.ResizeSize, propertyComponentPool.IsResizable, create, type);
            }
            else
            {
                pool = new ComponentPoolMultiThread<ComponentPoolable>(propertyComponentPool.InitialSize, propertyComponentPool.ResizeSize, propertyComponentPool.IsResizable, create, type);
            }

            _entityWorld.SetPool(type, pool);
        }

        private EntitySystem SetSystem(Type systemType, EntitySystem system, GameLoopType gameLoopType, int layer = 0, ExecutionType executionType = ExecutionType.Synchronous)
        {
            system.EntityWorld = _entityWorld;

            if (_systems.ContainsKey(systemType))
            {
                _systems[systemType].Add(system);
            }
            else
            {
                var genericType = typeof(List<>);
                var listType = genericType.MakeGenericType(systemType);
                _systems[systemType] = (IList)Activator.CreateInstance(listType);
                _systems[systemType].Add(system);
            }

            switch (gameLoopType)
            {
                case GameLoopType.Draw:
                    {
                        SetSystem(ref _drawLayers, system, layer, executionType);
                    }

                    break;
                case GameLoopType.Update:
                    {
                        SetSystem(ref _updateLayers, system, layer, executionType);
                    }

                    break;
            }

            if (!_mergedBag.Contains(system))
            {
                _mergedBag.Add(system);
            }

            system.Bit = _systemBitManager.GetBitFor(system);

            return system;
        }

        private sealed class SystemLayer
        {
            public readonly Bag<EntitySystem> Synchronous;

            public readonly Bag<EntitySystem> Asynchronous;

            public SystemLayer()
            {
                Asynchronous = new Bag<EntitySystem>();
                Synchronous = new Bag<EntitySystem>();
            }

            public Bag<EntitySystem> this[ExecutionType executionType]
            {
                get
                {
                    switch (executionType)
                    {
                        case ExecutionType.Synchronous:
                            return Synchronous;
                        case ExecutionType.Asynchronous:
                            return Asynchronous;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(executionType));
                    }
                }
            }
        }
    }
}