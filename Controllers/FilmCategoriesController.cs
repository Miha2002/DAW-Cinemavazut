using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW_Cinemavazut.ContextModels;
using Proiect_DAW_Cinemavazut.Models;

namespace Proiect_DAW_Cinemavazut.Controllers
{
    public class FilmCategoriesController : Controller
    {
        private readonly CinemavazutContext _context;

        public FilmCategoriesController(CinemavazutContext context)
        {
            _context = context;
        }

        // GET: FilmCategories
        public async Task<IActionResult> Index()
        {
              return _context.FilmCategorie != null ? 
                          View(await _context.FilmCategorie.ToListAsync()) :
                          Problem("Entity set 'CinemavazutContext.FilmCategorie'  is null.");
        }

        // GET: FilmCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FilmCategorie == null)
            {
                return NotFound();
            }

            var filmCategorie = await _context.FilmCategorie
                .FirstOrDefaultAsync(m => m.id_filmcategorie == id);
            if (filmCategorie == null)
            {
                return NotFound();
            }

            return View(filmCategorie);
        }

        // GET: FilmCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FilmCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_filmcategorie,id_film,id_categorie")] FilmCategorie filmCategorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filmCategorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filmCategorie);
        }

        // GET: FilmCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FilmCategorie == null)
            {
                return NotFound();
            }

            var filmCategorie = await _context.FilmCategorie.FindAsync(id);
            if (filmCategorie == null)
            {
                return NotFound();
            }
            return View(filmCategorie);
        }

        // POST: FilmCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_filmcategorie,id_film,id_categorie")] FilmCategorie filmCategorie)
        {
            if (id != filmCategorie.id_filmcategorie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmCategorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmCategorieExists(filmCategorie.id_filmcategorie))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(filmCategorie);
        }

        // GET: FilmCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FilmCategorie == null)
            {
                return NotFound();
            }

            var filmCategorie = await _context.FilmCategorie
                .FirstOrDefaultAsync(m => m.id_filmcategorie == id);
            if (filmCategorie == null)
            {
                return NotFound();
            }

            return View(filmCategorie);
        }

        // POST: FilmCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FilmCategorie == null)
            {
                return Problem("Entity set 'CinemavazutContext.FilmCategorie'  is null.");
            }
            var filmCategorie = await _context.FilmCategorie.FindAsync(id);
            if (filmCategorie != null)
            {
                _context.FilmCategorie.Remove(filmCategorie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmCategorieExists(int id)
        {
          return (_context.FilmCategorie?.Any(e => e.id_filmcategorie == id)).GetValueOrDefault();
        }
    }
}
