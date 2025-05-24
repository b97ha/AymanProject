using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AymanProject.Models;

namespace AymanProject.Controllers
{
    public class SubCriteriansController : BaseController
    {
        private readonly EvaluationContext _context;

        public SubCriteriansController(EvaluationContext context)
        {
            _context = context;
        }

        // GET: SubCriterians
        public async Task<IActionResult> Index()
        {
            var subCriterians = await _context.SubCriterians
                .Include(s => s.MainCriterian)
                .ToListAsync();
            return View(subCriterians);
        }

        // GET: SubCriterians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCriterian = await _context.SubCriterians
                .Include(s => s.MainCriterian)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subCriterian == null)
            {
                return NotFound();
            }

            return View(subCriterian);
        }

        // GET: SubCriterians/Create
        public async Task<IActionResult> Create(int mainId)
        {
            var mainCriterian = await _context.MainCriterians.FindAsync(mainId);
            if (mainCriterian == null)
            {
                return NotFound();
            }

            ViewBag.MainCriterian = mainCriterian;
            var subCriterian = new SubCriterian { MainId = mainId };
            return View(subCriterian);
        }

        // POST: SubCriterians/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainId,Text_Ar,Text_En,Weight")] SubCriterian subCriterian)
        {
            ModelState.Remove("MainCriterian");

            if (ModelState.IsValid)
            {
                _context.Add(subCriterian);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sub criterian created successfully";
                return RedirectToAction("Details", "MainCriterians", new { id = subCriterian.MainId });
            }

            // If we got this far, something failed, reload the main criterian
            var mainCriterian = await _context.MainCriterians.FindAsync(subCriterian.MainId);
            ViewBag.MainCriterian = mainCriterian;
            return View(subCriterian);
        }

        // GET: SubCriterians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCriterian = await _context.SubCriterians
                .Include(s => s.MainCriterian)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (subCriterian == null)
            {
                return NotFound();
            }
            return View(subCriterian);
        }

        // POST: SubCriterians/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MainId,Text_Ar,Text_En,Weight")] SubCriterian subCriterian)
        {
            if (id != subCriterian.Id)
            {
                return NotFound();
            }
            ModelState.Remove("MainCriterian");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subCriterian);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Sub criterian updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCriterianExists(subCriterian.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "MainCriterians", new { id = subCriterian.MainId });
            }

            // If we got this far, something failed, reload the main criterian
            var mainCriterian = await _context.MainCriterians.FindAsync(subCriterian.MainId);
            ViewBag.MainCriterian = mainCriterian;
            return View(subCriterian);
        }

        // GET: SubCriterians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCriterian = await _context.SubCriterians
                .Include(s => s.MainCriterian)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subCriterian == null)
            {
                return NotFound();
            }

            return View(subCriterian);
        }

        // POST: SubCriterians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCriterian = await _context.SubCriterians.FindAsync(id);
            if (subCriterian != null)
            {
                var mainId = subCriterian.MainId;
                _context.SubCriterians.Remove(subCriterian);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sub criterian deleted successfully";
                return RedirectToAction("Details", "MainCriterians", new { id = mainId });
            }

            return RedirectToAction("Index", "MainCriterians");
        }

        private bool SubCriterianExists(int id)
        {
            return _context.SubCriterians.Any(e => e.Id == id);
        }
    }
}