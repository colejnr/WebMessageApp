using Microsoft.AspNetCore.Mvc;
using WebChatApp.Data;
using WebChatApp.Models;
using WebChatApp.Utils;

namespace WebChatApp.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDBContext _context;
        public MessageController(ApplicationDBContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMessage(CreateMessage createMessage)
        {
            Message message = new Message();
            message.Content = EncryptionHelper.Encrypt(createMessage.Message, createMessage.SecretKey);
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return View(message);
        }


    }
}
