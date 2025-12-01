namespace Infrastructure.Mail.Options;

public class EmailProviderOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = "PsicoAgenda";
}