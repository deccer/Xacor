using System;

namespace Xacor.Game.ECS
{
    public sealed class TriggerMultiCondition : Trigger
    {
        private readonly Func<BlackBoard, TriggerStateType, bool> _condition;

        private readonly Action<TriggerStateType> _onFire;

        public TriggerMultiCondition(Func<BlackBoard, TriggerStateType, bool> condition, Action<TriggerStateType> onFire = null, params string[] names)
        {
            WorldPropertiesMonitored.AddRange(names);
            _condition = condition;
            _onFire = onFire;
        }

        public new void RemoveThisTrigger()
        {
            BlackBoard.RemoveTrigger(this);
        }

        protected override void CalledOnFire(TriggerStateType triggerStateType)
        {
            _onFire?.Invoke(triggerStateType);
        }

        protected override bool CheckConditionToFire()
        {
            return _condition(BlackBoard, TriggerStateType);
        }
    }
}