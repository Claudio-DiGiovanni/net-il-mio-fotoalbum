using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly AlbumContext _context;

        public CategoryController(AlbumContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories!.ToArray();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var categoryToDelete = _context.Categories!.FirstOrDefault(c => c.Id == id);
            if (categoryToDelete is null)
            {
                return NotFound();
            }
            _context.Categories!.Remove(categoryToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
