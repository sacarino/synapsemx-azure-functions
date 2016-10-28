#r "Newtonsoft.Json"

using System;
using Newtonsoft.Json;

public static void Run(string message, TraceWriter log)
{
    dynamic @event = JsonConvert.DeserializeObject<dynamic>(message);
    log.Info($"C# Queue trigger function processed: {@event.EmailAddress}");
}