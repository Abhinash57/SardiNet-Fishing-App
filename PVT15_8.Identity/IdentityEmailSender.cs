using Microsoft.AspNetCore.Identity;
using PVT15_8.Identity.Data.Models;
using System.Text.Json.Serialization;

namespace PVT15_8.Identity;

public class IdentityEmailSender(HttpClient http, IConfiguration config) : IEmailSender<User>
{
    private readonly string _fromEmail = "noreply@monimon.org";
    private readonly string _fromName = "SardiNET";

    public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        var gatewayLink = RewriteUrlToGateway(confirmationLink);

        string subject = "Confirm your email address";
        string htmlMessage = $"Please confirm your account by <a href='{gatewayLink}'>clicking here</a>.";
        
        await SendEmailAsync(email, subject, htmlMessage);
    }

    public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        string subject = "Your password reset code";
        string htmlMessage = $"Please reset your password using the following code: <strong>{resetCode}</strong>";
        
        await SendEmailAsync(email, subject, htmlMessage);
    }

    public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        string subject = "Reset your password";
        string htmlMessage = $"Please reset your password by <a href='{resetLink}'>clicking here</a>.";
        
        await SendEmailAsync(email, subject, htmlMessage);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {
        var payload = new MailtrapSendRequest(
            To: [new EmailContact(toEmail, toEmail)],
            From: new EmailContact(_fromEmail, _fromName),
            Subject: subject,
            Html: htmlMessage
        );

        if (config["Mailtrap:ApiKey"] != "404")
        {
            var response = await http.PostAsJsonAsync("api/send", payload);
            response.EnsureSuccessStatusCode();
        }
    }

    private string RewriteUrlToGateway(string url)
    {
        var gatewayUri = new Uri(config["GatewayUrl"] ?? throw new InvalidOperationException("Gatewayurl not in config"));

        var uriBuilder = new UriBuilder(url)
        {
            Host = gatewayUri.Host,
            Port = gatewayUri.Port,
            Scheme = gatewayUri.Scheme,
        };
        
        uriBuilder.Path = "/identity" + uriBuilder.Path;
        return uriBuilder.Uri.ToString();
    }
}
public record EmailContact(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("name")] string Name
);

public record MailtrapSendRequest(
    [property: JsonPropertyName("to")] EmailContact[] To,
    [property: JsonPropertyName("from")] EmailContact From,
    [property: JsonPropertyName("subject")] string Subject,
    [property: JsonPropertyName("html")] string Html
);
