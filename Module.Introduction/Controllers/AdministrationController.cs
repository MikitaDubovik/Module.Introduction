using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Introduction.Services;

namespace Module.Introduction.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService _administrationService;

        public AdministrationController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _administrationService.GetUsersAsync();

            return View(users);
        }
    }
}