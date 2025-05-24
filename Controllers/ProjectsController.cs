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

            // Add debug information for troubleshooting (optional - remove in production)
            if (HttpContext.Request.Query.ContainsKey("debug"))
            {
                ViewData["ShowDebug"] = "true";

                // Log calculation details for debugging
                var debugInfo = new System.Text.StringBuilder();
                debugInfo.AppendLine($"=== PROJECT DETAILS CALCULATION DEBUG ===");
                debugInfo.AppendLine($"Project: {project.Title}");
                debugInfo.AppendLine($"Main Criteria Count: {project.ProjectMainCriterians?.Count ?? 0}");

                if (project.ProjectMainCriterians?.Any() == true)
                {
                    double manualTotal = 0;
                    foreach (var pmc in project.ProjectMainCriterians)
                    {
                        var score = pmc.CalculateScore();
                        debugInfo.AppendLine($"Main {pmc.MainCriterianId}: {score:F3}");
                        manualTotal += score;
                    }

                    debugInfo.AppendLine($"Manual Total: {manualTotal:F3}");
                    debugInfo.AppendLine($"TotalScore Property: {project.TotalScore:F3}");
                    debugInfo.AppendLine($"Match: {Math.Abs((project.TotalScore ?? 0) - manualTotal) < 0.001}");
                }

                // Log to console/debug output
                System.Diagnostics.Debug.WriteLine(debugInfo.ToString());

                // You could also pass this to the view for display
                ViewData["DebugInfo"] = debugInfo.ToString();
            }

            return View(project);
        }

        // Add a specific debug endpoint for detailed calculation verification
        [HttpGet]
        public async Task<IActionResult> DebugProjectCalculation(int id)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.MainCriterian)
                .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.ProjectSubCriterians)
                        .ThenInclude(psc => psc.SubCriterian)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound("Project not found");

            var debugInfo = new
            {
                ProjectId = project.Id,
                ProjectTitle = project.Title,
                MainCriteriaCount = project.ProjectMainCriterians?.Count ?? 0,

                // Current TotalScore property value
                CurrentTotalScore = project.TotalScore,

                // Manual calculation
                ManualCalculation = project.ProjectMainCriterians?.Select(pmc => new
                {
                    MainCriterionId = pmc.MainCriterianId,
                    MainCriterionName = pmc.MainCriterian.Text_En,
                    MainWeight = pmc.MainCriterian.Weight,
                    MainEvaluation = pmc.UserEvaluation,
                    CalculateScoreResult = pmc.CalculateScore(),
                    SubCriteriaCount = pmc.ProjectSubCriterians?.Count ?? 0,
                    SubCriteriaDetails = pmc.ProjectSubCriterians?.Select(psc => new
                    {
                        SubCriterionName = psc.SubCriterian.Text_En,
                        SubWeight = psc.SubCriterian.Weight,
                        SubEvaluation = psc.UserEvaluation,
                        IndividualScore = (pmc.MainCriterian.Weight * pmc.UserEvaluation) * (psc.SubCriterian.Weight * psc.UserEvaluation) / 100.0
                    }).ToList()
                }).ToList(),

                // Verification
                ManualTotal = project.ProjectMainCriterians?.Sum(pmc => pmc.CalculateScore()) ?? 0,
                TotalScorePropertyValue = project.TotalScore ?? 0,
                ValuesMatch = Math.Abs((project.TotalScore ?? 0) - (project.ProjectMainCriterians?.Sum(pmc => pmc.CalculateScore()) ?? 0)) < 0.001,

                // Recommendations
                Recommendations = new List<string>
        {
            project.TotalScore == null ? "❌ TotalScore is null - no evaluations found" : "✅ TotalScore has value",
            (project.ProjectMainCriterians?.Count ?? 0) == 0 ? "❌ No main criteria found" : $"✅ Found {project.ProjectMainCriterians.Count} main criteria",
            Math.Abs((project.TotalScore ?? 0) - (project.ProjectMainCriterians?.Sum(pmc => pmc.CalculateScore()) ?? 0)) < 0.001 ?
                "✅ Calculation is correct" : "❌ Calculation mismatch - check TotalScore property"
        }
            };

            return Json(debugInfo);
        }

        // Add method to recalculate and fix any calculation issues
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecalculateScore(int id)
        {
            try
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

                // The TotalScore is calculated automatically via the property
                // No need to save anything to database as it's a computed property

                var recalculatedScore = project.TotalScore;
                var manualTotal = project.ProjectMainCriterians?.Sum(pmc => pmc.CalculateScore()) ?? 0;

                TempData["SuccessMessage"] = CurrentLanguage == "ar"
                    ? $"تم إعادة حساب النتيجة: {recalculatedScore:F3}%"
                    : $"Score recalculated: {recalculatedScore:F3}%";

                // Log for verification
                System.Diagnostics.Debug.WriteLine($"Recalculated - Property: {recalculatedScore:F3}, Manual: {manualTotal:F3}");

                return RedirectToActionWithLang(nameof(Details), routeValues: new { id = id });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = CurrentLanguage == "ar"
                    ? "حدث خطأ أثناء إعادة الحساب"
                    : "An error occurred while recalculating";

                return RedirectToActionWithLang(nameof(Details), routeValues: new { id = id });
            }
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

        // Alternative: Add a specific debug endpoint
        [HttpGet]
        public IActionResult VerifyCalculation(int id)
        {
            var project = _context.Projects
                .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.MainCriterian)
                .Include(p => p.ProjectMainCriterians)
                    .ThenInclude(pmc => pmc.ProjectSubCriterians)
                        .ThenInclude(psc => psc.SubCriterian)
                .FirstOrDefault(p => p.Id == id);

            if (project == null)
                return Json(new { error = "Project not found" });

            var result = new
            {
                ProjectTitle = project.Title,
                TotalScoreProperty = project.TotalScore,
                MainCriteriaCount = project.ProjectMainCriterians.Count,
                MainCriteriaScores = project.ProjectMainCriterians.Select(pmc => new
                {
                    MainCriterionId = pmc.MainCriterianId,
                    MainCriterionName = pmc.MainCriterian.Text_En,
                    MainWeight = pmc.MainCriterian.Weight,
                    MainEvaluation = pmc.UserEvaluation,
                    CalculatedScore = pmc.CalculateScore(),
                    SubCriteriaCount = pmc.ProjectSubCriterians.Count,
                    SubCriteriaDetails = pmc.ProjectSubCriterians.Select(psc => new
                    {
                        SubCriterionName = psc.SubCriterian.Text_En,
                        SubWeight = psc.SubCriterian.Weight,
                        SubEvaluation = psc.UserEvaluation,
                        IndividualScore = (pmc.MainCriterian.Weight * pmc.UserEvaluation) * (psc.SubCriterian.Weight * psc.UserEvaluation) / 100.0
                    }).ToList()
                }).ToList(),
                ManualCalculatedTotal = project.ProjectMainCriterians.Sum(pmc => pmc.CalculateScore())
            };

            return Json(result);
        }
    }

}