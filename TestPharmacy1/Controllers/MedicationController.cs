using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestPharmacy1.Models.Medications;
using TestPharmacy1.Models;

namespace TestPharmacy1.Controllers
{
    public class MedicationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Image image;
        private readonly IHostEnvironment _hostEnvironment;
        public MedicationController(ApplicationDbContext context,IHostEnvironment hostEnvironment,Image image)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            this.image = image;
        }
        public IActionResult Index()
        {
            var medications = _context.Medication.Include(o => o.TypeOfMedication).Include(o => o.ConsistencyOfMedication).Include(o => o.Image).ToList();
            return View(medications);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _hostEnvironment.ContentRootPath;
                string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                string extension = Path.GetExtension(image.ImageFile.FileName);
                image.Name = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRoot + "/Image/", fileName);
                using (var fileStream = new FileStream(path,FileMode.Create))
                {
                    await image.ImageFile.CopyToAsync(fileStream);
                }
                var medication = new Medication
                {

                };
                _context.Image.Add(image);

                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
