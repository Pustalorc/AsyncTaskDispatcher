using System;
using Rocket.API;

namespace Pustalorc.Plugins.AsynchronousTaskDispatcher.Plugin;

[Serializable]
public class AsyncTaskDispatcherPluginConfiguration : IRocketPluginConfiguration
{
    public ushort DelayOnEmptyQueue { get; set; }
    public ushort DelayOnItemNotReadyAndSolo { get; set; }
    public byte MaxQueueSystems { get; set; }

    public void LoadDefaults()
    {
        DelayOnEmptyQueue = 5000;
        DelayOnItemNotReadyAndSolo = 1000;
        MaxQueueSystems = 2;
    }
}