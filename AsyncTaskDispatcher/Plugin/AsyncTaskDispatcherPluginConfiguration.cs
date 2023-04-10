using System;
using Rocket.API;

namespace Pustalorc.Plugins.AsynchronousTaskDispatcher.Plugin;

/// <summary>
///     The configuration class for the plugin
/// </summary>
[Serializable]
public class AsyncTaskDispatcherPluginConfiguration : IRocketPluginConfiguration
{
    /// <summary>
    ///     The amount of time (in ms) to delay the queue from processing another element if the queue is empty.
    /// </summary>
    public ushort DelayOnEmptyQueue { get; set; }

    /// <summary>
    ///     The amount of time (in ms) to delay the queue from processing another element if the entire queue is not ready for
    ///     execution.
    /// </summary>
    public ushort DelayOnQueueNotReady { get; set; }

    /// <summary>
    ///     The amount of time (in ms) to delay the queue from processing another element if the last checked item was not
    ///     ready for execution.
    /// </summary>
    public ushort DelayOnItemNotReady { get; set; }

    /// <summary>
    ///     The amount of time (in ms) to delay the queue from processing another element after executing any element.
    /// </summary>
    public ushort DelayOnItemExecuted { get; set; }

    /// <summary>
    ///     The maximum number of systems that will be available to queue tasks
    /// </summary>
    public byte MaxQueueSystems { get; set; }

    /// <summary>
    ///     Constructs the configuration with some default values that do not interfere in deserialization.
    ///     Helpful to solve issues when config changes.
    /// </summary>
    public AsyncTaskDispatcherPluginConfiguration()
    {
        DelayOnEmptyQueue = 5000;
        DelayOnQueueNotReady = 1000;
        DelayOnItemNotReady = 25;
        DelayOnItemExecuted = 1;
        MaxQueueSystems = 2;
    }

    /// <summary>
    ///     Loads the default config values for the plugin.
    /// </summary>
    public void LoadDefaults()
    {
        DelayOnEmptyQueue = 5000;
        DelayOnQueueNotReady = 1000;
        DelayOnItemNotReady = 25;
        DelayOnItemExecuted = 1;
        MaxQueueSystems = 2;
    }
}