using Microsoft.AspNetCore.Mvc;

namespace Module.Introduction.Controllers
{
    public class CustomErrorController : Controller
    {
        //Test
        public IActionResult Index()
        {
            return View();
        }
    }
}