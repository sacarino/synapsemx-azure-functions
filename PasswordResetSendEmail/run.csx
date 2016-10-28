#r "Newtonsoft.Json"
#load "..\Shared\Email\SendEmail.csx"

using System;
using Newtonsoft.Json;

public async static Task Run(string message, TraceWriter log)
{
    dynamic @event = JsonConvert.DeserializeObject<dynamic>(message);
    log.Info($"C# Queue trigger function processed: {@event.EmailAddress}");

    // extract the data from the event that goes in the email template
    var data = new Dictionary<string,string>{
        "token" => @event.Token
    };

    // send the email
    await SendTemplateEmailAsync("PasswordReset", @event.EmailAddress, "wes@synapsemx.com", data);
}