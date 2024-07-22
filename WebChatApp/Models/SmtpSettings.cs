using System;
using Org.BouncyCastle.Tls;

namespace WebChatApp.Models
{
    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public SmtpSettings()
        {
            Server = string.Empty;
            SenderName = string.Empty;
            SenderEmail = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
        }
    }

}