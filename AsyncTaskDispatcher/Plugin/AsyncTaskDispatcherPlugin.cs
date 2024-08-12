using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pustalorc.Libraries.AsyncThreadingUtils.TaskQueue.QueueableTasks;
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
        var tasksToRequeue = new List<QueueableTask>();
        if (currentNumberOfSystems < maxQueueSystems)
            for (var i = currentNumberOfSystems - 1; i < maxQueueSystems; i++)
                TaskQueueSystems.Add(new TaskQueueWithConfiguration(config));
        else if (currentNumberOfSystems > maxQueueSystems)
            for (var i = currentNumberOfSystems - 1; i >= maxQueueSystems; i--)
            {
                var queueSystem = TaskQueueSystems[i];
                tasksToRequeue.AddRange(queueSystem.ExposeQueue);
                queueSystem.Clear();
                TaskQueueSystems.RemoveAt(i);
            }

        foreach (var taskToRequeue in tasksToRequeue)
            QueueTask(taskToRequeue);

        foreach (var system in TaskQueueSystems)
        {
            system.ReloadConfiguration(config);
            system.Start();
        }

        Logger.Log("Async TaskDispatcher v1.2.1, with Utils Library v1.1.0, by Pustalorc has been loaded!");
    }

    protected override void Unload()
    {
        foreach (var system in TaskQueueSystems)
            system.Stop();

        Logger.Log("Async TaskDispatcher v1.2.1, with Utils Library v1.1.0, by Pustalorc has been unloaded!");
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