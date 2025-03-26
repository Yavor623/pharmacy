using Microsoft.AspNetCore.Mvc;
using TestPharmacy1.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
    }
}
