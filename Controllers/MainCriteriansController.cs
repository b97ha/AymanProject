using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AymanProject.Models;

namespace AymanProject.Controllers
{
    public class MainCriteriansController : Controller
    {
        private readonly EvaluationContext _context;

        public MainCriteriansController(EvaluationContext context)
        {
            _context = context;
        }

        // GET: MainCriterians
        public async Task<IActionResult> Index()
        {
            //using (var context = new EvaluationContext())
            //{
            //    DatabaseInitializer.Initialize(context);
            //    Console.WriteLine("Database created and seeded successfully.");
            //}
            return View(await _context.MainCriterians.ToListAsync());
        }

        // GET: MainCriterians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainCriterian = await _context.MainCriterians
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text_Ar,Text_En,Weight")] MainCriterian mainCriterian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainCriterian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text_Ar,Text_En,Weight")] MainCriterian mainCriterian)
        {
            if (id != mainCriterian.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainCriterian);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainCriterian == null)
            {
                return NotFound();
            }

            return View(mainCriterian);
        }

        // POST: MainCriterians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainCriterian = await _context.MainCriterians.FindAsync(id);
            if (mainCriterian != null)
            {
                _context.MainCriterians.Remove(mainCriterian);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MainCriterianExists(int id)
        {
            return _context.MainCriterians.Any(e => e.Id == id);
        }
    }
}
