using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;
using TestPharmacy1.Models;
using TestPharmacy1.Models.Accounts;
using TestPharmacy1.Models.Precsription;
using TestPharmacy1.Models.Precsriptions;

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
        [Authorize]
        public IActionResult Index()
        {
            var prescripions = _context.Prescription.ToList();
            var userPrescriptions =
                from prescription in prescripions
                where prescription.UserId == _userManager.GetUserId(User)
                select prescription;
            return View(userPrescriptions);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [Authorize]
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
        [Authorize]
        [HttpGet]
		public IActionResult Edit(int id)
		{
			var userPrescriptions = _context.Prescription.Where(a => a.UserId == _userManager.GetUserId(User));
			var prescription = userPrescriptions.FirstOrDefault(a => a.PrescriptionId == id);
			var model = new EditPrescriptionViewModel
			{
				PrescriptionId = prescription.PrescriptionId,
				UserId = prescription.UserId,
				Medications = prescription.Medications,
				PrescribedDate = prescription.PrescribedDate,
				Description = prescription.Description,
				Prescription = prescription
			};
			return View(model);
		}
        [Authorize]
        [HttpPost]
		public async Task<IActionResult> Edit(int id, EditPrescriptionViewModel model)
		{
			if (ModelState.IsValid)
			{
				var userPrescriptions = _context.Prescription.Where(a => a.UserId == _userManager.GetUserId(User));
				var prescription = userPrescriptions.FirstOrDefault(a => a.PrescriptionId == id);
				prescription.PrescriptionId = model.PrescriptionId;
				prescription.PrescribedDate = model.PrescribedDate;
				prescription.UserId = model.UserId;
				prescription.Medications = model.Medications;
				prescription.Description = model.Description;
				_context.Prescription.Update(prescription);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var userPrescriptions = _context.Prescription.Where(a => a.UserId == _userManager.GetUserId(User));
            var prescription = userPrescriptions.FirstOrDefault(a => a.PrescriptionId == id);
            var model = new DeletePrescriptionViewModel
            {
                PrescriptionId = prescription.PrescriptionId,
                PrescribedDate = prescription.PrescribedDate,
                Medications = prescription.Medications,
                Description = prescription.Description
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id, DeletePrescriptionViewModel model)
        {
            var userPrescriptions = _context.Prescription.Where(a => a.UserId == _userManager.GetUserId(User));
            var prescription = userPrescriptions.FirstOrDefault(a => a.PrescriptionId == id);
            _context.Prescription.Remove(prescription);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
