using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mf_dev_beckend_2023.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace mf_dev_beckend_2023.Controllers
{
    [Authorize(Roles = "Admin")]//somento usuario admin consegue acessar, se tiver sem a especificação de role, tds autorizados consegume
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        //get dessa porra pra poder carrega na desgraça da pagaina o tela de login
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            var user = await _context.usuarios
                .FirstOrDefaultAsync(m => m.ID == usuario.ID);
            if (user == null)
            {
                ViewBag.Message = "Usuário e/ou Senha inválidos!";
                return View();
            }

            //usuario.senha = oq o user digitou, dados.senha oq ta no BD
            bool senharIsRight = BCrypt.Net.BCrypt.Verify(usuario.senha, user.senha);
            if (senharIsRight)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.NameIdentifier, user.Nome),
                    new Claim(ClaimTypes.Role, user.perfil.ToString()),
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                var props = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.ToLocalTime().AddHours(8),
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(principal, props);

                //manda pra raiz do sistema
                return Redirect("/");
            }


            ViewBag.Message = "Usuario e/ou senha invalidos";
                return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Usuarios");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
              return View(await _context.usuarios.ToListAsync());
                
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,senha,perfil")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.senha = BCrypt.Net.BCrypt.HashPassword(usuario.senha);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,senha,perfil")] Usuario usuario)
        {
            if (id != usuario.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.senha = BCrypt.Net.BCrypt.HashPassword(usuario.senha);
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.ID))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usuarios == null)
            {
                return Problem("Entity set 'AppDbContext.usuarios'  is null.");
            }
            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.usuarios?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
