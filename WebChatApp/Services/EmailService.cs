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

	}
}
