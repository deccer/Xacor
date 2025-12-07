using System;
using System.Collections.Generic;

namespace Xacor.Graphics;

//TODO(deccer) plug this into RenderFrame
public class ResourceDeletionQueue
{
    private readonly List<uint>[] _deletionQueues;
    private readonly int _framesInFlight;

    public ResourceDeletionQueue(int framesInFlight)
    {
        _framesInFlight = framesInFlight;
        _deletionQueues = new List<uint>[framesInFlight];
        for (var i = 0; i < framesInFlight; i++)
        {
            _deletionQueues[i] = [];
        }
    }

    public void EnqueueDeletion(
        uint resourceHandle, 
        int currentFrameIndex)
    {
        _deletionQueues[currentFrameIndex % _framesInFlight].Add(resourceHandle);
    }

    public void ProcessDeletions(
        int frameIndex, 
        Action<uint> deleteDelegate)
    {
        var queueIndex = frameIndex % _framesInFlight;
        var queue = _deletionQueues[queueIndex];

        foreach (var handle in queue)
        {
            deleteDelegate(handle);
        }
        
        queue.Clear();
    }
}