using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW_Cinemavazut.ContextModels;
using Proiect_DAW_Cinemavazut.Models;

namespace Proiect_DAW_Cinemavazut.Controllers
{
    public class FilmeController : Controller
    {
        private readonly CinemavazutContext _context;

        public FilmeController(CinemavazutContext context)
        {
            _context = context;
        }

        // GET: Galerie
        public async Task<IActionResult> Galerie(int? id)
        {
            if (id == 0)
            {
                return _context.Filme != null ?
                            View(await _context.Filme.ToListAsync()) :
                            Problem("Entity set 'CinemavazutContext.Filme'  is null.");
            }

            if (id != 0 || id != null) {
                var legatura = _context.FilmCategorie.Where(x => x.id_categorie == id);
                List<Filme> lista_filme = new List<Filme>();

                if (legatura != null)
                {
                    foreach (var item in legatura)
                    {
                        var temp = _context.Filme.FirstOrDefault(x => x.id_film == item.id_film);
                        lista_filme.Add(temp);
                    }
                }
                if(lista_filme == null)
                {
                    return NotFound();
                }
                return _context.Filme != null ?
                        View(lista_filme) :
                        Problem("Entity set 'CinemavazutContext.Filme'  is null.");
            }

            return NotFound();
        }


        // GET: Galerie Personala
        public async Task<IActionResult> GaleriePersonala()
        {
            int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            List<Filme> filme_view = new List<Filme>();
            foreach (var film in _context.Filme)
            {
                if(_context.Filme_vazute.Any(x => x.id_film == film.id_film && x.id_utilizator == UserId))
                {
                    filme_view.Add(film);
                }
            }
            return View(filme_view);
        }


        // GET: Wishlist
        public async Task<IActionResult> Watchlist()
        {
            int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            List<Filme> filme_view = new List<Filme>();
            foreach (var film in _context.Filme)
            {
                if (_context.Filme_urmatoare.Any(x => x.id_film == film.id_film && x.id_utilizator == UserId))
                {
                    filme_view.Add(film);
                }
            }
            return View(filme_view);
        }


        // GET favorite
        public async Task<IActionResult> Favorite()
        {
            int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            List<Filme> filme_view = new List<Filme>();

            foreach (var film in _context.Filme)
            {
                if (_context.Filme_vazute.Any(x => x.id_film == film.id_film && x.id_utilizator == UserId && x.favorit == true))
                {
                    filme_view.Add(film);
                }
            }
            return View(filme_view);
        }



        // GET: Search Page
        public async Task<IActionResult> Search (string searchString)
        {
            if (_context.Filme == null)
            {
                Problem("Entity set 'CinemavazutContext.Filme'  is null.");
            }

            var movies = from m in _context.Filme
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.titlu!.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }


