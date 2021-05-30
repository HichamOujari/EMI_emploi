using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using emi_emploi.Data;
using emi_emploi.Models;

namespace emi_emploi.Controllers
{
    public class FilieresController : Controller
    {
        private readonly SchoolContext _context;

        public FilieresController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Filieres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Filieres.ToListAsync());
        }

        // GET: Filieres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiere = await _context.Filieres
                .FirstOrDefaultAsync(m => m.id == id);
            if (filiere == null)
            {
                return NotFound();
            }

            return View(filiere);
        }

        // GET: Filieres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filieres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nom")] Filiere filiere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filiere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filiere);
        }

        // GET: Filieres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiere = await _context.Filieres.FindAsync(id);
            if (filiere == null)
            {
                return NotFound();
            }
            return View(filiere);
        }

        // POST: Filieres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nom")] Filiere filiere)
        {
            if (id != filiere.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filiere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FiliereExists(filiere.id))
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
            return View(filiere);
        }

        // GET: Filieres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filiere = await _context.Filieres
                .FirstOrDefaultAsync(m => m.id == id);
            if (filiere == null)
            {
                return NotFound();
            }

            return View(filiere);
        }

        // POST: Filieres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filiere = await _context.Filieres.FindAsync(id);
            _context.Filieres.Remove(filiere);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FiliereExists(int id)
        {
            return _context.Filieres.Any(e => e.id == id);
        }
    }
}
