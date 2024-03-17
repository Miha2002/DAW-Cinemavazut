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
    public class Categorii_filmeController : Controller
    {
        private readonly CinemavazutContext _context;

        public Categorii_filmeController(CinemavazutContext context)
        {
            _context = context;
        }

        // GET: Categorii_filme
        public async Task<IActionResult> Index()
        {
              return _context.Categorii_filme != null ? 
                          View(await _context.Categorii_filme.ToListAsync()) :
                          Problem("Entity set 'CinemavazutContext.Categorii_filme'  is null.");
        }

        // GET: Categorii_filme/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categorii_filme == null)
            {
                return NotFound();
            }

            var Categorii_filme = await _context.Categorii_filme
                .FirstOrDefaultAsync(m => m.id_categorie == id);
            if (Categorii_filme == null)
            {
                return NotFound();
            }

            return View(Categorii_filme);
        }

        // GET: Categorii_filme/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorii_filme/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_categorie,denumire")] Categorii_filme Categorii_filme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Categorii_filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Categorii_filme);
        }

        // GET: Categorii_filme/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categorii_filme == null)
            {
                return NotFound();
            }

            var Categorii_filme = await _context.Categorii_filme.FindAsync(id);
            if (Categorii_filme == null)
            {
                return NotFound();
            }
            return View(Categorii_filme);
        }

        // POST: Categorii_filme/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_categorie,denumire")] Categorii_filme Categorii_filme)
        {
            if (id != Categorii_filme.id_categorie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Categorii_filme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Categorii_filmeExists(Categorii_filme.id_categorie))
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
            return View(Categorii_filme);
        }

        // GET: Categorii_filme/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categorii_filme == null)
            {
                return NotFound();
            }

            var Categorii_filme = await _context.Categorii_filme
                .FirstOrDefaultAsync(m => m.id_categorie == id);
            if (Categorii_filme == null)
            {
                return NotFound();
            }

            return View(Categorii_filme);
        }

        // POST: Categorii_filme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categorii_filme == null)
            {
                return Problem("Entity set 'CinemavazutContext.Categorii_filme'  is null.");
            }
            var Categorii_filme = await _context.Categorii_filme.FindAsync(id);
            if (Categorii_filme != null)
            {
                _context.Categorii_filme.Remove(Categorii_filme);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Categorii_filmeExists(int id)
        {
          return (_context.Categorii_filme?.Any(e => e.id_categorie == id)).GetValueOrDefault();
        }
    }
}
