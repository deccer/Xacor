using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace Xacor.Graphics;

internal abstract class GraphicsDevice
{
    protected struct Submission
    {
        public IntPtr BufferStart; 
        public int Length;
        public Action<IntPtr> OnComplete; 
    }
    
    protected readonly ConcurrentQueue<Submission> SubmissionQueue = new();

    public unsafe void Submit(CommandRecorder recorder)
    {
        var detachedBuffer = recorder.DetachBuffer(); 
        
        SubmissionQueue.Enqueue(new Submission 
        { 
            BufferStart = (IntPtr)detachedBuffer.Data, 
            Length = detachedBuffer.Length,
            OnComplete = (p) => 
            {
                NativeMemory.Free((void*)p);
            }
        });
        
        recorder.Reset(allocateFresh: true);
    }
}