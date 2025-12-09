namespace Xacor;

public interface IStatistics
{
    public int CommandBufferSizeInBytes { get; }
    
    public int CommandBufferCommandCount { get; }
}