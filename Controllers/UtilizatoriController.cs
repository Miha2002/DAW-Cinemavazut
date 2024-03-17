using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing.Printing;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW_Cinemavazut.ContextModels;
using Proiect_DAW_Cinemavazut.Models;

namespace Proiect_DAW_Cinemavazut.Controllers
{
    public class UtilizatoriController : Controller
    {
        private readonly CinemavazutContext _context;

        public UtilizatoriController(CinemavazutContext context)
        {
            _context = context;
        }


        // GET: Profile
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null || _context.Utilizatori == null)
            {
                return NotFound();
            }

            var utilizatori = await _context.Utilizatori.FindAsync(id);
            if (utilizatori == null)
            {
                return NotFound();
            }
            /*return _context.Utilizatori != null ?
                        View(utilizatori) :
                        Problem("Entity set 'CinemavazutContext.Utilizatori'  is null.");*/

            dynamic mymodel = new ExpandoObject();
            mymodel.Profil = utilizatori;
            var result = (from c in _context.Utilizatori select c).OrderByDescending(x => x.scor).ToList();
            mymodel.Utilizatori = result;
            return View(mymodel);
        }

        // GET: Utilizatori
        public async Task<IActionResult> Index()
        {
              return _context.Utilizatori != null ? 
                          View(await _context.Utilizatori.ToListAsync()) :
                          Problem("Entity set 'CinemavazutContext.Utilizatori'  is null.");
        }

        // GET: Utilizatori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Utilizatori == null)
            {
                return NotFound();
            }

            var utilizatori = await _context.Utilizatori
                .FirstOrDefaultAsync(m => m.id_utilizator == id);
            if (utilizatori == null)
            {
                return NotFound();
            }

            return View(utilizatori);
        }

        // GET: Utilizatori/Create
        public IActionResult Create()
        {
            return View();
        }


        //Validations
        static bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        static bool IsValidPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
            return isValidated;
        }

        static bool IsValidPhone(string phone)
        {
            if (phone.Length != 10)
            {
                return false;
            }
            else 
            { 
                return true;
            }
        }

        static bool IsValidAge(DateTime birthday)
        {
            int age = DateTime.Today.Year - birthday.Year;
            if (age < 12)
            {
                return false;
            }
            return true;
        }

        // POST: Utilizatori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_utilizator,nume,prenume,email,parola,telefon,data_nastere")] Utilizatori utilizatori)
        {
            var users = _context.Utilizatori;
            foreach (var user in users)
            {
                if (user.email == utilizatori.email)
                {
                    ModelState.AddModelError(nameof(Utilizatori.email), "Email-ul acesta este luat!");
                }

                if (user.telefon == utilizatori.telefon)
                {

                    ModelState.AddModelError(nameof(Utilizatori.telefon), "Nr. acesta de telefon este luat!");
                }
            }

            if (!IsValidEmail(utilizatori.email))
            {
                ModelState.AddModelError(nameof(Utilizatori.email), "Email-ul invalid!");
            }

            if (!IsValidPassword(utilizatori.parola))
            {
                ModelState.AddModelError(nameof(Utilizatori.email), "Parola invalida! (min.8 char., sa includa A-Z si 1-9)");
            }

            if (utilizatori.telefon != null)
            {
                if (!IsValidPhone(utilizatori.telefon))
                {
                    ModelState.AddModelError(nameof(Utilizatori.email), "Nr. de telefon invalid!");
                }
            }

            if (!IsValidAge(utilizatori.data_nastere))
            {
                ModelState.AddModelError(nameof(Utilizatori.email), "Varsta minima de 13 ani!");
            }

            if (ModelState.IsValid)
            {
                utilizatori.rol = 1;
                utilizatori.data_inscriere = DateTime.Now;
                _context.Add(utilizatori);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(utilizatori);
        }

        // GET: Utilizatori/SignIn
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn (bool rememberMe, [Bind("email,parola")] Utilizatori utilizatori)
        {
            Console.WriteLine(utilizatori.email);
            Console.WriteLine(utilizatori.parola);
            Console.WriteLine(rememberMe);
            await _context.SaveChangesAsync();

            var util = _context.Utilizatori.Any(x => x.email == utilizatori.email && x.parola == utilizatori.parola);
            
            if (util)
            {
                var temp = _context.Utilizatori.FirstOrDefault(x => x.email == utilizatori.email);
                int id = temp.id_utilizator;
                int rol = temp.rol;
                FinalizeSignIn(id, utilizatori.email, rol, rememberMe);
                AboutCookies();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else return View(utilizatori);
        }

        private void FinalizeSignIn(int id, string email, int rol, bool createPersistentCookie)
        {
            int timeout = createPersistentCookie ? 30 : 1;

            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(timeout);
            
            // 1 - loggedin
            // 100 - admin
            // else - not loggedin

            if (rol == 1)
            {
                Console.WriteLine("User logged in!");
            }
            else if(rol == 100)
            {
                Console.WriteLine("Admin logged in!");
            }
            else
            {
                Console.WriteLine("Error: You shouldn't be here!");
            }
            if (id == null)
            {
                Console.WriteLine("ID issue!");
            }
            if (email == null)
            {
                Console.WriteLine("Email issue!");
            }
            const string CookieUserId = "UserId";
            const string CookieUserEmail = "UserEmail";
            const string CookieUserRol = "UserRol";

            Response.Cookies.Append(CookieUserId, id.ToString(), cookieOptions);
            Response.Cookies.Append(CookieUserEmail, email.ToString(), cookieOptions);
            Response.Cookies.Append(CookieUserRol, rol.ToString(), cookieOptions);
        }

        public void AboutCookies()
        {
            //Accessing the Cookie Data inside a Method
            if(Request.Cookies["UserId"]==null)
            {
                Console.WriteLine("Something went wrong!");
            }
            string? UserEmail = Request.Cookies["UserEmail"];
            int? UserId = Convert.ToInt32(Request.Cookies["UserId"]);
            string Message = $"UserEmail: {UserEmail}, UserId: {UserId}";
            Console.WriteLine(Message);
        }

        public IActionResult DeleteCookie()
        {
            // Delete the cookie from the browser.
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("UserEmail");

            return RedirectToAction("SignIn", "Utilizatori", new { area = "" });
        }


        // GET: Utilizatori/Edit/5
        public async Task<IActionResult> EditProfile(int? id)
        {
            if (id == null || _context.Utilizatori == null)
            {
                return NotFound();
            }

            var utilizatori = await _context.Utilizatori.FindAsync(id);
            if (utilizatori == null)
            {
                return NotFound();
            }
            return View(utilizatori);
        }

        // POST: Utilizatori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int id, [Bind("id_utilizator,nume,prenume,parola,telefon,data_nastere")] Utilizatori utilizatori)
        {
            if (id != utilizatori.id_utilizator)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizatori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizatoriExists(utilizatori.id_utilizator))
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
            return View(utilizatori);
        }



        // GET: Utilizatori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Utilizatori == null)
            {
                return NotFound();
            }

            var utilizatori = await _context.Utilizatori.FindAsync(id);
            if (utilizatori == null)
            {
                return NotFound();
            }
            return View(utilizatori);
        }

        // POST: Utilizatori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_utilizator,nume,prenume,email,parola,telefon,data_nastere,data_inscriere,rol")] Utilizatori utilizatori)
        {
            if (id != utilizatori.id_utilizator)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizatori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizatoriExists(utilizatori.id_utilizator))
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
            return View(utilizatori);
        }

        // GET: Utilizatori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Utilizatori == null)
            {
                return NotFound();
            }

            var utilizatori = await _context.Utilizatori
                .FirstOrDefaultAsync(m => m.id_utilizator == id);
            if (utilizatori == null)
            {
                return NotFound();
            }

            return View(utilizatori);
        }

        // POST: Utilizatori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Utilizatori == null)
            {
                return Problem("Entity set 'CinemavazutContext.Utilizatori'  is null.");
            }
            var utilizatori = await _context.Utilizatori.FindAsync(id);
            if (utilizatori != null)
            {
                _context.Utilizatori.Remove(utilizatori);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizatoriExists(int id)
        {
          return (_context.Utilizatori?.Any(e => e.id_utilizator == id)).GetValueOrDefault();
        }
    }
}
