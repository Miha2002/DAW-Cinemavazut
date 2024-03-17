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
    public class RecenziiController : Controller
    {
        private readonly CinemavazutContext _context;

        public RecenziiController(CinemavazutContext context)
        {
            _context = context;
        }

        // GET: Recenzii
        public async Task<IActionResult> Index()
        {
              return _context.Recenzii != null ? 
                          View(await _context.Recenzii.ToListAsync()) :
                          Problem("Entity set 'CinemavazutContext.Recenzii'  is null.");
        }

        // GET: Recenzii/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recenzii == null)
            {
                return NotFound();
            }

            var recenzii = await _context.Recenzii
                .FirstOrDefaultAsync(m => m.id_recenzie == id);
            if (recenzii == null)
            {
                return NotFound();
            }

            return View(recenzii);
        }

        // GET: Recenzii/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recenzii/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("id_recenzie,titlu,comentariu,rating")] Recenzii recenzii)
        {
            if (ModelState.IsValid)
            {
                int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
                recenzii.id_utilizator = UserId;
                recenzii.id_film = id;
                _context.Add(recenzii);
                await _context.SaveChangesAsync();

                Utilizatori utilizator = _context.Utilizatori.FirstOrDefault(x => x.id_utilizator == UserId);
                utilizator.scor += 5;

                _context.Utilizatori.Update(utilizator);
                await _context.SaveChangesAsync();

                return RedirectToAction("PaginaFilm", "Filme", new { area = "", id = id });
            }
            return View(recenzii);
        }

        // GET: Recenzii/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recenzii == null)
            {
                return NotFound();
            }

            var recenzii = await _context.Recenzii.FindAsync(id);
            if (recenzii == null)
            {
                return NotFound();
            }
            return View(recenzii);
        }

        // POST: Recenzii/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_recenzie,titlu,comentariu,rating,id_film,id_utilizator")] Recenzii recenzii)
        {
            if (id != recenzii.id_recenzie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recenzii);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenziiExists(recenzii.id_recenzie))
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
            return View(recenzii);
        }

        // GET: Recenzii/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recenzii == null)
            {
                return NotFound();
            }

            var recenzii = await _context.Recenzii
                .FirstOrDefaultAsync(m => m.id_recenzie == id);
            if (recenzii == null)
            {
                return NotFound();
            }

            return View(recenzii);
        }

        // POST: Recenzii/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recenzii == null)
            {
                return Problem("Entity set 'CinemavazutContext.Recenzii'  is null.");
            }
            var recenzii = await _context.Recenzii.FindAsync(id);
            if (recenzii != null)
            {
                _context.Recenzii.Remove(recenzii);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecenziiExists(int id)
        {
          return (_context.Recenzii?.Any(e => e.id_recenzie == id)).GetValueOrDefault();
        }
    }
}
