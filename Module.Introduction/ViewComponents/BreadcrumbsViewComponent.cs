using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TestsProject
{
    //[ViewComponent(Name = "Breadcrumbs")]
    public class BreadcrumbsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(HttpContext context)
        {
            string currentPath = "Home" +  context.Request.Path.Value.Replace('/','>'); //context.Request.Path.Value;
            return View("Default", currentPath);
        }
    }
}
