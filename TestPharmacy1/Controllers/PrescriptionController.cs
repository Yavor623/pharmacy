using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;
using TestPharmacy1.Models;
using TestPharmacy1.Models.Precsription;

namespace TestPharmacy1.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PrescriptionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var prescripions = _context.Prescription.ToList();
            var userPrescriptions =
                from prescription in prescripions
                where prescription.UserId == _userManager.GetUserId(User)
                select prescription;
            return View(userPrescriptions);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prescription = new Prescription()
                {
                    Medications = model.Medications,
                    UserId = model.UserId,
                    PrescribedDate = model.PrescribedDate,
                    Description = model.Description
                };
                await _context.Prescription.AddAsync(prescription);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
