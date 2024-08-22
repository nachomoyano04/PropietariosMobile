using Microsoft.AspNetCore.Mvc;

namespace InmoEscudero.Controllers
{
    public class InquilinoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}