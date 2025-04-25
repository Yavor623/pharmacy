using Microsoft.AspNetCore.Mvc;

namespace TestPharmacy1.Controllers
{
    public class AboutUs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
