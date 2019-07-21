using System;
using System.Collections.Generic;

namespace Xacor.Game.ECS
{
    public class Trigger
    {
        public Trigger(params string[] propertyName)
        {
            IsFired = false;
            WorldPropertiesMonitored = new List<string>();
            foreach (var item in propertyName)
            {
                WorldPropertiesMonitored.Add(item);
            }
        }

        public event Action<Trigger> OnFire;

        public BlackBoard BlackBoard { get; internal set; }

        public TriggerStateType TriggerStateType { get; private set; }

        public List<string> WorldPropertiesMonitored { get; protected set; }

        internal bool IsFired { get; set; }

        public void RemoveThisTrigger()
        {
            BlackBoard.RemoveTrigger(this);
        }

        internal void Fire(TriggerStateType triggerStateType)
        {
            IsFired = true;
            TriggerStateType = triggerStateType;
            if (CheckConditionToFire())
            {
                CalledOnFire(triggerStateType);
                OnFire?.Invoke(this);
            }

            IsFired = false;
        }

        protected virtual void CalledOnFire(TriggerStateType triggerStateType)
        {
        }

        protected virtual bool CheckConditionToFire()
        {
            return true;
        }
    }
}