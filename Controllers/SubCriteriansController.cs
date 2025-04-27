using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AymanProject.Models;

namespace AymanProject.Controllers
{
    public class SubCriteriansController : Controller
    {
        private readonly EvaluationContext _context;

        public SubCriteriansController(EvaluationContext context)
        {
            _context = context;
        }

        // GET: SubCriterians
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubCriterians.ToListAsync());
        }

        // GET: SubCriterians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCriterian = await _context.SubCriterians
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subCriterian == null)
            {
                return NotFound();
            }

            return View(subCriterian);
        }

        // GET: SubCriterians/Create
        public IActionResult Create(int mainId)
        {
            var subCriterian = new SubCriterian { MainId = mainId };
            return View(subCriterian);
        }

        // POST: SubCriterians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainId,Text_Ar,Text_En,Weight")] SubCriterian subCriterian)
        {
            ModelState.Remove("MainCriterian");

            if (ModelState.IsValid)
            {
                _context.Add(subCriterian);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "MainCriterians", new { id = subCriterian.MainId });
            }
            return View(subCriterian);
        }
     
        // GET: SubCriterians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCriterian = await _context.SubCriterians.FindAsync(id);
            if (subCriterian == null)
            {
                return NotFound();
            }
            return View(subCriterian);
        }

        // POST: SubCriterians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MainId,Text_Ar,Text_En,Weight")] SubCriterian subCriterian, string lang = "en")
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
                _context.SubCriterians.Remove(subCriterian);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "MainCriterians", new { id = subCriterian.MainId });
        }

        private bool SubCriterianExists(int id)
        {
            return _context.SubCriterians.Any(e => e.Id == id);
        }
    }
}
