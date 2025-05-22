using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TestPharmacy1.Data;
using TestPharmacy1.Models;
using TestPharmacy1.Models.Information;
using TestPharmacy1.Models.Precsription;

namespace TestPharmacy1.Controllers
{
    public class AboutUs : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AboutUs(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var aboutUs = _context.AboutUs.ToList();
            if (aboutUs.Count != 0)
            {
                var newAboutUs = _context.AboutUs.First();
                return View(newAboutUs);
            }
            ViewBag.IsThereAny = false;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit()
        {
            if (_context.AboutUs.Count() != 0)
            {
                var information = _context.AboutUs.First();
                var model = new EditAboutUsViewModel
                {
                    Id = information.Id,
                    Info = information.Info
                };
                return View(model);
            }
            else
            {
                return View();
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAboutUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.AboutUs.Count() != 0)
                {
                    var information = _context.AboutUs.FirstOrDefault();
                    information.Info = model.Info;
                    _context.AboutUs.Update(information);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    var info = new Models.AboutUs
                    {
                        Info = model.Info
                    };
                    _context.AboutUs.Add(info);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }

    }
}
