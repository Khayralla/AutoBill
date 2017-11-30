using Microsoft.AspNetCore.Mvc;

namespace AutoBill.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            //https://www.codeproject.com/Tips/1217662/What-Controller-Is-This-View-For
            ViewBag.controllerName = GetType().Name.Replace("Controller", string.Empty);
        }
    }
}