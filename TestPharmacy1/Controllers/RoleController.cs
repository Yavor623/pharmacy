using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using TestPharmacy1.Data;
using TestPharmacy1.Models;
using TestPharmacy1.Models.Roles;

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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Id = model.Id,
                    Name = model.Name,
                    NormalizedName = model.Name.ToUpper()
                };
				_context.Roles.Add(role);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
        [HttpGet]
        public IActionResult Edit(string id) 
        {
            var role = _context.Roles.FirstOrDefault(a => a.Id == id);
            var model = new EditRoleViewModel 
            {
                Id = role.Id,
                Name = role.Name,
                Role = role
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _context.Roles.FirstOrDefault(a => a.Id == id);
                role.Id = model.Id;
                role.Name = model.Name;
                role.NormalizedName = model.Name.ToUpper();
                _context.Roles.Update(role);
                _context.SaveChanges();
                return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<IActionResult> Delete(string confirmed_value, string id)
		{
			if (confirmed_value == "Yes")
			{
				var role = _context.Roles.Find(id);
				_context.Roles.Remove(role);
				_context.SaveChanges();
			}
			else
			{
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
	}
}
