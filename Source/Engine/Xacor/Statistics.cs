namespace Xacor;

internal sealed class Statistics : IStatistics
{
    public int CommandBufferSizeInBytes { get; set; }
    
    public int CommandBufferCommandCount { get; set; }
}