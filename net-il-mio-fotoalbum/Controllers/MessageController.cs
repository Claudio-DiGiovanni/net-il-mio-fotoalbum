using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;
using System.Data;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {

        private readonly AlbumContext _context;

        public MessageController(AlbumContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var messages = _context.Messages!.ToArray();
            return View(messages);
        }
    }
}
