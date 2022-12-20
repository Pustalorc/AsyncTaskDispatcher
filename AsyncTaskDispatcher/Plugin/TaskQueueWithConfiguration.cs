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

    public TaskQueueWithConfiguration(AsyncTaskDispatcherPluginConfiguration configuration) : base(LogException)
    {
        ReloadConfiguration(configuration);
    }

    public void ReloadConfiguration(AsyncTaskDispatcherPluginConfiguration configuration)
    {
        var delayOnEmpty = configuration.DelayOnEmptyQueue;
        if (delayOnEmpty == 0)
            delayOnEmpty = 1;

        var delayOnSolo = configuration.DelayOnItemNotReadyAndSolo;
        if (delayOnSolo == 0)
            delayOnSolo = 1;

        DelayOnEmptyQueue = delayOnEmpty;
        DelayOnItemNotReadyAndSolo = delayOnSolo;
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