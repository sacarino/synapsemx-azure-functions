public static void Run(dynamic passwordResetEvent, TraceWriter log)
{
    log.Info($"C# Blob trigger function processed: {passwordResetEvent}");
}