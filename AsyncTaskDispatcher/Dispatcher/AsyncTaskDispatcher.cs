using System;
using System.Threading;
using System.Threading.Tasks;
using Pustalorc.Libraries.AsyncThreadingUtils.TaskQueue.QueueableTasks;
using Pustalorc.Plugins.AsynchronousTaskDispatcher.Plugin;

namespace Pustalorc.Plugins.AsynchronousTaskDispatcher.Dispatcher;

public static class AsyncTaskDispatcher
{
    public static Action QueueAnonymousTask(Func<CancellationToken, Task> functionToExecute, int delay = 0,
        bool repeating = false)
    {
        var pluginInstance = AsyncTaskDispatcherPlugin.Instance;
        if (pluginInstance == null)
            throw new PluginNotLoadedException();

        return pluginInstance.QueueAnonymousTask(functionToExecute, delay, repeating);
    }

    public static void QueueTask(QueueableTask task)
    {
        var pluginInstance = AsyncTaskDispatcherPlugin.Instance;
        if (pluginInstance == null)
            throw new PluginNotLoadedException();

        pluginInstance.QueueTask(task);
    }
}