namespace Xacor.Game.ECS
{
    public interface IEntityTemplate
    {
        Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args);
    }
}