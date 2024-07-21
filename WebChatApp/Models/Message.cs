namespace WebChatApp.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeCreated { get; set; }

        public Message() 
        {
            Id = Guid.NewGuid().ToString();
            Content = string.Empty;
            TimeCreated = DateTime.Now;
        }
    }
}
