﻿using Dapr.Client;
using TastyBeans.Shared.Application;

namespace TastyBeans.Shared.Infrastructure.Bindings;

public class EmailSender : IEmailSender
{
    private readonly DaprClient _daprClient;

    public EmailSender(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task SendEmailAsync(string emailAddress, string subject, string bodyHtml)
    {
        var metadata = new Dictionary<string, string>
        {
            { "emailTo", emailAddress },
            { "subject", subject },
            { "emailFrom", "noreply@recommend.coffee" }
        };

        await _daprClient.InvokeBindingAsync("smtp", "create", bodyHtml, metadata);
    }
}