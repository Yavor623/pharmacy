using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics.Contracts;
using TestPharmacy1.Data;
using TestPharmacy1.Models;

namespace TestPharmacy1.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ContactsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {         
            var contacts = _context.Information.ToList();
            if (contacts.Count!=0)
            {
                var newContacts = _context.Information.First();
                return View(newContacts);
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit()
        {
            if (_context.Information.Count() != 0)
            {
                var information = _context.Information.First();
                var model = new EditInformationViewModel
                {
                    Id = information.Id,
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Information.Count() != 0)
                {
                    var information = _context.Information.First();
                    information.Phone = model.Phone;
                    information.Email = model.Email;
                    _context.Information.Update(information);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    var information = _context.Information.First();
                    information.Phone = model.Phone;
                    information.Email = model.Email;
                    _context.Information.Add(information);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }
    }
}
