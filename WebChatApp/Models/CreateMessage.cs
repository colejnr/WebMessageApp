using System.ComponentModel.DataAnnotations;
namespace WebChatApp.Models 
{ 

    public class CreateMessage
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string SecretKey { get; set; }

        public CreateMessage() 
        {
            Message = string.Empty;
            SecretKey = string.Empty;
        }

    }
}
