using System;
using Rocket.API;

namespace Pustalorc.Plugins.AsynchronousTaskDispatcher.Plugin;

/// <summary>
/// The configuration class for the plugin
/// </summary>
[Serializable]
public class AsyncTaskDispatcherPluginConfiguration : IRocketPluginConfiguration
{
    /// <summary>
    /// The amount of time (in ms) to delay the queue from processing another element if the queue is empty.
    /// </summary>
    public ushort DelayOnEmptyQueue { get; set; }

    /// <summary>
    /// The amount of time (in ms) to delay the queue from processing another element if the queue only has one item left but it's not ready for execution.
    /// </summary>
    public ushort DelayOnItemNotReadyAndSolo { get; set; }

    /// <summary>
    /// The maximum number of systems that will be available to queue tasks
    /// </summary>
    public byte MaxQueueSystems { get; set; }

    /// <summary>
    /// Loads the default config values for the plugin.
    /// </summary>
    public void LoadDefaults()
    {
        DelayOnEmptyQueue = 5000;
        DelayOnItemNotReadyAndSolo = 1000;
        MaxQueueSystems = 2;
    }
}