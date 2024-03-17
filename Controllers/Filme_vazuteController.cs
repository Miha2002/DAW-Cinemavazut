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
    public class Filme_vazuteController : Controller
    {
        private readonly CinemavazutContext _context;

        public Filme_vazuteController(CinemavazutContext context)
        {
            _context = context;
        }

        // GET: Filme_vazute
        public async Task<IActionResult> Index()
        {
              return _context.Filme_vazute != null ? 
                          View(await _context.Filme_vazute.ToListAsync()) :
                          Problem("Entity set 'CinemavazutContext.Filme_vazute'  is null.");
        }

        // GET: Filme_vazute/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Filme_vazute == null)
            {
                return NotFound();
            }

            var filme_vazute = await _context.Filme_vazute
                .FirstOrDefaultAsync(m => m.id_vazute == id);
            if (filme_vazute == null)
            {
                return NotFound();
            }

            return View(filme_vazute);
        }

        // GET: Filme_vazute/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filme_vazute/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_vazute,comentariu,favorit,id_film,id_utilizator")] Filme_vazute filme_vazute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filme_vazute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filme_vazute);
        }

        // GET: Filme_vazute/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Filme_vazute == null)
            {
                return NotFound();
            }

            var filme_vazute = await _context.Filme_vazute.FindAsync(id);
            if (filme_vazute == null)
            {
                return NotFound();
            }
            return View(filme_vazute);
        }

        // POST: Filme_vazute/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_vazute,comentariu,favorit,id_film,id_utilizator")] Filme_vazute filme_vazute)
        {
            if (id != filme_vazute.id_vazute)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filme_vazute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Filme_vazuteExists(filme_vazute.id_vazute))
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
            return View(filme_vazute);
        }

        // GET: Filme_vazute/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Filme_vazute == null)
            {
                return NotFound();
            }

            var filme_vazute = await _context.Filme_vazute
                .FirstOrDefaultAsync(m => m.id_vazute == id);
            if (filme_vazute == null)
            {
                return NotFound();
            }

            return View(filme_vazute);
        }

        // POST: Filme_vazute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Filme_vazute == null)
            {
                return Problem("Entity set 'CinemavazutContext.Filme_vazute'  is null.");
            }
            var filme_vazute = await _context.Filme_vazute.FindAsync(id);
            if (filme_vazute != null)
            {
                _context.Filme_vazute.Remove(filme_vazute);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Filme_vazuteExists(int id)
        {
          return (_context.Filme_vazute?.Any(e => e.id_vazute == id)).GetValueOrDefault();
        }
    }
}
