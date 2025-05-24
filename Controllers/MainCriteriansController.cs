using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AymanProject.Models;

namespace AymanProject.Controllers
{
    public class MainCriteriansController : BaseController
    {
        private readonly EvaluationContext _context;

        public MainCriteriansController(EvaluationContext context)
        {
            _context = context;
        }

        // GET: MainCriterians
        public async Task<IActionResult> Index()
        {
            var mainCriterians = await _context.MainCriterians
                .Include(m => m.SubCriterians)
                .OrderBy(m => m.Id)
                .AsNoTracking()
                .ToListAsync();

            return View(mainCriterians);
        }

        // GET: MainCriterians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainCriterian = await _context.MainCriterians
                .Include(m => m.SubCriterians.OrderBy(s => s.Id))
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mainCriterian == null)
            {
                return NotFound();
            }

            return View(mainCriterian);
        }

        // GET: MainCriterians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainCriterians/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text_Ar,Text_En,Weight")] MainCriterian mainCriterian)
        {
            // Validate ID uniqueness
            if (await _context.MainCriterians.AnyAsync(m => m.Id == mainCriterian.Id))
            {
                ModelState.AddModelError("Id", "A Main Criterian with this ID already exists.");
            }

            // Validate weight range
            if (mainCriterian.Weight < 0 || mainCriterian.Weight > 1)
            {
                ModelState.AddModelError("Weight", "Weight must be between 0 and 1.");
            }
            ModelState.Remove("SubCriterians");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(mainCriterian);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = CurrentLanguage == "ar"
                        ? "تم إنشاء المعيار الرئيسي بنجاح"
                        : "Main criterian created successfully";

                    return RedirectToActionWithLang(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                    // Log the exception here
                }
            }

            return View(mainCriterian);
        }

        // GET: MainCriterians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainCriterian = await _context.MainCriterians.FindAsync(id);
            if (mainCriterian == null)
            {
                return NotFound();
            }

            return View(mainCriterian);
        }

        // POST: MainCriterians/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text_Ar,Text_En,Weight")] MainCriterian mainCriterian)
        {
            if (id != mainCriterian.Id)
            {
                return NotFound();
            }

            // Validate weight range
            if (mainCriterian.Weight < 0 || mainCriterian.Weight > 1)
            {
                ModelState.AddModelError("Weight", "Weight must be between 0 and 1.");
            }

            ModelState.Remove("SubCriterians");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainCriterian);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = CurrentLanguage == "ar"
                        ? "تم تحديث المعيار الرئيسي بنجاح"
                        : "Main criterian updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainCriterianExists(mainCriterian.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the data.");
                    return View(mainCriterian);
                }

                return RedirectToActionWithLang(nameof(Index));
            }

            return View(mainCriterian);
        }

        // GET: MainCriterians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainCriterian = await _context.MainCriterians
                .Include(m => m.SubCriterians)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mainCriterian == null)
            {
                return NotFound();
            }

            // Check if there are related evaluations
            var hasEvaluations = await _context.ProjectMainCriterians
                .AnyAsync(pmc => pmc.MainCriterianId == id);

            if (hasEvaluations)
            {
                TempData["ErrorMessage"] = CurrentLanguage == "ar"
                    ? "لا يمكن حذف هذا المعيار لأنه مرتبط بتقييمات مشاريع"
                    : "Cannot delete this criterion as it is associated with project evaluations";

                return RedirectToActionWithLang(nameof(Details), routeValues: new { id });
            }

            ViewData["HasSubCriterians"] = mainCriterian.SubCriterians?.Any() ?? false;

            return View(mainCriterian);
        }

        // POST: MainCriterians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var mainCriterian = await _context.MainCriterians
                    .Include(m => m.SubCriterians)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (mainCriterian != null)
                {
                    // Check again for evaluations
                    var hasEvaluations = await _context.ProjectMainCriterians
                        .AnyAsync(pmc => pmc.MainCriterianId == id);

                    if (hasEvaluations)
                    {
                        TempData["ErrorMessage"] = CurrentLanguage == "ar"
                            ? "لا يمكن حذف هذا المعيار لأنه مرتبط بتقييمات مشاريع"
                            : "Cannot delete this criterion as it is associated with project evaluations";

                        return RedirectToActionWithLang(nameof(Index));
                    }

                    _context.MainCriterians.Remove(mainCriterian);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = CurrentLanguage == "ar"
                        ? "تم حذف المعيار الرئيسي بنجاح"
                        : "Main criterian deleted successfully";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = CurrentLanguage == "ar"
                    ? "حدث خطأ أثناء حذف المعيار"
                    : "An error occurred while deleting the criterion";
            }

            return RedirectToActionWithLang(nameof(Index));
        }

        private bool MainCriterianExists(int id)
        {
            return _context.MainCriterians.Any(e => e.Id == id);
        }
    }
}