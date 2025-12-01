using Infrastructure.Mail.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Mail;

public class EmailSender : IEmailSender
{
    private readonly EmailProviderOptions _options;
    private readonly ISendGridClient _client;

    public EmailSender(IOptions<EmailProviderOptions> options, ISendGridClient? client = null)
    {
        _options = options.Value;
        _client = client ?? new SendGridClient(_options.ApiKey);
    }
    public async Task SendAsync(string to, string subject, string body, CancellationToken ct = default)
    {
        var msg = MailHelper.CreateSingleEmail(
            new EmailAddress(_options.FromEmail, _options.FromName),
            new EmailAddress(to), subject, body, body);

        await _client.SendEmailAsync(msg, ct);
    }
}