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
    public class EtudiantsController : Controller
    {
        private readonly SchoolContext _context;
        public int idFiliere = 1;

        public EtudiantsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Etudiants
        public async Task<IActionResult> Index()
        {
            var promotions = await _context.Promotions.ToListAsync();
            var filiere = await _context.ListFilieres.Where(filiere => filiere.IdFiliere == idFiliere).ToListAsync();
            var groupes = await _context.Groupes.Where(grp => filiere.Select(filiere => filiere.id).Contains(grp.IdFiliere)).ToListAsync();

            var listEtudiant = await _context.Etudiants.Where(e => groupes.Select(grp => grp.id).Contains(e.IdGroupe)).ToListAsync();
            
            var listInfosEtudiant = new List<InfosEtudiant>();
            var infosEtudiant = new InfosEtudiant();

            string groupeName = null;
            string promotionName = null;
            string filiereName =null;

            foreach (Etudiant e in listEtudiant)
            {
                foreach (Groupe grp in groupes)
                {
                    if (e.IdGroupe == grp.id)
                    {
                        groupeName = grp.nom;
                        foreach(ListFiliere f in filiere)
                        {
                            if (f.id == grp.IdFiliere)
                            {
                                foreach(Promotion pro in promotions)
                                {
                                    if (pro.id == f.IdPromotion)
                                    {
                                        filiereName = await _context.Filieres.Where(fil => fil.id == f.IdFiliere).Select(fil => fil.nom).FirstAsync();
                                        promotionName = pro.nom;
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
                
                infosEtudiant = new InfosEtudiant { id = e.id, nom = e.nom, prenom = e.prenom, email = e.email, date_naissance = e.date_naissance, Groupe = groupeName,filiere= filiereName, promotion=promotionName };
                listInfosEtudiant.Add(infosEtudiant);
            }
            return View(listInfosEtudiant);
        }

        // GET: Etudiants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View(null);
            }

            var etudiant = await _context.Etudiants
                .FirstOrDefaultAsync(m => m.id == id);
            if (etudiant == null)
            {
                return View(null);
            }

            return View(etudiant);
        }

        // GET: Etudiants/Create
        public async Task<IActionResult> Create()
        {
            var promotions = await _context.Promotions.ToListAsync();
            var filiere = await _context.ListFilieres.Where(filiere => filiere.IdFiliere == idFiliere).ToListAsync();
            var groupes = await _context.Groupes.Where(grp => filiere.Select(filiere => filiere.id).Contains(grp.IdFiliere)).ToListAsync();
            var listGroup = new List<GroupePromotion>();

            var groupPromotion = new GroupePromotion();
            string promName = null;
            foreach(Groupe grp in groupes)
            {
                promName = promotions.Where(p => filiere.Where(f => f.IdFiliere == grp.IdFiliere).Select(f => f.IdPromotion).Contains(p.id)).Select(p=>p.nom).First().ToString();
                groupPromotion = new GroupePromotion { idGroup = grp.id, groupeName = grp.nom,PromotionName = promName };
                listGroup.Add(groupPromotion);
            }

            ViewBag.Groups = listGroup;

            return View();
        }

        // POST: Etudiants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nom,prenom,email,date_naissance,IdGroupe")] Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etudiant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etudiant);
        }

        // GET: Etudiants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants.FindAsync(id);
            if (etudiant == null)
            {
                return NotFound();
            }
            return View(etudiant);
        }

        // POST: Etudiants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nom,prenom,email,date_naissance,IdGroupe")] Etudiant etudiant)
        {
            if (id != etudiant.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etudiant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtudiantExists(etudiant.id))
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
            return View(etudiant);
        }

        // GET: Etudiants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etudiant = await _context.Etudiants
                .FirstOrDefaultAsync(m => m.id == id);
            if (etudiant == null)
            {
                return NotFound();
            }

            return View(etudiant);
        }

        // POST: Etudiants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etudiant = await _context.Etudiants.FindAsync(id);
            _context.Etudiants.Remove(etudiant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtudiantExists(int id)
        {
            return _context.Etudiants.Any(e => e.id == id);
        }
    }
}