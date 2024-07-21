﻿using Microsoft.AspNetCore.Mvc;
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

    }
}
