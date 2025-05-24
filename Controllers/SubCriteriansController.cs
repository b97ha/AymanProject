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
                .OrderBy(s => s.MainId)
                .ThenBy(s => s.Id)
                .AsNoTracking()
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
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (subCriterian == null)
            {
                return NotFound();
            }

            return View(subCriterian);
        }

        // GET: SubCriterians/Create
        public async Task<IActionResult> Create(int? mainId)
        {
            if (mainId == null)
            {
                TempData["ErrorMessage"] = CurrentLanguage == "ar"
                    ? "يجب تحديد المعيار الرئيسي"
                    : "Main Criterion must be specified";

                return RedirectToActionWithLang("Index", "MainCriterians");
            }

            var mainCriterian = await _context.MainCriterians
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == mainId);

            if (mainCriterian == null)
            {
                TempData["ErrorMessage"] = CurrentLanguage == "ar"
                    ? "المعيار الرئيسي غير موجود"
                    : "Main Criterion not found";

                return RedirectToActionWithLang("Index", "MainCriterians");
            }

            ViewBag.MainCriterian = mainCriterian;

            var subCriterian = new SubCriterian { MainId = mainId.Value };

            return View(subCriterian);
        }

        // POST: SubCriterians/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainId,Text_Ar,Text_En,Weight")] SubCriterian subCriterian)
        {
            // Validate MainId exists
            var mainCriterianExists = await _context.MainCriterians
                .AnyAsync(m => m.Id == subCriterian.MainId);

            if (!mainCriterianExists)
            {
                ModelState.AddModelError("MainId", "Invalid Main Criterion.");
            }

            // Validate weight range
            if (subCriterian.Weight < 0 || subCriterian.Weight > 1)
            {
                ModelState.AddModelError("Weight", "Weight must be between 0 and 1.");
            }

            // Optional: Validate total weights for this main criterion don't exceed 1
            var existingWeightsSum = await _context.SubCriterians
                .Where(s => s.MainId == subCriterian.MainId)
                .SumAsync(s => s.Weight);

            if (existingWeightsSum + subCriterian.Weight > 1.0)
            {
                ModelState.AddModelError("Weight",
                    CurrentLanguage == "ar"
                        ? $"مجموع الأوزان للمعيار الرئيسي سيتجاوز 1.0. المتبقي: {1.0 - existingWeightsSum:0.000}"
                        : $"Total weights for this main criterion will exceed 1.0. Remaining: {1.0 - existingWeightsSum:0.000}");
            }
            ModelState.Remove("MainCriterian");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(subCriterian);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = CurrentLanguage == "ar"
                        ? "تم إنشاء المعيار الفرعي بنجاح"
                        : "Sub criterian created successfully";

                    return RedirectToActionWithLang("Details", "MainCriterians",
                        routeValues: new { id = subCriterian.MainId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                }
            }

            // If we got this far, something failed, reload the main criterian
            var mainCriterian = await _context.MainCriterians
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == subCriterian.MainId);

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

            // Validate weight range only
            if (subCriterian.Weight < 0 || subCriterian.Weight > 1)
            {
                ModelState.AddModelError("Weight", "Weight must be between 0 and 1.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subCriterian);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = CurrentLanguage == "ar"
                        ? "تم تحديث المعيار الفرعي بنجاح"
                        : "Sub criterian updated successfully";
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
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the data.");

                    // Reload the main criterian for the view
                    var mainCriterian = await _context.MainCriterians
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.Id == subCriterian.MainId);
                    ViewBag.MainCriterian = mainCriterian;

                    return View(subCriterian);
                }

                return RedirectToActionWithLang("Details", "MainCriterians",
                    routeValues: new { id = subCriterian.MainId });
            }

            // If we got this far, something failed, reload the main criterian
            var reloadedMainCriterian = await _context.MainCriterians
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == subCriterian.MainId);
            ViewBag.MainCriterian = reloadedMainCriterian;

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
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (subCriterian == null)
            {
                return NotFound();
            }

            // Check if there are related evaluations
            var hasEvaluations = await _context.ProjectSubCriterians
                .AnyAsync(psc => psc.SubCriterianId == id);

            if (hasEvaluations)
            {
                TempData["ErrorMessage"] = CurrentLanguage == "ar"
                    ? "لا يمكن حذف هذا المعيار لأنه مرتبط بتقييمات مشاريع"
                    : "Cannot delete this criterion as it is associated with project evaluations";

                return RedirectToActionWithLang(nameof(Details), routeValues: new { id });
            }

            return View(subCriterian);
        }

        // POST: SubCriterians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var subCriterian = await _context.SubCriterians.FindAsync(id);

                if (subCriterian != null)
                {
                    // Check again for evaluations
                    var hasEvaluations = await _context.ProjectSubCriterians
                        .AnyAsync(psc => psc.SubCriterianId == id);

                    if (hasEvaluations)
                    {
                        TempData["ErrorMessage"] = CurrentLanguage == "ar"
                            ? "لا يمكن حذف هذا المعيار لأنه مرتبط بتقييمات مشاريع"
                            : "Cannot delete this criterion as it is associated with project evaluations";
                    }
                    else
                    {
                        var mainId = subCriterian.MainId;

                        _context.SubCriterians.Remove(subCriterian);
                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = CurrentLanguage == "ar"
                            ? "تم حذف المعيار الفرعي بنجاح"
                            : "Sub criterian deleted successfully";

                        return RedirectToActionWithLang("Details", "MainCriterians",
                            routeValues: new { id = mainId });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = CurrentLanguage == "ar"
                    ? "حدث خطأ أثناء حذف المعيار"
                    : "An error occurred while deleting the criterion";
            }

            return RedirectToActionWithLang("Index", "MainCriterians");
        }

        private bool SubCriterianExists(int id)
        {
            return _context.SubCriterians.Any(e => e.Id == id);
        }
    }
}