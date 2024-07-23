using Microsoft.AspNetCore.Mvc;
using WebChatApp.Data;
using WebChatApp.Models;
using WebChatApp.Services;
using WebChatApp.Utils;
using System.Threading.Tasks;

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
                // Encrypt the message
                var encryptedContent = EncryptionHelper.Encrypt(model.Message, model.SecretKey);

                // Create and save the message
                var message = new Message
                {
                    Content = encryptedContent,
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
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                throw new Exception("Message not found");
            }

            var viewMessage = new ViewMessage
            {
                Id = message.Id,
                EncryptedContent = message.Content,
                TimeCreated = message.TimeCreated
            };

            try
            {
                viewMessage.DecryptedContent = EncryptionHelper.Decrypt(message.Content, decryptMessage.SecretKey);
                return View(viewMessage);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Invalid secret key";
                return RedirectToAction("DecryptMessage", new { id });
            }
        }

        public async Task<IActionResult> DecryptMessage(string id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                throw new Exception("Message not found");
            }

            var decryptMessage = new DecryptMessage
            {
                Id = message.Id
            };

            return View(decryptMessage);
        }
    }
}
