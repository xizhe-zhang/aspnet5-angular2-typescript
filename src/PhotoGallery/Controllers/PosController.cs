using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoGallery.Controllers
{
    public class PosController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(int? id=1)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
