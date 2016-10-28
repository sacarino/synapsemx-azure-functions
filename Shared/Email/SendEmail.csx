#r "SendGrid";
#r "SendGrid.CSharp.HTTP.Client";

using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Configuration;
using System.Environment;
using System.Threading.Tasks;

public async static Task SendTemplateEmailAsync(string template, string to, string @from, Dictionary<string, string> data)
{
    var templates = new Dictionary<string,string>()
        .Add("InvitationToJoin","1c2871e7-411c-46a0-979b-970841d1e4ee")
        .Add("PasswordReset", "1c2871e7-411c-46a0-979b-970841d1e4ee")
    };

    // lookup the templateId based on the template name
    var templateId = templates[template];

    // create a sendgrid api client
    dynamic sendGrid = new SendGridAPIClient(GetEnvironmentVariable("SendGrid.ApiKey"));

    // create the from/to addresses
    var fromEmail = new Email(from);
    var toEmail = new Email(to);

    // fake some body content to make the api happy
    var content = new Content("text/html", "this has to contain something");

    // setup the mail message
    var mail = new Mail(fromEmail, string.Empty, toEmail, content) { TemplateId = templateId };

    // populate any tokens in the email body
    foreach (var tokenData in data)
    {
        mail.Personalization[0].AddSubstitution(tokenData.Key, tokenData.Value);
    }

    // send the email
    dynamic response = await sendGrid.client.mail.send.post(requestBody: mail.Get());

    if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
    {
        throw new ApplicationException($"SendGrid returned a status code of {response.StatusCode}");
    }
}