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

namespace TestPharmacy1.Controllers
{
    public class MedicationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MedicationController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var medications = _context.Medication.Include(o => o.TypeOfMedication).Include(o => o.ConsistencyOfMedication).ToList();
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
    }
}
