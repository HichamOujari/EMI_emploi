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
    public class ListFilieresController : Controller
    {
        private readonly SchoolContext _context;
        public int idFiliere = 1;

        public ListFilieresController(SchoolContext context)
        {
            _context = context;
        }

        // GET: ListFilieres

        public async Task<IActionResult> Index()
        {

            var listfiliere = await _context.ListFilieres.Where(f=> f.IdFiliere== idFiliere).ToListAsync();
            var filieres = await _context.Filieres.ToArrayAsync();
            var promotions = await _context.Promotions.ToArrayAsync();

            //à afficher
            var ListInfosfilieres = new List<infosFiliere>();
            infosFiliere tmp_InfosFiliere;

            foreach(ListFiliere lf in listfiliere)
            {
                tmp_InfosFiliere = new infosFiliere { id = lf.id, Promotion = promotions.Where(p => p.id == lf.IdPromotion).Select(p=>p.nom).First().ToString(),Filiere=filieres.Where(f=>f.id==lf.IdFiliere).Select(f=>f.nom).First().ToString() };
                ListInfosfilieres.Add(tmp_InfosFiliere);
            }


            return View(ListInfosfilieres);
        }

        // GET: ListFilieres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listFiliere = await _context.ListFilieres
                .FirstOrDefaultAsync(m => m.id == id);
            if (listFiliere == null)
            {
                return NotFound();
            }

            return View(listFiliere);
        }

        // GET: ListFilieres/Create
        public async Task<IActionResult> Create()
        {
            var filieres = await _context.Filieres.ToArrayAsync();
            var promotions = await _context.Promotions.ToArrayAsync();
            ViewBag.filieres = filieres as List<Filiere>;
            ViewBag.promotions = promotions;

            return View();
        }

        // POST: ListFilieres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,IdFiliere,IdPromotion")] ListFiliere listFiliere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listFiliere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listFiliere);
        }

        // GET: ListFilieres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listFiliere = await _context.ListFilieres.FindAsync(id);
            if (listFiliere == null)
            {
                return NotFound();
            }
            return View(listFiliere);
        }

        // POST: ListFilieres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,IdFiliere,IdPromotion")] ListFiliere listFiliere)
        {
            if (id != listFiliere.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listFiliere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListFiliereExists(listFiliere.id))
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
            return View(listFiliere);
        }

        // GET: ListFilieres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listFiliere = await _context.ListFilieres
                .FirstOrDefaultAsync(m => m.id == id);
            if (listFiliere == null)
            {
                return NotFound();
            }

            return View(listFiliere);
        }

        // POST: ListFilieres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listFiliere = await _context.ListFilieres.FindAsync(id);
            _context.ListFilieres.Remove(listFiliere);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListFiliereExists(int id)
        {
            return _context.ListFilieres.Any(e => e.id == id);
        }
    }
}
