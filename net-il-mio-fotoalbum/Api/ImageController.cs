using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly AlbumContext _context;

        public ImageController(AlbumContext context)
        {
            _context = context;
        }

        [HttpGet("/")]
        public IActionResult GetImages([FromQuery] string? title)
        {
            var images = _context.Images!
                .Where(i => title == null ||
                 i.Title.ToLower()
                .Contains(title.ToLower()))
                .Include(i => i.Categories)
                .ToList();

            foreach (var image in images) image.Categories = null;

            return Ok(images);
        }

        [HttpGet("{id}")]
        public IActionResult GetImage(int? id)
        {
            var image = _context.Images!.FirstOrDefault(p => p.Id == id);

            if (image is null)
            {
                return NotFound();
            }

            return Ok(image);
        }

    }
}
