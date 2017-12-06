//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace AutoBill.Controllers
//{
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//    [Route("api/[controller]")]
//    public class GreetingController : Controller
//    {
//        [HttpGet]
//        public string Get()
//        {
//            return $"Hello {User.FindFirst(ClaimTypes.NameIdentifier).Value}";
//        }
//    }
//}