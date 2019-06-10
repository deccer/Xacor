namespace Xacor.Graphics
{
    public abstract class ConstantBuffer<T>
    {
        public abstract void UpdateBuffer(T constants);
    }
}