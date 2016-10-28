public static void Run(string passwordResetEvent, TraceWriter log)
{
    log.Info($"C# Blob trigger function processed: {passwordResetEvent}");
}