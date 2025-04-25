using Microsoft.AspNetCore.Mvc;

namespace TestPharmacy1.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
