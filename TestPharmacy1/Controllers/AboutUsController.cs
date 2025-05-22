using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;
using TestPharmacy1.Models;
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
            var aboutUs = _context.Information.ToList();
            if (aboutUs.Count != 0)
            {
                var newAboutUs = _context.Information.First();
                return View(newAboutUs);
            }
            ViewBag.IsThereAny = false;
            return View();
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Edit()
        {
            if (_context.Information.Count() != 0)
            {
                var information = _context.Information.First();
                var model = new EditInformationViewModel
                {
                    Id = information.Id,
                    AboutUs = information.AboutUs,
                    Phone = information.Phone,
                    Email = information.Email
                };
                return View(model);
            }
            else
            {
                return View();
            }
            return View();
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Information.Count() != 0)
                {
                    var information = _context.Information.First();
                    information.AboutUs = model.AboutUs;
                    information.Email = model.Email;
                    information.Phone = model.Phone;
                    _context.Information.Update(information);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    var information = _context.Information.First();
                    information.AboutUs = model.AboutUs;
                    information.Email = "";
                    information.Phone = "";
                    _context.Information.Add(information);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }

    }
}
