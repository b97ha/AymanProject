using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AymanProject.Models;

namespace AymanProject.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly EvaluationContext _context;

        public ProjectsController(EvaluationContext context)
        {
            _context = context;
        }

        // GET: Project
        public async Task<IActionResult> Index(string lang = "en")
        {
            ViewData["Lang"] = lang;

            var projects = await _context.Projects
                                        .Include(p => p.ProjectMainCriterians)
                                        .ToListAsync();

            return View(projects);
        }


        // GET: SubCriterians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Project/Create
        public IActionResult Create(string lang = "en")
        {
            ViewData["Lang"] = lang;
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Location,Description,SubmittedOn,EndOn")] Project project)
        {
            // Clear any validation errors for navigation properties
            ModelState.Remove("ProjectMainCriterians");
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }





        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int? id, string lang = "en")
        {
            ViewData["Lang"] = lang;
            if (id == null) return NotFound();

            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Location,Description,SubmittedOn,EndOn")] Project project)
        {
            if (id != project.Id) return NotFound();
            ModelState.Remove("ProjectMainCriterians");

            if (ModelState.IsValid)
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id, string lang = "en")
        {
            ViewData["Lang"] = lang;
            if (id == null) return NotFound();

            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Evaluate(int id, string lang = "en")
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
                .Include(m => m.SubCriterians)
                .ToListAsync();

            // Create a view model to pass both project and criterians
            ViewBag.MainCriterians = mainCriterians;
            ViewBag.Project = project;
            ViewData["Lang"] = lang;

            return View(project);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEvaluation(
            int projectId,
            string lang,
            int[] selectedMainCriterians,
            int[] subCriterianIds,
            int[] mainEvaluations,
            int[] subEvaluations)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectMainCriterians)
                .ThenInclude(pmc => pmc.ProjectSubCriterians)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            // Clear existing evaluations if needed
            var existingEvaluations = project.ProjectMainCriterians
                .Where(pmc => selectedMainCriterians.Contains(pmc.MainCriterianId))
                .ToList();

            _context.RemoveRange(existingEvaluations);

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

                _context.Add(projectMainCriterian);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = projectId, lang });
        }

        [HttpGet]
        public async Task<IActionResult> EvaluationSummary(int id, string lang = "en")
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

            ViewData["Lang"] = lang;
            return View(project);
        }
    }

}