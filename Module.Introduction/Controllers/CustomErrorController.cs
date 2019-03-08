using Microsoft.AspNetCore.Mvc;

namespace Module.Introduction.Controllers
{
    public class CustomErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}