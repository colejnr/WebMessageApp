using Microsoft.AspNetCore.Mvc;
using WebChatApp.Data;

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
    

    }
}
