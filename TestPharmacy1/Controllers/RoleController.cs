using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;

namespace TestPharmacy1.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public RoleController(RoleManager<IdentityRole> roleManager,ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
    }
}
