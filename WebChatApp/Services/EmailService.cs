using System;

public class Class1
{
	public class EmailService
	{
		private readonly SmtpSettings _smtpSettings;
	public EmailService(IOptions<SmtpSettings> smtpSettings)
			_smtpSettings = smtpSettings.Value;
	
	}
	public async Task SendEmaiAsync(string recipientEmail, string message)
	{
		var emailMessage = new MimeMessage();
		emailMessage.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
		emailMessage.To.Add(new MailboxAddress(recipientEmail, recipientEmail));
		emailMessage.Subject = subject;
		emailMessage.Body = new TextPart("plain")
		{
			Text = message
		};

	}
}
