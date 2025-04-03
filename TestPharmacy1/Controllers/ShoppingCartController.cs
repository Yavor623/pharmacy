using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TestPharmacy1.Models;

namespace TestPharmacy1.Controllers
{
    public class ShoppingCartController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ShoppingCartController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var ownedMedication = _context.OwnedMedication.Include(a => a.Medication).Include(a => a.User).ToList();
            var filteredOwnedMedication =
                from med in ownedMedication
                where med.UserId == _signInManager.UserManager.GetUserId(User)
                select med;
            return View(filteredOwnedMedication);
        }
    }
}
