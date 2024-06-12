using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class ProfesoresCursosController : Controller
    {
        private readonly BC100520Context _context;

        public ProfesoresCursosController(BC100520Context context)
        {
            _context = context;
        }

        // GET: ProfesoresCursos
        public async Task<IActionResult> Index()
        {
            var BC100520Context = _context.ProfesoresCursos.Include(p => p.IdCursoNavigation).Include(p => p.IdProfesorNavigation);
            return View(await BC100520Context.ToListAsync());
        }

        // GET: ProfesoresCursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProfesoresCursos == null)
            {
                return NotFound();
            }

            var profesoresCurso = await _context.ProfesoresCursos
                .Include(p => p.IdCursoNavigation)
                .Include(p => p.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.IdProfesoresCurso == id);
            if (profesoresCurso == null)
            {
                return NotFound();
            }

            return View(profesoresCurso);
        }

        // GET: ProfesoresCursos/Create
        public IActionResult Create()
        {
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso");
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor");
            return View();
        }

        // POST: ProfesoresCursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProfesoresCurso,IdCurso,IdProfesor")] ProfesoresCurso profesoresCurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesoresCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", profesoresCurso.IdCurso);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", profesoresCurso.IdProfesor);
            return View(profesoresCurso);
        }

        // GET: ProfesoresCursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProfesoresCursos == null)
            {
                return NotFound();
            }

            var profesoresCurso = await _context.ProfesoresCursos.FindAsync(id);
            if (profesoresCurso == null)
            {
                return NotFound();
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", profesoresCurso.IdCurso);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", profesoresCurso.IdProfesor);
            return View(profesoresCurso);
        }

        // POST: ProfesoresCursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProfesoresCurso,IdCurso,IdProfesor")] ProfesoresCurso profesoresCurso)
        {
            if (id != profesoresCurso.IdProfesoresCurso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesoresCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesoresCursoExists(profesoresCurso.IdProfesoresCurso))
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
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", profesoresCurso.IdCurso);
            ViewData["IdProfesor"] = new SelectList(_context.Profesores, "IdProfesor", "IdProfesor", profesoresCurso.IdProfesor);
            return View(profesoresCurso);
        }

        // GET: ProfesoresCursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProfesoresCursos == null)
            {
                return NotFound();
            }

            var profesoresCurso = await _context.ProfesoresCursos
                .Include(p => p.IdCursoNavigation)
                .Include(p => p.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.IdProfesoresCurso == id);
            if (profesoresCurso == null)
            {
                return NotFound();
            }

            return View(profesoresCurso);
        }

        // POST: ProfesoresCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProfesoresCursos == null)
            {
                return Problem("Entity set 'BC100520Context.Estudiantes'  is null.");
            }
            var profesoresCurso = await _context.ProfesoresCursos.FindAsync(id);
            if (profesoresCurso != null)
            {
                _context.ProfesoresCursos.Remove(profesoresCurso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesoresCursoExists(int id)
        {
          return (_context.ProfesoresCursos?.Any(e => e.IdProfesoresCurso == id)).GetValueOrDefault();
        }
    }
}
