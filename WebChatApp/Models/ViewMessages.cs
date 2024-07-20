namespace WebChatApp.Models
{
    public class ViewMessage
    {
        public string Id { get; set; }
        public string EncryptedContent { get; set; }
        public string DecryptedContent { get; set; }
        public DateTime TimeCreated { get; set; }

        public ViewMessage()
        {
            Id = Guid.NewGuid().ToString();
            EncryptedContent = string.Empty;
            DecryptedContent = string.Empty;
            TimeCreated = DateTime.Now;
        }

    }
}