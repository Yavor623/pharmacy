using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics.Contracts;
using TestPharmacy1.Data;
using TestPharmacy1.Models;
using TestPharmacy1.Models.Contacts;

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
            var contacts = _context.Contacts.ToList();
            if (contacts.Count != 0)
            {
                var newContacts = _context.Contacts.First();
                return View(newContacts);
            }
            ViewBag.IsThereAny = false;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit()
        {
            if (_context.Contacts.Count() != 0)
            {
                var information = _context.Contacts.First();
                var model = new EditContactsViewModel
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
        public async Task<IActionResult> Edit(EditContactsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Contacts.Count() != 0)
                {
                    var information = _context.Contacts.First();
                    information.Phone = model.Phone;
                    information.Email = model.Email;
                    _context.Contacts.Update(information);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    var info = new Contact
                    {
                        Email = model.Email,
                        Phone = model.Phone,
                    };
                    _context.Contacts.Add(info);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }
    }
}
