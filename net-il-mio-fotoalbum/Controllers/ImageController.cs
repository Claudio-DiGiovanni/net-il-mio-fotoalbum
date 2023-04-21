using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ImageController : Controller
    {
        private readonly AlbumContext _context;

        public ImageController(AlbumContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var images = _context.Images.Include(i => i.Categories).ToArray();

            return View(images);
        }

        public IActionResult Detail(int id)
        {
            var image = _context.Images.Include(i => i.Categories).SingleOrDefault(i => i.Id == id);

            if (image is null)
            {
                return NotFound();
            }

            return View(image);
        }

        public IActionResult Create()
        {
            var formModel = new ImageFormModel
            {
                Categories = _context.Categories!.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToList(),
            };

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ImageFormModel form)
        {
            if (!ModelState.IsValid)
            {
                form.Categories = _context.Categories.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToList();

                return View(form);
            }

            form.Image.Categories = form.SelectedCategoryIds.Select(sc => _context.Categories.FirstOrDefault(c => c.Id == Convert.ToInt32(sc) )).ToList()!;

            _context.Images.Add(form.Image);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}