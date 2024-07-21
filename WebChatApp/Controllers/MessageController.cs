using Microsoft.AspNetCore.Mvc;

namespace WebChatApp.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDBContext _context;
        public readonly MessageController(ApplicationDBContext context)
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

    }
}
