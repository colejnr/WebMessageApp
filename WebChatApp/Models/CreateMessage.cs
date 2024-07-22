using System.ComponentModel.DataAnnotations;
namespace WebChatApp.Models 
{ 

    public class CreateMessage
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string SecretKey { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public CreateMessage() 
        {
            Message = string.Empty;
            SecretKey = string.Empty;
            Email = string.Empty;
        }

    }
}
