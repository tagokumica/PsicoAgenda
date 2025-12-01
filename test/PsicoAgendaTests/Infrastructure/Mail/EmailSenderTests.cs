using System.Net;
using Infrastructure.Mail;
using Infrastructure.Mail.Options;
using Microsoft.Extensions.Options;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PsicoAgendaTests.Infrastructure.Mail;

public class EmailSenderTests
{
    private readonly EmailProviderOptions _options;
    private readonly IOptions<EmailProviderOptions> _optionsMock;
    private readonly Mock<ISendGridClient> _sendMock;
    private readonly EmailSender _emailSender;

    public EmailSenderTests()
    {
        _options = new EmailProviderOptions
        {
            ApiKey = "fake-api-key",
            FromEmail = "noreply@psicoagenda.com",
            FromName = "PsicoAgenda"
        };

        _optionsMock = Options.Create(_options);
        _sendMock = new Mock<ISendGridClient>();
        _emailSender = new EmailSender(_optionsMock, _sendMock.Object);
    }

    [Fact(DisplayName = "Construtor deve inicializar corretamente as opções")]
    public void Constructor_SetsOptions_Correctly()
    {
        // Act
        var sender = new EmailSender(_optionsMock);

        // Assert
        Assert.NotNull(sender);
    }

    [Fact(DisplayName = "SendAsync deve enviar e-mail com os parâmetros corretos")]
    public async Task SendAsync_CreatesAndSendsEmail_Correctly()
    {
        // Arrange
        var to = "user@example.com";
        var subject = "Teste Unitário";
        var body = "Corpo do e-mail";

        SendGridMessage? capturedMsg = null;

        _sendMock
            .Setup(c => c.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()))
            .Callback<SendGridMessage, CancellationToken>((msg, _) => capturedMsg = msg)
            .ReturnsAsync(new Response(HttpStatusCode.Accepted, null, null));

        // Act
        await _emailSender.SendAsync(to, subject, body, CancellationToken.None);

        // Assert
        _sendMock.Verify(c =>
            c.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()),
            Times.Once);

        Assert.NotNull(capturedMsg);
        Assert.Equal(_options.FromEmail, capturedMsg!.From.Email);
        Assert.Equal(_options.FromName, capturedMsg.From.Name);

        // Serializa o JSON enviado (forma mais confiável de validar dados)
        var json = capturedMsg.Serialize();

        Assert.Contains(subject, json);
        Assert.Contains(to, json);
        Assert.Contains(body, json);
        Assert.Contains(_options.FromEmail, json);
    }

    [Fact(DisplayName = "SendAsync deve lidar corretamente com falhas de envio (BadRequest)")]
    public async Task SendAsync_HandlesBadRequest_Gracefully()
    {
        // Arrange
        var to = "invalid@example.com";
        var subject = "Teste falho";
        var body = "Conteúdo inválido";

        _sendMock
            .Setup(c => c.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response(HttpStatusCode.BadRequest, null, null));

        // Act
        var exception = await Record.ExceptionAsync(() =>
            _emailSender.SendAsync(to, subject, body, CancellationToken.None));

        // Assert
        _sendMock.Verify(c =>
            c.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()),
            Times.Once);

        Assert.Null(exception);
    }
}
