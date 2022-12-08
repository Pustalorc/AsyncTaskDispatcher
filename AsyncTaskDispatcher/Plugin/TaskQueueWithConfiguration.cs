using System;
using Pustalorc.Libraries.AsyncThreadingUtils.TaskQueue.Implementations;
using Pustalorc.Plugins.AsynchronousTaskDispatcher.Plugin;
using Rocket.Core.Logging;

namespace Pustalorc.Plugins.AsynchronousTaskDispatcher.TaskQueueImplementation;

internal sealed class TaskQueueWithConfiguration : TaskQueue
{
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

    private static void LogException(Exception exception)
    {
        Logger.LogException(exception, "Exception thrown on second thread.");
    }
}