using System;

public class Class1
{
	public class EmailService
	{
		private readonly SmtpSettings _smtSettings;
	public EmailService(IOptions<SmtpSettings> smtSettings)
			_smtpSettings = smtSettings.Value;
	
	}
}
