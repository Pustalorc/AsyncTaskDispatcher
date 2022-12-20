using System;
using System.Threading;
using System.Threading.Tasks;
using Pustalorc.Libraries.AsyncThreadingUtils.TaskQueue.QueueableTasks;
using Pustalorc.Plugins.AsynchronousTaskDispatcher.Plugin;

namespace Pustalorc.Plugins.AsynchronousTaskDispatcher.Dispatcher;

/// <summary>
/// The class to interact with the Asynchronous Task Dispatcher.
/// </summary>
public static class AsyncTaskDispatcher
{
    /// <summary>
    ///     Queues an anonymous task for execution.
    /// </summary>
    /// <param name="functionToExecute">The <see cref="T:System.Func`2" /> that will be executed.</param>
    /// <param name="delay">The delay (in milliseconds) before this task should be executed.</param>
    /// <param name="repeating">A boolean determining if this task should repeat after execution.</param>
    /// <returns>An <see cref="Action" /> representing the method to cancel the anonymous task from executing.</returns>
    public static Action QueueAnonymousTask(Func<CancellationToken, Task> functionToExecute, int delay = 0,
        bool repeating = false)
    {
        var pluginInstance = AsyncTaskDispatcherPlugin.Instance;
        if (pluginInstance == null)
            throw new PluginNotLoadedException();

        return pluginInstance.QueueAnonymousTask(functionToExecute, delay, repeating);
    }

    /// <summary>
    ///     Queues a <see cref="QueueableTask" /> for execution.
    /// </summary>
    /// <param name="task">The <see cref="QueueableTask" /> to queue.</param>
    public static void QueueTask(QueueableTask task)
    {
        var pluginInstance = AsyncTaskDispatcherPlugin.Instance;
        if (pluginInstance == null)
            throw new PluginNotLoadedException();

        pluginInstance.QueueTask(task);
    }
}