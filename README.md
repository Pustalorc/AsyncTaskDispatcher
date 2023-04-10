# AsyncTaskDispatcher

Unturned Plugin to allow developers to queue asynchronous tasks on a separate thread like rocketmod's TaskDispatcher for queueing actions in the main thread, whilst allowing users to customize how the queue is processed.
Download is available on github [releases](https://github.com/Pustalorc/AsyncTaskDispatcher/releases/)

## Default Configuration:

```xml
<?xml version="1.0" encoding="utf-8"?>
<AsyncTaskDispatcherPluginConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <DelayOnEmptyQueue>5000</DelayOnEmptyQueue> <!-- The amount of time, in milliseconds, to delay a queue system if its queue is empty. Max value of 65535. -->
  <DelayOnQueueNotReady>1000</DelayOnQueueNotReady> <!-- The amount of time, in milliseconds, to delay a queue system if the entire queue is not ready for execution. Max value of 65535. -->
  <DelayOnItemNotReady>25</DelayOnItemNotReady> <!-- The amount of time, in milliseconds, to delay a queue system if the last executed task was not ready for execution. Max value of 65535. -->
  <DelayOnItemExecuted>1</DelayOnItemExecuted> <!-- The amount of time, in milliseconds, to delay a queue system after a task gets executed. Max value of 65535.  -->
  <MaxQueueSystems>2</MaxQueueSystems> <!-- The number of queue systems that will be started and running in the background. More will increase CPU usage, but increase throughput of task execution. -->
</AsyncTaskDispatcherPluginConfiguration>
```