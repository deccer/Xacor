using System.Collections.Generic;

namespace Xacor.Game.ECS
{
    public abstract class EntityProcessingSystem : EntitySystem
    {
        protected EntityProcessingSystem(Aspect aspect)
            : base(aspect)
        {
        }

        public abstract void Process(Entity entity);

        protected override void ProcessEntities(IDictionary<int, Entity> entities)
        {
            foreach (Entity entity in entities.Values)
            {
                this.Process(entity);
            }
        }
    }
}