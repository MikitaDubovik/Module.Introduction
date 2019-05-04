using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Introduction.Services;

namespace Module.Introduction.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private readonly IAdministratorService _administratorService;

        public AdministrationController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _administratorService.GetUsers();

            return View(users);
        }
    }
}