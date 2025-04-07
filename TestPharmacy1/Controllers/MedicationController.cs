using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestPharmacy1.Models.Medications;
using TestPharmacy1.Models;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Drawing;
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
                    HowToUse = model.HowToUse,
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
        public  async Task<IActionResult> AddToCart(int medId,string userId,int amount)
        {
            var look = _context.OwnedMedication.ToList();
            var filteredLook =
                from med in look
                where med.UserId == userId
                select med.MedicationId;
            if (!String.IsNullOrEmpty(userId))
            {
                if (filteredLook.Contains(medId))
                {
                    IEnumerable<OwnedMedication> change =
                        from med in look
                        where med.MedicationId == medId
                        select med;
                    var changedElement = change.FirstOrDefault();
                    changedElement.Amount += amount;
                    _context.OwnedMedication.Update(changedElement);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    OwnedMedication ownedMedication = new OwnedMedication()
                    {
                        MedicationId = medId,
                        UserId = userId,
                        Amount = amount
                    };
                    _context.OwnedMedication.Add(ownedMedication);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Login","Account",new object { });
        }
        [HttpGet]
        public  IActionResult Edit(int id)
        {
            //var medication = _context.Medication.FirstOrDefault(a => a.Id == id);
            //var model = new EditMedicationViewModel
            //{
            //    Name = medication.Name,
            //    Manufacturer = medication.Manufacturer,
            //    HowToUse = medication.HowToUse,
            //    IsPrescriptionNeeded = medication.IsPrescriptionNeeded,
            //    ConsistencyOfMedicationId = medication.ConsistencyOfMedicationId,
            //    TypeOfMedicationId = medication.TypeOfMedicationId,
            //    Amount = medication.Amount,
            //    Description = medication.Description,
            //    CurrentMedication = medication
            //};
            //if (medication.Image != null)
            //{
            //    using (var ms = new MemoryStream(medication.Image))
            //    {
            //        System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            //        //model.ImageFile = image;
            //    }
            //}
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditMedicationViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var medication = _context.Medication.Find(id);
            //    medication.Name = model.Name;
            //    medication.HowToUse = model.HowToUse;
            //    medication.Manufacturer = model.Manufacturer;
            //    medication.IsPrescriptionNeeded = model.IsPrescriptionNeeded;
            //    medication.ConsistencyOfMedicationId = model.ConsistencyOfMedicationId;
            //    medication.TypeOfMedicationId = model.TypeOfMedicationId;
            //}
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var med = _context.Medication.Find(id);
            var ownedMed =
                from ownMed in _context.OwnedMedication
                where ownMed.MedicationId == id
                select ownMed;
            ownedMed.ForEachAsync(a => _context.OwnedMedication.Remove(a));
            _context.Medication.Remove(med);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var currentMed = _context.Medication.Find(id);
            var medication = new DetailsMedicationViewModel
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
