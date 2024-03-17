using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW_Cinemavazut.ContextModels;
using Proiect_DAW_Cinemavazut.Models;

namespace Proiect_DAW_Cinemavazut.Controllers
{
    public class AchievementsController : Controller
    {
        private readonly CinemavazutContext _context;

        public AchievementsController(CinemavazutContext context)
        {
            _context = context;
        }

        public void CalculAchievements()
        {
            int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            var achievements = _context.UtilizatorAchievement.Where(x => x.id_utilizator == UserId);
            Utilizatori utilizator = _context.Utilizatori.FirstOrDefault(x => x.id_utilizator == UserId);

            var filme_vazute = _context.Filme_vazute.Where(x => x.id_utilizator == UserId);
            int nr_vazute = filme_vazute.Count();
            var rec = _context.Recenzii.Where(x => x.id_utilizator == UserId);
            int nr_rec = rec.Count();
            var quiz_facute = _context.Quizuri_trecute.Where(x => x.id_utilizator == UserId);
            int nr_quiz = quiz_facute.Count();

            if (nr_vazute >= 1)
            {
                if(!(achievements.Any(x => x.id_achievement == 3)))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 3;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 20;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();

                }
            }

            if (nr_vazute >= 20)
            {
                if (!achievements.Any(x => x.id_achievement == 4))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 4;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 20;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_vazute >= 50)
            {
                if (!achievements.Any(x => x.id_achievement == 6))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 6;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 50;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_vazute >= 100)
            {
                if (!achievements.Any(x => x.id_achievement == 7))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 7;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 100;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_vazute >= 200)
            {
                if (!achievements.Any(x => x.id_achievement == 20))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 20;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 200;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_rec >= 1)
            {
                if (!achievements.Any(x => x.id_achievement == 8))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 8;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 20;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_rec >= 20)
            {
                if (!achievements.Any(x => x.id_achievement == 9))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 9;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 20;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_rec >= 50)
            {
                if (!achievements.Any(x => x.id_achievement == 11))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 11;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 50;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_rec >= 100)
            {
                if (!achievements.Any(x => x.id_achievement == 13))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 13;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 100;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_rec >= 200)
            {
                if (!achievements.Any(x => x.id_achievement == 24))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 24;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 200;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_quiz >= 1)
            {
                if (!achievements.Any(x => x.id_achievement == 15))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 15;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 20;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_quiz  >= 20)
            {
                if (!achievements.Any(x => x.id_achievement == 16))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 16;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 20;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_quiz >= 50)
            {
                if (!achievements.Any(x => x.id_achievement == 17))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 17;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 50;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_quiz >= 100)
            {
                if (!achievements.Any(x => x.id_achievement == 18))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 18;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 100;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

            if (nr_quiz >= 200)
            {
                if (!achievements.Any(x => x.id_achievement == 22))
                {
                    UtilizatorAchievement temp = new();
                    temp.id_utilizator = UserId;
                    temp.id_achievement = 22;
                    _context.UtilizatorAchievement.Add(temp);
                    _context.SaveChanges();

                    utilizator.scor += 200;
                    _context.Utilizatori.Update(utilizator);
                    _context.SaveChanges();
                }
            }

        }

        // GET: Achievements
        public async Task<IActionResult> Badges()
        {
            int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            CalculAchievements();

            dynamic mymodel = new ExpandoObject();
            var ach_filme = _context.Achievements.Where(x => x.categorie_ach == 100);
            var ach_rec = _context.Achievements.Where(x => x.categorie_ach == 101);
            var ach_quiz = _context.Achievements.Where(x => x.categorie_ach == 102);
            var achievements = _context.UtilizatorAchievement.Where(x => x.id_utilizator == UserId);


            mymodel.Ach_Filme = ach_filme;
            mymodel.Ach_Recenzii = ach_rec;
            mymodel.Ach_Quiz = ach_quiz;
            mymodel.Utilizator = achievements;

            return View(mymodel);
        }

        // GET: Achievements
        public async Task<IActionResult> Index()
        {
              return _context.Achievements != null ? 
                          View(await _context.Achievements.ToListAsync()) :
                          Problem("Entity set 'CinemavazutContext.Achievements'  is null.");
        }

        // GET: Achievements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Achievements == null)
            {
                return NotFound();
            }

            var achievements = await _context.Achievements
                .FirstOrDefaultAsync(m => m.id_achievement == id);
            if (achievements == null)
            {
                return NotFound();
            }

            return View(achievements);
        }

        // GET: Achievements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Achievements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_achievement,denumire,id_categorie_ach,prag,badge")] Achievements achievements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(achievements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(achievements);
        }

        // GET: Achievements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Achievements == null)
            {
                return NotFound();
            }

            var achievements = await _context.Achievements.FindAsync(id);
            if (achievements == null)
            {
                return NotFound();
            }
            return View(achievements);
        }

        // POST: Achievements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_achievement,denumire,id_categorie_ach,prag,badge")] Achievements achievements)
        {
            if (id != achievements.id_achievement)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achievements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchievementsExists(achievements.id_achievement))
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
            return View(achievements);
        }

        // GET: Achievements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Achievements == null)
            {
                return NotFound();
            }

            var achievements = await _context.Achievements
                .FirstOrDefaultAsync(m => m.id_achievement == id);
            if (achievements == null)
            {
                return NotFound();
            }

            return View(achievements);
        }

        // POST: Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Achievements == null)
            {
                return Problem("Entity set 'CinemavazutContext.Achievements'  is null.");
            }
            var achievements = await _context.Achievements.FindAsync(id);
            if (achievements != null)
            {
                _context.Achievements.Remove(achievements);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AchievementsExists(int id)
        {
          return (_context.Achievements?.Any(e => e.id_achievement == id)).GetValueOrDefault();
        }
    }
}
