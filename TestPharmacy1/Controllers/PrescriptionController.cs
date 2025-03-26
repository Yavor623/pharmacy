using Microsoft.AspNetCore.Mvc;

namespace TestPharmacy1.Controllers
{
    public class PrescriptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
