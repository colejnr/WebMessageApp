using System.ComponentModel.DataAnnotations;

namespace WebChatApp.Models
{
    public class DecryptMessage
    {
        [Required]
        public string SecretKey { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }
}
