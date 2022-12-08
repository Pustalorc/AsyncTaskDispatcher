using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pustalorc.Libraries.AsyncThreadingUtils.TaskQueue.QueueableTasks;
using Pustalorc.Plugins.AsynchronousTaskDispatcher.TaskQueueImplementation;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;

namespace Pustalorc.Plugins.AsynchronousTaskDispatcher.Plugin;

internal sealed class AsyncTaskDispatcherPlugin : RocketPlugin<AsyncTaskDispatcherPluginConfiguration>
{
    internal static AsyncTaskDispatcherPlugin? Instance { get; private set; }

    private List<TaskQueueWithConfiguration> TaskQueueSystems { get; }

    internal AsyncTaskDispatcherPlugin()
    {
        var config = Configuration.Instance;
        var maxQueueSystems = config.MaxQueueSystems;
        if (maxQueueSystems == 0)
            maxQueueSystems = 1;

        TaskQueueSystems = new List<TaskQueueWithConfiguration>(config.MaxQueueSystems);

        for (var i = 0; i < maxQueueSystems; i++)
            TaskQueueSystems.Add(new TaskQueueWithConfiguration(config));

        Instance = this;
    }

    protected override void Load()
    {
        var config = Configuration.Instance;
        var maxQueueSystems = config.MaxQueueSystems;
        if (maxQueueSystems == 0)
            maxQueueSystems = 1;

        var currentNumberOfSystems = TaskQueueSystems.Count;
        if (currentNumberOfSystems < maxQueueSystems)
            for (var i = 0; i < maxQueueSystems - currentNumberOfSystems; i++)
                TaskQueueSystems.Add(new TaskQueueWithConfiguration(config));
        else if (currentNumberOfSystems > maxQueueSystems)
            for (var i = 0; i < currentNumberOfSystems - maxQueueSystems; i++)
                TaskQueueSystems.RemoveAt(TaskQueueSystems.Count - 1);

        foreach (var system in TaskQueueSystems)
        {
            system.ReloadConfiguration(config);
            system.Start();
        }

        Logger.Log("Async TaskDispatcher by Pustalorc has been loaded!");
    }

    protected override void Unload()
    {
        foreach (var system in TaskQueueSystems)
            system.Stop();

        Logger.Log("Async TaskDispatcher by Pustalorc has been unloaded!");
    }

    private TaskQueueWithConfiguration GetEmptiestSystem()
    {
        return TaskQueueSystems.OrderBy(k => k.QueueCount).First();
    }

    internal Action QueueAnonymousTask(Func<CancellationToken, Task> functionToExecute, int delay = 0,
        bool repeating = false)
    {
        return GetEmptiestSystem().QueueAnonymousTask(functionToExecute, delay, repeating);
    }

    internal void QueueTask(QueueableTask task)
    {
        GetEmptiestSystem().QueueTask(task);
    }
}