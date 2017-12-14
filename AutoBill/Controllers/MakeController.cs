using AutoBill.Models;
using AutoBill.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoBill.Controllers.MakeControllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class MakeController : Controller
    {
        private readonly IBillService _billService;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger _logger;

        public MakeController(IBillService billService, UserManager<ApplicationUser> userManager)//, ILogger logger)
        {
            _billService = billService;
            _userManager = userManager;
            //  _logger = logger;
        }

        // GET: Makes
        public async Task<IActionResult> Index()
        {
            return View(await _billService.GetMakesListAsync());
        }

        // GET: Makes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !id.HasValue)
                return NotFound();

            var make = await _billService.GetMakeAsync(id.Value);
            if (make == null)
            {
                return NotFound();
            }

            return View(make);
        }

        // GET: Makes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Makes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MakeId,MakeName")] Make make)
        {
            if (ModelState.IsValid)
            {
                make = await _billService.SaveMakeAsync(make.MakeName);
                return RedirectToAction(nameof(Index));
            }
            return View(make);
        }

        // GET: Makes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var make = await _billService.GetMakeAsync(id.Value);
            if (make == null)
                return NotFound();
            
            return View(make);
        }

        // POST: Makes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MakeId,MakeName")] Make make)
        {
            if (id != make.MakeId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _billService.UpdateMakeAsync(make);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await MakeExists(make.MakeId))
                        throw;
                    else
                        return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(make);
        }

        // GET: Makes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !id.HasValue)
                return NotFound();

            var make = await _billService.GetMakeAsync(id.Value);
            if (make == null)
                return NotFound();

            return View(make);
        }

        // POST: Makes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(int id)
        {
            if (await _billService.DeleteMakeAsync(id))
                return RedirectToAction(nameof(Index));

            return BadRequest(new { error = "Could not delete make" });
        }

        private async Task<bool> MakeExists(int id)
        {
            return await _billService.MakeExists(id);
        }
    }
}
