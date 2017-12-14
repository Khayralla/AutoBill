using AutoBill.Email;
using AutoBill.Models;
using AutoBill.Models.AutoSaleBillViewModels;
using AutoBill.Services;
using AutoBill.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoBill.Controllers
{
    [Authorize]
    [Route("BillOfSale")]
    public class BillOfSaleController : BaseController
    {
        private readonly IBillService _billService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly INodeServices _nodeServices;
        private readonly IEmailSender _emailSender;
        private readonly IEmailConfiguration _emailConfiguration;

        public BillOfSaleController(IBillService billService, 
                                    UserManager<ApplicationUser> userManager, 
                                    ILogger<AccountController> logger, 
                                    INodeServices nodeServices, 
                                    IEmailSender emailSender, 
                                    IEmailConfiguration emailConfiguration)
        {
            _billService = billService;
            _userManager = userManager;
            _logger = logger;
            _nodeServices = nodeServices;
            _emailSender = emailSender;
            _emailConfiguration = emailConfiguration;
        }

        [Route("")] // Combines to define the route template "Home"
        [Route("Index")] // Combines to define the route template "Home/Index"
        //[Route("/")] // Does not combine, defines the route template ""
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return Challenge();

            var saleBill = new SaleBillViewModel
            {
                Makes = await _billService.GetMakesAsync(),
                Car = new CarViewModel(),
                BodyTypes = await _billService.GetBodyTypesAsync()
            };

            saleBill.Makes.Insert(0, new SelectListItem { Value = "0", Text = "Select" });

            return View(saleBill);
        }

        [HttpGet("{makeId}")]
        [Route("ModelsList")]
        public async Task<JsonResult> ModelsList([FromQuery]int makeId)
        {
            var models = await _billService.GetModelsAsync(makeId);
            models.Insert(0, new Model { ModelId = 0, ModelName = "Select" });
            return Json(new SelectList(models,nameof(Model.ModelId), nameof(Model.ModelName)));
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SaleBillViewModel saleBillViewModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return Challenge();

            //// ------- Validation ------- //
            if (saleBillViewModel.Car.MakeId == 0)
                ModelState.AddModelError("Mark", "Select Mark");

            if (saleBillViewModel.Car.ModelId == 0)
                ModelState.AddModelError("Model", "Select Model");

            if (saleBillViewModel.Car.BodyTypeId == 0)
                ModelState.AddModelError("Body Type", "Select Body Type");

            if (ModelState.IsValid)
            {
                try
                {
                    var sb = await _billService.GetSaleBillAsync(saleBillViewModel.Customer.FirstName, saleBillViewModel.Customer.LastName, saleBillViewModel.Car.VIN, saleBillViewModel.Insurance.InsuranceName, currentUser);
                    if (sb == null)
                    {
                        var id = await _billService.SaveSaleBillAsync(saleBillViewModel, currentUser);
                        if (id != -1)
                            return RedirectToAction(actionName: nameof(CreatePdf), routeValues: new { id = id });
                    }
                    else
                        return RedirectToAction(actionName: nameof(CreatePdf), routeValues: new { id = sb.Id });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            else
            {
                var errors1 = ModelState.Where(x => x.Value.Errors.Count > 0)
                                        .Select(x => new { x.Key, x.Value.Errors })
                                        .ToArray();
                var errors2 = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .ToArray();
            }
            // If we got this far, something failed, redisplay form
            return View(saleBillViewModel);
        }

        [HttpGet("{id}")]
        [Route("CreatePdf")]
        public async Task<IActionResult> CreatePdf([FromQuery] int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return Challenge();

            var sb = await _billService.GetSaleBillAsync(id, currentUser);
            if (sb == null)
                return BadRequest(new { error = "Could not find document." });

            var car = await _billService.GetCarByVinAsync(sb.CarVin);
            var carTradeWith = await _billService.GetCarByVinAsync(sb.CarTradeWithVin);
            var customer = await _billService.GetCustomerAsync(sb.CustomerId);
            var insurance = await _billService.GetInsuranceAsync(sb.InsuranceId);

            var html = Helper.GetHtml(customer, car, carTradeWith, insurance, sb);
            var result = await _nodeServices.InvokeAsync<byte[]>("./pdf", html);

            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.Headers.Add("x-filename", "report.pdf");
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "x-filename");
            HttpContext.Response.Body.Write(result, 0, result.Length);

            return new ContentResult();
        }

        public async Task SendEmail()
        {
            try
            {
                var fromEmailAddress = new List<EmailAddress>
                {
                    new EmailAddress{Address = "khayralla@gmail.com", Name = "khayralla_Test1"}
                };

                var toEmailAddress = new List<EmailAddress>
                {
                    new EmailAddress {Address = "khayralla_m@yahoo.com", Name = "khayralla_Test2"}
                };

                var emailMessage = new EmailMessage
                {
                    Content = "Contest is string",
                    FromAddresses = fromEmailAddress,
                    Subject = "Subject go here",
                    ToAddresses = toEmailAddress
                };

                await _emailSender.SendAsync(_emailConfiguration, emailMessage);
            }
            catch (Exception)
            {

            }
        }

        [HttpGet("{vin}")]
        [Route("GetCarByVIN")]
        public async Task<JsonResult> GetCarByVIN([FromQuery]string vin)
        {
            CarViewModel carViewModel = null;
            var car = await _billService.GetCarByVinAsync(vin);
            if (car != null)
            {
                carViewModel = new CarViewModel
                {
                    BodyTypeId = car.BodyTypeId,
                    Color = car.Color,
                    Kilometres = car.Kilometres,
                    MakeId = car.MakeId,
                    ModelId = car.ModelId,
                    Odometer = car.Odometer,
                    VIN = car.VIN,
                    Year = car.Year
                };

                carViewModel.Models = await _billService.GetSelectListModelsAsync(carViewModel.MakeId);
            }

            var temp = Json(carViewModel);
            return temp;
        }

    }
}