using Microsoft.AspNetCore.Mvc;
using ProiectIS.Models;
namespace ProiectIS.Controllers
{
    public class ModeratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
