namespace HR.LeaveManagement.Application.Models.Email;

public record EmailMessage(string To, string Subject, string Body);