namespace Xacor.Game.ECS
{
    public interface IComponentPool<T>
        where T : ComponentPoolable
    {
        void CleanUp();

        T New();

        void ReturnObject(T component);
    }
}