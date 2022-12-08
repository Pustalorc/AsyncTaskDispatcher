using System;

namespace Pustalorc.Plugins.AsynchronousTaskDispatcher.Dispatcher;

public sealed class PluginNotLoadedException : Exception
{
    public PluginNotLoadedException() : base(
        "The plugin AsyncTaskDispatcher is not loaded! Please make sure the plugin is loaded to use this feature.")
    {
    }
}