using System;

namespace Xacor.Graphics;

public interface ICommandExecutor
{
    void Execute(ReadOnlySpan<byte> commandBuffer);
}