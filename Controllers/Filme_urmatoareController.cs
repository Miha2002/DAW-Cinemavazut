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
    public class Filme_urmatoareController : Controller
    {
        private readonly CinemavazutContext _context;

        public Filme_urmatoareController(CinemavazutContext context)
        {
            _context = context;
        }

        // GET: Filme_urmatoare
        public async Task<IActionResult> Index()
        {
              return _context.Filme_urmatoare != null ? 
                          View(await _context.Filme_urmatoare.ToListAsync()) :
                          Problem("Entity set 'CinemavazutContext.Filme_urmatoare'  is null.");
        }

        // GET: Filme_urmatoare/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Filme_urmatoare == null)
            {
                return NotFound();
            }

            var filme_urmatoare = await _context.Filme_urmatoare
                .FirstOrDefaultAsync(m => m.id_urmatoare == id);
            if (filme_urmatoare == null)
            {
                return NotFound();
            }

            return View(filme_urmatoare);
        }

        // GET: Filme_urmatoare/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filme_urmatoare/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_urmatoare,comentariu,id_film,id_utilizator")] Filme_urmatoare filme_urmatoare)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filme_urmatoare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filme_urmatoare);
        }

        // GET: Filme_urmatoare/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Filme_urmatoare == null)
            {
                return NotFound();
            }

            var filme_urmatoare = await _context.Filme_urmatoare.FindAsync(id);
            if (filme_urmatoare == null)
            {
                return NotFound();
            }
            return View(filme_urmatoare);
        }

        // POST: Filme_urmatoare/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_urmatoare,comentariu,id_film,id_utilizator")] Filme_urmatoare filme_urmatoare)
        {
            if (id != filme_urmatoare.id_urmatoare)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filme_urmatoare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Filme_urmatoareExists(filme_urmatoare.id_urmatoare))
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
            return View(filme_urmatoare);
        }

        // GET: Filme_urmatoare/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Filme_urmatoare == null)
            {
                return NotFound();
            }

            var filme_urmatoare = await _context.Filme_urmatoare
                .FirstOrDefaultAsync(m => m.id_urmatoare == id);
            if (filme_urmatoare == null)
            {
                return NotFound();
            }

            return View(filme_urmatoare);
        }

        // POST: Filme_urmatoare/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Filme_urmatoare == null)
            {
                return Problem("Entity set 'CinemavazutContext.Filme_urmatoare'  is null.");
            }
            var filme_urmatoare = await _context.Filme_urmatoare.FindAsync(id);
            if (filme_urmatoare != null)
            {
                _context.Filme_urmatoare.Remove(filme_urmatoare);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Filme_urmatoareExists(int id)
        {
          return (_context.Filme_urmatoare?.Any(e => e.id_urmatoare == id)).GetValueOrDefault();
        }
    }
}
