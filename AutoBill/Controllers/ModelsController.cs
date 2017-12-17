using AutoBill.Data;
using AutoBill.Models;
using AutoBill.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoBill.Controllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class ModelsController : Controller
    {
        private readonly IBillService _billService;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger _logger;

        public ModelsController(IBillService billService, UserManager<ApplicationUser> userManager)//, ILogger logger)
        {
            _billService = billService;
            _userManager = userManager;
            //  _logger = logger;
        }

        // GET: Models
        public async Task<IActionResult> Index()
        {
            return View(await _billService.GetModelsAsync());
        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !id.HasValue)
                return NotFound();

            var model = await _billService.GetModelAsync(id.Value);
            if (model == null)
                return NotFound();

            return View(model);
        }

        // GET: Models/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelId,MakeId,ModelName")] Model model)
        {
            if (ModelState.IsValid)
            {
                await _billService.AddModelAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue)
                return NotFound();

            var model = await _billService.GetModelAsync(id.Value);
            if (model == null)
                return NotFound();

            return View(model);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelId,MakeId,ModelName")] Model model)
        {
            if (model == null || id != model.ModelId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _billService.UpdateModelAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ModelExists(model.ModelId))
                        throw;
                    else
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !id.HasValue)
                return NotFound();

            var model = await _billService.GetModelAsync(id.Value);
            if (model == null)
                return NotFound();

            return View(model);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _billService.DeleteModelAsync(id);
            if (deleted)
                return RedirectToAction(nameof(Index));

            return View();
        }

        private async Task<bool> ModelExists(int id)
        {
            return await _billService.ModelExists(id);
        }
    }
}
