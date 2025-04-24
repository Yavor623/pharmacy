using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TestPharmacy1.Models;
using TestPharmacy1.Models.ShoppingCart;

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
        public async Task<IActionResult> Delete(string confirmed_value, int id)
        {
            if (confirmed_value == "Yes")
            {
                var user = _signInManager.UserManager.GetUserId(User);
                var userMed = _context.OwnedMedication.Where(a=>a.UserId == user);
                var med = userMed.Where(a => a.Id == id);
                med.ForEachAsync(a => _context.OwnedMedication.Remove(a));
                _context.SaveChanges();
            }
            else
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id, string prescriptionMessage)
        {
            ViewBag.Prescription = prescriptionMessage;
            var currentMed = _context.Medication.Find(id);
            var medication = new ShoppingCartDetailsViewModel
            {
                Id = id,
                Name = currentMed.Name,
                Manufacturer = currentMed.Manufacturer,
                HowToUse = currentMed.HowToUse,
                IsPrescriptionNeeded = currentMed.IsPrescriptionNeeded,
                Amount = currentMed.Amount,
                Description = currentMed.Description,
                TypeOfMedicationId = currentMed.TypeOfMedicationId,
                ConsistencyOfMedicationId = currentMed.ConsistencyOfMedicationId,
                Price = currentMed.Price,
                Image = currentMed.Image
            };
            return View(medication);
        }
    }
}
