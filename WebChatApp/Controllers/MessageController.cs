using Microsoft.AspNetCore.Mvc;
using WebChatApp.Data;
using WebChatApp.Models;
using WebChatApp.Services;
using WebChatApp.Utils;

namespace WebChatApp.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly EmailService _emailService;
        public MessageController(ApplicationDBContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMessage(CreateMessage model, CreateMessage createMessage)
        {
            if (ModelState.IsValid)
            {
                var message = new Message
                {
                    Content = model.Message,
                    TimeCreated = DateTime.Now
                };
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                // Send email with secret key
                var emailSubject = "Your Secret Key";
                var emailMessage = $"Your secret key is: {model.SecretKey}, Please do not share";
                await _emailService.SendEmailAsync(model.Email, emailSubject, emailMessage);
                Message messages = new Message();
                messages.Content = EncryptionHelper.Encrypt(createMessage.Message, createMessage.SecretKey);
                await _context.Messages.AddAsync(messages);
                await _context.SaveChangesAsync();
                return View(messages);
            }
            return View(model);
        }
        public async Task<IActionResult> ViewMessage(string id, DecryptMessage decryptMessage)
        {
            Message? message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                throw new Exception("Message not found");

            }
            ViewMessage viewMessage = new ViewMessage();
            viewMessage.Id = message.Id;
            try
            {
                viewMessage.EncryptedContent = message.Content;
                viewMessage.DecryptedContent = EncryptionHelper.Decrypt(message.Content, decryptMessage.SecretKey);
                viewMessage.TimeCreated = message.TimeCreated;
                return View(viewMessage);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Invalid secret key";
                return RedirectToAction("DecryptMessage", new { id });
            }

        }
        public async Task<IActionResult> DecryptMessage(string Id)
        {
            Message? message = await _context.Messages.FindAsync(Id);
            if (message == null)
            {
                throw new Exception("Message not found");

            }
            DecryptMessage decryptMessage = new DecryptMessage();
            decryptMessage.Id = message.Id;

            return View(decryptMessage);
        }
    }
}
