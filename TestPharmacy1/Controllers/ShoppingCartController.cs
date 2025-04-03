using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestPharmacy1.Controllers
{
    public class ShoppingCartController : Controller
    {
        public readonly ApplicationDbContext _context; 
        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var ownedMedication = _context.OwnedMedication.Include(a => a.Medication).Include(a => a.User).ToList();
            return View();
        }
    }
}
