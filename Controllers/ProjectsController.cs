using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AymanProject.Models;

namespace AymanProject.Controllers
{
    public class ProjectsController : BaseController // Changed to inherit from BaseController
    {
        private readonly EvaluationContext _context;

        public ProjectsController(EvaluationContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.ProjectMainCriterians)
                .ThenInclude(i => i.MainCriterian)
                .Include(p => p.ProjectMainCriterians)
                .ThenInclude(i => i.ProjectSubCriterians)
                .ThenInclude(i => i.SubCriterian)
                .OrderByDescending(p => p.SubmittedOn)
                .AsNoTracking()
                .ToListAsync();

            return View(projects);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.MainCriterian)
                .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.ProjectSubCriterians)
                        .ThenInclude(psc => psc.SubCriterian)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Location,Description,SubmittedOn,EndOn")] Project project)
        {
            // Clear any validation errors for navigation properties
            ModelState.Remove("ProjectMainCriterians");

            // Validate dates
            if (project.EndOn <= project.SubmittedOn)
            {
                ModelState.AddModelError("EndOn", CurrentLanguage == "ar"
                    ? "تاريخ الانتهاء يجب أن يكون بعد تاريخ التقديم"
                    : "End date must be after submitted date");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(project);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = CurrentLanguage == "ar"
                        ? "تم إنشاء المشروع بنجاح"
                        : "Project created successfully";

                    return RedirectToActionWithLang(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", CurrentLanguage == "ar"
                        ? "حدث خطأ أثناء حفظ البيانات"
                        : "An error occurred while saving the data");
                }
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Location,Description,SubmittedOn,EndOn")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            ModelState.Remove("ProjectMainCriterians");

            // Validate dates
            if (project.EndOn <= project.SubmittedOn)
            {
                ModelState.AddModelError("EndOn", CurrentLanguage == "ar"
                    ? "تاريخ الانتهاء يجب أن يكون بعد تاريخ التقديم"
                    : "End date must be after submitted date");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = CurrentLanguage == "ar"
                        ? "تم تحديث المشروع بنجاح"
                        : "Project updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
                    ModelState.AddModelError("", CurrentLanguage == "ar"
                        ? "حدث خطأ أثناء تحديث البيانات"
                        : "An error occurred while updating the data");
                    return View(project);
                }

                return RedirectToActionWithLang(nameof(Index));
            }

            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ProjectMainCriterians)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            // Check if project has evaluations
            ViewData["HasEvaluations"] = project.ProjectMainCriterians?.Any() ?? false;

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var project = await _context.Projects
                    .Include(p => p.ProjectMainCriterians)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (project != null)
                {
                    _context.Projects.Remove(project);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = CurrentLanguage == "ar"
                        ? "تم حذف المشروع بنجاح"
                        : "Project deleted successfully";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = CurrentLanguage == "ar"
                    ? "حدث خطأ أثناء حذف المشروع"
                    : "An error occurred while deleting the project";
            }

            return RedirectToActionWithLang(nameof(Index));
        }

        // GET: Projects/Evaluate/5
        [HttpGet]
        public async Task<IActionResult> Evaluate(int id)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.MainCriterian)
                .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.ProjectSubCriterians)
                        .ThenInclude(psc => psc.SubCriterian)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var mainCriterians = await _context.MainCriterians
                .Include(m => m.SubCriterians.OrderBy(s => s.Id))
                .OrderBy(m => m.Id)
                .AsNoTracking()
                .ToListAsync();

            // Create a view model to pass both project and criterians
            ViewBag.MainCriterians = mainCriterians;
            ViewBag.Project = project;

            return View(project);
        }

        // POST: Projects/SaveEvaluation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEvaluation(
            int projectId,
            int[] selectedMainCriterians,
            int[] subCriterianIds,
            int[] mainEvaluations,
            int[] subEvaluations)
        {
            try
            {
                var project = await _context.Projects
                    .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.ProjectSubCriterians)
                    .FirstOrDefaultAsync(p => p.Id == projectId);

                if (project == null)
                {
                    return NotFound();
                }

                // Remove ALL existing evaluations for this project
                var allExistingEvaluations = project.ProjectMainCriterians.ToList();
                _context.RemoveRange(allExistingEvaluations);

                // Only add evaluations for selected criterians
                if (selectedMainCriterians != null && selectedMainCriterians.Length > 0)
                {
                    // Process selected main criterians
                    for (int i = 0; i < selectedMainCriterians.Length; i++)
                    {
                        var mainCriterianId = selectedMainCriterians[i];
                        var mainEvaluation = mainEvaluations[i];

                        var projectMainCriterian = new ProjectMainCriterian
                        {
                            ProjectId = projectId,
                            MainCriterianId = mainCriterianId,
                            UserEvaluation = mainEvaluation,
                            ProjectSubCriterians = new List<ProjectSubCriterian>()
                        };

                        // Get all sub-criterians for this main criterian
                        var mainCriterianSubIds = await _context.SubCriterians
                            .Where(s => s.MainId == mainCriterianId)
                            .Select(s => s.Id)
                            .ToListAsync();

                        // Process sub-criterians for this main criterian
                        if (subCriterianIds != null && subEvaluations != null)
                        {
                            for (int j = 0; j < subCriterianIds.Length; j++)
                            {
                                if (mainCriterianSubIds.Contains(subCriterianIds[j]))
                                {
                                    projectMainCriterian.ProjectSubCriterians.Add(new ProjectSubCriterian
                                    {
                                        SubCriterianId = subCriterianIds[j],
                                        UserEvaluation = subEvaluations[j]
                                    });
                                }
                            }
                        }

                        _context.Add(projectMainCriterian);
                    }
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = CurrentLanguage == "ar"
                    ? "تم حفظ التقييم بنجاح"
                    : "Evaluation saved successfully";

                return RedirectToActionWithLang("Details", routeValues: new { id = projectId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = CurrentLanguage == "ar"
                    ? "حدث خطأ أثناء حفظ التقييم"
                    : "An error occurred while saving the evaluation";

                return RedirectToActionWithLang("Evaluate", routeValues: new { id = projectId });
            }
        }

        // GET: Projects/EvaluationSummary/5
        [HttpGet]
        public async Task<IActionResult> EvaluationSummary(int id)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.MainCriterian)
                .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.ProjectSubCriterians)
                        .ThenInclude(psc => psc.SubCriterian)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }


        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}