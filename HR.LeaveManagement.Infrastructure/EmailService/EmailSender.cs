using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;

namespace HR.LeaveManagement.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    
    public Task<bool> SendEmailAsync(EmailMessage email)
    {
        Console.WriteLine($"Sending email");
        Console.WriteLine($"FromAddress: {_emailSettings.FromAddress}");
        Console.WriteLine($"FromName: {_emailSettings.FromAddress}");
        Console.WriteLine($"ApiKey: {_emailSettings.ApiKey}");
        
        Console.WriteLine($"Email Subject: {email.Subject}");
        Console.WriteLine($"Email To: {email.To}");
        Console.WriteLine($"Email Body: {email.Body}");
        Console.WriteLine($"Sent email");
        
        return Task.FromResult(true);
    }
}