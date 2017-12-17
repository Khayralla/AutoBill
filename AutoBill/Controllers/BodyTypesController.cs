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
    public class BodyTypesController : Controller
    {
        private readonly IBillService _billService;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger _logger;


        public BodyTypesController(IBillService billService, UserManager<ApplicationUser> userManager)//, ILogger logger)
        {
            _billService = billService;
            _userManager = userManager;
            //  _logger = logger;
        }

        // GET: BodyTypes
        public async Task<IActionResult> Index()
        {
            return View(await _billService.GetBodyTypesListAsync());
        }

        // GET: BodyTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var bodyType = await _billService.GetBodyTypeAsync(id.Value);
            if (bodyType == null)
                return NotFound();

            return View(bodyType);
        }

        // GET: BodyTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BodyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BodyTypeId,BodyTypeName")] BodyType bodyType)
        {
            if (ModelState.IsValid)
            {
                if (await _billService.AddBodyTypeAsync(bodyType))
                    return RedirectToAction(nameof(Index));
            }
            return View(bodyType);
        }

        // GET: BodyTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var bodyType = await _billService.GetBodyTypeAsync(id.Value);
            if (bodyType == null)
                return NotFound();

            return View(bodyType);
        }

        // POST: BodyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BodyTypeId,BodyTypeName")] BodyType bodyType)
        {
            if (id != bodyType.BodyTypeId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _billService.UpdateBodyTypeAsync(bodyType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _billService.BodyTypeExistsAsync(bodyType.BodyTypeId))
                        throw;
                    else
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bodyType);
        }

        // GET: BodyTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var bodyType = await _billService.GetBodyTypeAsync(id.Value);
            if (bodyType == null)
                return NotFound();

            return View(bodyType);
        }

        // POST: BodyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _billService.DeleteBodyTypeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
