using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly BC100520Context _context;

        public InscripcionesController(BC100520Context context)
        {
            _context = context;
        }

        // GET: Inscripciones
        public async Task<IActionResult> Index()
        {
            var BC100520Context = _context.CursosInscritos.Include(c => c.IdCursoNavigation).Include(c => c.IdEstudianteNavigation);
            return View(await BC100520Context.ToListAsync());
        }

        // GET: Inscripciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CursosInscritos == null)
            {
                return NotFound();
            }

            var cursosInscrito = await _context.CursosInscritos
                .Include(c => c.IdCursoNavigation)
                .Include(c => c.IdEstudianteNavigation)
                .FirstOrDefaultAsync(m => m.IdInscripcion == id);
            if (cursosInscrito == null)
            {
                return NotFound();
            }

            return View(cursosInscrito);
        }

        // GET: Inscripciones/Create
        public IActionResult Create()
        {
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso");
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante");
            return View();
        }

        // POST: Inscripciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInscripcion,IdEstudiante,IdCurso")] CursosInscrito cursosInscrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cursosInscrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", cursosInscrito.IdCurso);
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", cursosInscrito.IdEstudiante);
            return View(cursosInscrito);
        }

        // GET: Inscripciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CursosInscritos == null)
            {
                return NotFound();
            }

            var cursosInscrito = await _context.CursosInscritos.FindAsync(id);
            if (cursosInscrito == null)
            {
                return NotFound();
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", cursosInscrito.IdCurso);
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", cursosInscrito.IdEstudiante);
            return View(cursosInscrito);
        }

        // POST: Inscripciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInscripcion,IdEstudiante,IdCurso")] CursosInscrito cursosInscrito)
        {
            if (id != cursosInscrito.IdInscripcion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursosInscrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursosInscritoExists(cursosInscrito.IdInscripcion))
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
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "IdCurso", cursosInscrito.IdCurso);
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", cursosInscrito.IdEstudiante);
            return View(cursosInscrito);
        }

        // GET: Inscripciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CursosInscritos == null)
            {
                return NotFound();
            }

            var cursosInscrito = await _context.CursosInscritos
                .Include(c => c.IdCursoNavigation)
                .Include(c => c.IdEstudianteNavigation)
                .FirstOrDefaultAsync(m => m.IdInscripcion == id);
            if (cursosInscrito == null)
            {
                return NotFound();
            }

            return View(cursosInscrito);
        }

        // POST: Inscripciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CursosInscritos == null)
            {
                return Problem("Entity set 'BC100520Context.CursosInscritos'  is null.");
            }
            var cursosInscrito = await _context.CursosInscritos.FindAsync(id);
            if (cursosInscrito != null)
            {
                _context.CursosInscritos.Remove(cursosInscrito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursosInscritoExists(int id)
        {
          return (_context.CursosInscritos?.Any(e => e.IdInscripcion == id)).GetValueOrDefault();
        }
    }
}
