namespace Xacor.Ecs;

//TODO(deccer) remove this when you use Arch
internal interface IComponentPool 
{
    bool Has(int entityId);
    
    void Remove(int entityId);
}