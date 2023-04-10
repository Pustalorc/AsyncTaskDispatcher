using System;
using System.Collections.Generic;
using System.Linq;
using Pustalorc.Libraries.AsyncThreadingUtils.TaskQueue.Implementations;
using Pustalorc.Libraries.AsyncThreadingUtils.TaskQueue.QueueableTasks;
using Rocket.Core.Logging;

namespace Pustalorc.Plugins.AsynchronousTaskDispatcher.Plugin;

internal sealed class TaskQueueWithConfiguration : TaskQueue
{
    internal List<QueueableTask> ExposeQueue => Queue.ToList();

    public int QueueCount => Queue.Count;

    public TaskQueueWithConfiguration(AsyncTaskDispatcherPluginConfiguration configuration) : base(LogException,
        delayOnItemExecuted: configuration.DelayOnItemExecuted)
    {
        ReloadConfiguration(configuration);
    }

    public void ReloadConfiguration(AsyncTaskDispatcherPluginConfiguration configuration)
    {
        var delayOnEmptyQueue = configuration.DelayOnEmptyQueue;
        if (delayOnEmptyQueue == 0)
            delayOnEmptyQueue = 1;

        var delayOnQueueNotReady = configuration.DelayOnQueueNotReady;
        if (delayOnQueueNotReady == 0)
            delayOnQueueNotReady = 1;

        var delayOnItemNotReady = configuration.DelayOnItemNotReady;
        if (delayOnItemNotReady == 0)
            delayOnItemNotReady = 1;

        DelayOnEmptyQueue = delayOnEmptyQueue;
        DelayOnQueueNotReady = delayOnQueueNotReady;
        DelayOnItemNotReady = delayOnItemNotReady;
        DelayOnItemExecuted = configuration.DelayOnItemExecuted;
    }

    internal void Clear()
    {
        while (Queue.TryDequeue(out _))
        {
        }
    }

    private static void LogException(Exception exception)
    {
        Logger.LogException(exception, "Exception thrown on second thread.");
    }
}