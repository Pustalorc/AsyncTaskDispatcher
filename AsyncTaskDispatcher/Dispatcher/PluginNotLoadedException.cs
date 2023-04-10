using System;

namespace Pustalorc.Plugins.AsynchronousTaskDispatcher.Dispatcher;

/// <inheritdoc />
/// <summary>
///     An exception signifying that the AsyncTaskDispatcher plugin is not currently loaded.
/// </summary>
public sealed class PluginNotLoadedException : Exception
{
    /// <inheritdoc />
    /// <summary>
    ///     Creates a new instance of the exception with the default message.
    /// </summary>
    public PluginNotLoadedException() : base(
        "The plugin AsyncTaskDispatcher is not loaded! Please make sure the plugin is loaded to use this feature.")
    {
    }
}