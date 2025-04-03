using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestPharmacy1.Models.Medications;
using TestPharmacy1.Models;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Formats.Tar;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace TestPharmacy1.Controllers
{
    public class MedicationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MedicationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(string searchString,string selectedOption)
        {
            var medications = _context.Medication.Include(o => o.TypeOfMedication).Include(o => o.ConsistencyOfMedication).ToList();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                var queryLowNums =
                 from med in medications
                 where med.Name.ToLower().Contains(searchString.ToLower())
                 select med;
                return View(queryLowNums);
            }
            if (!String.IsNullOrEmpty(selectedOption))
            {
                switch (selectedOption) 
                {
                    case "Price":
                        var queryPrice = medications.OrderByDescending(o => o.Price);
                        return View(queryPrice);
                        break;
                    case "Name":
                        var queryName = medications.OrderBy(o => o.Name);
                        return View(queryName);
                        break;
                     default : return View(medications);

                }

            }
            return View(medications);
        }
        public IActionResult FilteredIndex(List<Medication>medications)
        {
            return View(medications);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["TypeOfMedicationId"] = new SelectList(_context.TypeOfMedication, "Id", "Name");
            ViewData["ConsistencyOfMedicationId"] = new SelectList(_context.ConsistencyOfMedication, "Id", "Name");
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var medication = new Medication
                {
                    Name = model.Name,
                    Manufacturer = model.Manufacturer,
                    ExpirationDate = model.ExpirationDate,
                    IsPrescriptionNeeded = model.IsPrescriptionNeeded,
                    Amount = model.Amount,
                    Description = model.Description,
                    TypeOfMedicationId = model.TypeOfMedicationId,
                    ConsistencyOfMedicationId = model.ConsistencyOfMedicationId,
                    Price = model.Price
                };
                if (model.ImageFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await model.ImageFile.CopyToAsync(ms);
                        medication.Image = ms.ToArray();
                    }
                }
                    _context.Medication.Add(medication);
                    _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypesOfMedication = new SelectList(_context.TypeOfMedication, "Id", "Name",model.TypeOfMedicationId);
            ViewBag.ConsistencyOfMedication = new SelectList(_context.ConsistencyOfMedication, "Id", "Name", model.ConsistencyOfMedicationId);
            return View(model);
        }
        [HttpPost]
        public  async Task<IActionResult> AddToCart(int medId,string userId)
        {
            var look = _context.OwnedMedication.ToList();
            var filteredLook =
                from med in look
                where med.UserId == userId
                select med.MedicationId;
            if (!String.IsNullOrEmpty(userId))
            {
                if (!filteredLook.Contains(medId))
                {

                    OwnedMedication ownedMedication = new OwnedMedication()
                    {
                        MedicationId = medId,
                        UserId = userId
                    };
                    _context.OwnedMedication.Add(ownedMedication);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Login","Account",new object { });
        }

    }
}
