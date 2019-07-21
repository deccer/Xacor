using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xacor.Game.ECS
{
    public class BlackBoard
    {
        private readonly object _entryLock;

        private readonly Dictionary<string, object> _intelligence;

        private readonly Dictionary<string, List<Trigger>> _triggers;

        public BlackBoard()
        {
            _triggers = new Dictionary<string, List<Trigger>>();
            _intelligence = new Dictionary<string, object>();
            _entryLock = new object();
        }

        public void AddTrigger(Trigger trigger, bool evaluateNow = false)
        {
            lock (_entryLock)
            {
                trigger.BlackBoard = this;
                foreach (var intelName in trigger.WorldPropertiesMonitored)
                {
                    if (_triggers.ContainsKey(intelName))
                    {
                        _triggers[intelName].Add(trigger);
                    }
                    else
                    {
                        _triggers[intelName] = new List<Trigger>
                                                       {
                                                           trigger
                                                       };
                    }
                }

                if (evaluateNow)
                {
                    if (trigger.IsFired == false)
                    {
                        trigger.Fire(TriggerStateType.TriggerAdded);
                    }
                }
            }
        }

        public void AtomicOperateOnEntry(Action<BlackBoard> operation)
        {
            lock (_entryLock)
            {
                operation(this);
            }
        }

        public T GetEntry<T>(string name)
        {
            var ret = GetEntry(name);
            return ret == null ? default(T) : (T)ret;
        }

        public object GetEntry(string name)
        {
            _intelligence.TryGetValue(name, out var ret);
            return ret;
        }

        public void RemoveEntry(string name)
        {
            lock (_entryLock)
            {
                _intelligence.Remove(name);

                if (_triggers.ContainsKey(name))
                {
                    foreach (var item in _triggers[name].Where(item => item.IsFired == false))
                    {
                        item.Fire(TriggerStateType.ValueRemoved);
                    }
                }
            }
        }

        public void RemoveTrigger(Trigger trigger)
        {
            lock (_entryLock)
            {
                foreach (var intelName in trigger.WorldPropertiesMonitored)
                {
                    _triggers[intelName].Remove(trigger);
                }
            }
        }

        public void SetEntry<T>(string name, T intel)
        {
            lock (_entryLock)
            {
                var triggerStateType = _intelligence.ContainsKey(name) ? TriggerStateType.ValueChanged : TriggerStateType.ValueAdded;
                _intelligence[name] = intel;

                if (_triggers.ContainsKey(name))
                {
                    foreach (var item in _triggers[name].Where(item => item.IsFired == false))
                    {
                        item.Fire(triggerStateType);
                    }
                }
            }
        }

        public List<Trigger> TriggerList(string name)
        {
            return _triggers[name];
        }
    }
}
