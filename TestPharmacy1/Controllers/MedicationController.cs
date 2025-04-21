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
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TestPharmacy1.Controllers
{
    public class MedicationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private string viewBagMessage = "You need prescription for this medication!";
        public MedicationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(string searchString,string selectedOption,int id,int submitValue,bool isItChecked)
        {
            ViewData["TypeOfMedication"] = _context.TypeOfMedication.Select(a => a.Name);
            ViewData["ConsistencyOfMedicationId"] = new SelectList(_context.ConsistencyOfMedication, "Id", "Name");
            ViewBag.AmountOfItems = isItChecked == true? submitValue:8;
            ViewBag.CurrentPage = id;
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
        public void CheckIfItHasMed(IEnumerable<int> fileteredOwnedMedications,int medId,List<OwnedMedication> ownedMedications,int amount,string userId)
        {
            if (fileteredOwnedMedications.Contains(medId))
            {
                IEnumerable<OwnedMedication> change =
                from med in ownedMedications
                    where med.MedicationId == medId
                    select med;
                var changedElement = change.FirstOrDefault();
                changedElement.Amount += amount;
                _context.OwnedMedication.Update(changedElement);
                _context.SaveChanges();
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
            }
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

			var ownedMedications = _context.OwnedMedication.ToList();
            var medications = _context.Medication.ToList();
            var prescriptions = _context.Prescription.ToList();
            var fileteredOwnedMedications =
                from med in ownedMedications
                where med.UserId == userId
                select med.MedicationId;
            var filteredMedications =
                from med in medications
                where med.Id == medId
                select med;
            var filteredPrescription =
                from med in prescriptions
                where med.UserId == userId
                select med.Medications;
            if (!String.IsNullOrEmpty(userId))
            {
                if ((from c in filteredMedications select c.IsPrescriptionNeeded).First())
                {
                    foreach (var i in filteredPrescription)
                    {
                        if(i.Contains((from c in filteredMedications select c.Name).First()))
                        {
							CheckIfItHasMed(fileteredOwnedMedications, medId, ownedMedications, amount, userId);
							return RedirectToAction("Index");
						}
                        else
                        {
                            return RedirectToAction("Details", "Medication", new { id = medId , prescriptionMessage = viewBagMessage});
						}
                    }
                }
                else
                {
                    CheckIfItHasMed(fileteredOwnedMedications, medId, ownedMedications, amount, userId);
                    return RedirectToAction("Index");
                }
            }
            else
            {
				return RedirectToAction("Login", "Account", new object { });
			}
            return RedirectToAction("Details","Medication",new { id=medId , prescriptionMessage = ""});
		}
        [HttpGet]
        public  IActionResult Edit(int id)
        {
            var medication = _context.Medication.FirstOrDefault(a => a.Id == id);
            var model = new EditMedicationViewModel
            {
                Name = medication.Name,
                Manufacturer = medication.Manufacturer,
                HowToUse = medication.HowToUse,
                IsPrescriptionNeeded = medication.IsPrescriptionNeeded,
                ConsistencyOfMedicationId = medication.ConsistencyOfMedicationId,
                TypeOfMedicationId = medication.TypeOfMedicationId,
                Amount = medication.Amount,
                Description = medication.Description,
                ByteImage = medication.Image,
                Price = medication.Price,
                CurrentMedication = medication
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditMedicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var medication = _context.Medication.Find(id);
                medication.Name = model.Name;
                medication.HowToUse = model.HowToUse;
                medication.Price = model.Price;
                medication.Manufacturer = model.Manufacturer;
                medication.Amount = model.Amount;
                medication.Description = model.Description;
                medication.IsPrescriptionNeeded = model.IsPrescriptionNeeded;
                medication.ConsistencyOfMedicationId = model.ConsistencyOfMedicationId;
                medication.TypeOfMedicationId = model.TypeOfMedicationId;
                if (model.ImageFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await model.ImageFile.CopyToAsync(ms);
                        medication.Image = ms.ToArray();
                    }
                }
                else
                {
                    medication.Image = model.ByteImage;
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string confirmed_value, int id)
        {
            if (confirmed_value == "Yes")
            {
                var med = _context.Medication.Find(id);
                var ownedMed =
                    from ownMed in _context.OwnedMedication
                    where ownMed.MedicationId == id
                    select ownMed;
                ownedMed.ForEachAsync(a => _context.OwnedMedication.Remove(a));
                _context.Medication.Remove(med);
                _context.SaveChanges();
            }
            else
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id,string prescriptionMessage)
        {
            ViewBag.Prescription = prescriptionMessage;
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
