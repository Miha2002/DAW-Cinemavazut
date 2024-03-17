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
    public class QuizuriController : Controller
    {
        private readonly CinemavazutContext _context;

        public QuizuriController(CinemavazutContext context)
        {
            _context = context;
        }

        // GET: Test
        public IActionResult Test(int id)
        {
            var quiz = _context.Quizuri.FirstOrDefault(x => x.id_film == id);
            if (quiz == null)
            {
                Console.WriteLine(id);
                return NotFound();
            }

            return View(quiz);
        }
        public IActionResult Test4()
        {
            return View();
        }
        public IActionResult Test14()
        {
            return View();
        }
        public IActionResult Test15()
        {
            return View();
        }


        // GET: Result
        public IActionResult TestResult()
        {
            int score = TempData["quizScore"] as int? ?? 0;
            ViewBag.QuizScore = score;

            int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            Utilizatori utilizator = _context.Utilizatori.FirstOrDefault(x => x.id_utilizator == UserId);

            utilizator.scor += 10;
            _context.Utilizatori.Update(utilizator);
            _context.SaveChangesAsync();

            return View();
        }

        public IActionResult TestFailed()
        {
            return View();
        }



        // ------------------------------------

        // GET: Quizuri
        public async Task<IActionResult> Index()
        {
              return _context.Quizuri != null ? 
                          View(await _context.Quizuri.ToListAsync()) :
                          Problem("Entity set 'CinemavazutContext.Quizuri'  is null.");
        }

        // GET: Quizuri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Quizuri == null)
            {
                return NotFound();
            }

            var quizuri = await _context.Quizuri
                .FirstOrDefaultAsync(m => m.id_quiz == id);
            if (quizuri == null)
            {
                return NotFound();
            }

            return View(quizuri);
        }

        // GET: Quizuri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quizuri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_quiz,id_film,intrebare1,raspuns1_corect,raspuns1_gresit1,raspuns1_gresit2,intrebare2,raspuns2_corect,raspuns2_gresit1,raspuns2_gresit2,intrebare3,raspuns3_corect,raspuns3_gresit1,raspuns3_gresit2,intrebare4,raspuns4_corect,raspuns4_gresit1,raspuns4_gresit2,intrebare5,raspuns5_corect,raspuns5_gresit1,raspuns5_gresit2")] Quizuri quizuri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quizuri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quizuri);
        }

        // GET: Quizuri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Quizuri == null)
            {
                return NotFound();
            }

            var quizuri = await _context.Quizuri.FindAsync(id);
            if (quizuri == null)
            {
                return NotFound();
            }
            return View(quizuri);
        }

        // POST: Quizuri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_quiz,id_film,intrebare1,raspuns1_corect,raspuns1_gresit1,raspuns1_gresit2,intrebare2,raspuns2_corect,raspuns2_gresit1,raspuns2_gresit2,intrebare3,raspuns3_corect,raspuns3_gresit1,raspuns3_gresit2,intrebare4,raspuns4_corect,raspuns4_gresit1,raspuns4_gresit2,intrebare5,raspuns5_corect,raspuns5_gresit1,raspuns5_gresit2")] Quizuri quizuri)
        {
            if (id != quizuri.id_quiz)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizuri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizuriExists(quizuri.id_quiz))
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
            return View(quizuri);
        }

        // GET: Quizuri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Quizuri == null)
            {
                return NotFound();
            }

            var quizuri = await _context.Quizuri
                .FirstOrDefaultAsync(m => m.id_quiz == id);
            if (quizuri == null)
            {
                return NotFound();
            }

            return View(quizuri);
        }

        // POST: Quizuri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quizuri == null)
            {
                return Problem("Entity set 'CinemavazutContext.Quizuri'  is null.");
            }
            var quizuri = await _context.Quizuri.FindAsync(id);
            if (quizuri != null)
            {
                _context.Quizuri.Remove(quizuri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizuriExists(int id)
        {
          return (_context.Quizuri?.Any(e => e.id_quiz == id)).GetValueOrDefault();
        }
    }
}