        // GET: PaginaFilm
        public async Task<IActionResult> PaginaFilm(int? id)
        {
            if (id == null || _context.Filme == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            dynamic mymodel = new ExpandoObject();

            var recenzii = _context.Recenzii.Where(x => x.id_film == filme.id_film);
            List<Utilizatori> utilizatori = new List<Utilizatori>();
            foreach (var item in recenzii)
            {
                var util_recenzii = _context.Utilizatori.Where(x => x.id_utilizator == item.id_utilizator);
                foreach(var i in util_recenzii)
                {
                    utilizatori.Add(i);
                }
            }

            if(Request.Cookies["UserId"] != null) {
                int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
                var vazut = _context.Filme_vazute.FirstOrDefault(x => x.id_utilizator == UserId && x.id_film == id);
                mymodel.Vazut = vazut;

                var watchlist = _context.Filme_urmatoare.FirstOrDefault(x => x.id_utilizator == UserId && x.id_film == id);
                mymodel.Watchlist = watchlist;
            }

            mymodel.Film = filme;
            mymodel.Recenzii = recenzii;
            mymodel.Utilizatori = utilizatori;

            return View(mymodel);
        }


        public async Task<IActionResult> AddFavList(int id)
        {
            int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            Filme_vazute? vazut = _context.Filme_vazute.FirstOrDefault(x => x.id_utilizator == UserId && x.id_film == id);
            Utilizatori utilizator = _context.Utilizatori.FirstOrDefault(x => x.id_utilizator == UserId);

            if (vazut != null)
            {
                vazut.favorit = !vazut.favorit;
                _context.Filme_vazute.Update(vazut);
                await _context.SaveChangesAsync();
            }
            else
            {
                Filme_vazute temp = new()
                {
                    id_utilizator = UserId,
                    id_film = id,
                    favorit = true
                };

                utilizator.scor += 2;

                _context.Utilizatori.Update(utilizator);
                await _context.SaveChangesAsync();

                _context.Filme_vazute.Update(temp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("PaginaFilm", "Filme", new { area = "", id = id });
        }
        public async Task<IActionResult> AddVazutList(int id)
        {
            int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            Filme_vazute? vazut = _context.Filme_vazute.FirstOrDefault(x => x.id_utilizator == UserId && x.id_film == id);
            Utilizatori utilizator = _context.Utilizatori.FirstOrDefault(x => x.id_utilizator == UserId);

            if (vazut != null)
            {
                utilizator.scor -= 2;
                _context.Utilizatori.Update(utilizator);
                await _context.SaveChangesAsync();

                _context.Filme_vazute.Remove(vazut);
                await _context.SaveChangesAsync();
            }
            else
            {
                Filme_vazute temp = new()
                {
                    id_utilizator = UserId,
                    id_film = id,
                    favorit = false
                };
                utilizator.scor += 2;
                _context.Utilizatori.Update(utilizator);
                await _context.SaveChangesAsync();

                _context.Filme_vazute.Update(temp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("PaginaFilm", "Filme", new { area = "", id = id });
        }
        public async Task<IActionResult> AddWatchList(int id)
        {
            int UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            Filme_urmatoare? urm = _context.Filme_urmatoare.FirstOrDefault(x => x.id_utilizator == UserId && x.id_film == id);

            if (urm != null)
            {
                _context.Filme_urmatoare.Remove(urm);
                await _context.SaveChangesAsync();
            }
            else
            {
                Filme_urmatoare temp = new()
                {
                    id_utilizator = UserId,
                    id_film = id,
                };
                _context.Filme_urmatoare.Update(temp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("PaginaFilm", "Filme", new { area = "", id = id });
        }




        // ------------------------------


        // GET: Filme
        public async Task<IActionResult> Index()
        {
              return _context.Filme != null ? 
                          View(await _context.Filme.ToListAsync()) :
                          Problem("Entity set 'CinemavazutContext.Filme'  is null.");
        }

        // GET: Filme/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Filme == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.id_film == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // GET: Filme/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filme/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_film,titlu,an_lansare,descriere,studio,durata")] Filme filme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filme/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Filme == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }

        // POST: Filme/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_film,titlu,an_lansare,descriere,studio,durata")] Filme filme)
        {
            if (id != filme.id_film)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmeExists(filme.id_film))
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
            return View(filme);
        }

        // GET: Filme/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Filme == null)
            {
                return NotFound();
            }

            var filme = await _context.Filme
                .FirstOrDefaultAsync(m => m.id_film == id);
            if (filme == null)
            {
                return NotFound();
            }

            return View(filme);
        }

        // POST: Filme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Filme == null)
            {
                return Problem("Entity set 'CinemavazutContext.Filme'  is null.");
            }
            var filme = await _context.Filme.FindAsync(id);
            if (filme != null)
            {
                _context.Filme.Remove(filme);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmeExists(int id)
        {
          return (_context.Filme?.Any(e => e.id_film == id)).GetValueOrDefault();
        }
    }
}
