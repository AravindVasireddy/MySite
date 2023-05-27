using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AVPatients.Models;

namespace AVPatients.Controllers
{
    public class AVMedicationController : Controller
    {
        private readonly PatientsContext _context;

        public AVMedicationController(PatientsContext context)
        {
            _context = context;
        }

        // GET: AVMedication
        public async Task<IActionResult> Index(string medicationTypeId, string typeName)
        {

            if (medicationTypeId != null)
            {
                Response.Cookies.Append("medicationTypeId", medicationTypeId);
                HttpContext.Session.SetString("medicationTypeId", medicationTypeId);
            }
            else if (Request.Query["medicationTypeId"].Any())
            {
                Response.Cookies.Append("medicationTypeId", Request.Query["medicationTypeId"]);
                HttpContext.Session.SetString("medicationTypeId", Request.Query["medicationTypeId"]);
            }
            else if (Request.Cookies["medicationTypeId"] != null)
            {
                medicationTypeId = Request.Cookies["medicationTypeId"].ToString();
            }
            else if (HttpContext.Session.GetString("medicationTypeId") != null)
            {
                medicationTypeId = HttpContext.Session.GetString("medicationTypeId");
            }
            else
            {
                TempData["message"] = "Select  medication type to see its medication";
                return RedirectToAction("Index", "AVMedicationType");
            }

            if (typeName != null)
            {
                Response.Cookies.Append("typeName", typeName);
                HttpContext.Session.SetString("typeName", typeName);
                ViewData["typeName"] = typeName;
            }
            else if (Request.Cookies["typeName"] != null)
            {
                ViewData["typeName"] = Request.Cookies["typeName"].ToString();
            }
            else if (HttpContext.Session.GetString("typeName") != null)
            {
                ViewData["typeName"] = HttpContext.Session.GetString("typeName");
            }
            else
            {
                var medicationType = (from medType in _context.MedicationType
                                        .Where(x => x.MedicationTypeId == Convert.ToInt32(medicationTypeId))
                                      select medType).FirstOrDefault();

                if (medicationType != null)
                {
                    Response.Cookies.Append("typeName", medicationType.Name);
                    HttpContext.Session.SetString("typeName", medicationType.Name);
                    ViewData["typeName"] = medicationType.Name;
                }
            }

            var patientsContext = _context.Medication.Include(m => m.ConcentrationCodeNavigation)
                                    .Include(m => m.DispensingCodeNavigation)
                                    .Include(m => m.MedicationType)
                                    .Where(m => m.MedicationTypeId == Convert.ToInt32(medicationTypeId))
                                    .OrderBy(m => m.Name + m.Concentration);

            return View(await patientsContext.ToListAsync());
        }

        // GET: AVMedication/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (Request.Cookies["typeName"] != null)
            {
                ViewData["typeName"] = Request.Cookies["typeName"].ToString();
            }
            else if (HttpContext.Session.GetString("typeName") != null)
            {
                ViewData["typeName"] = HttpContext.Session.GetString("typeName");
            }

            var medication = await _context.Medication
                .Include(m => m.ConcentrationCodeNavigation)
                .Include(m => m.DispensingCodeNavigation)
                .Include(m => m.MedicationType)
                .FirstOrDefaultAsync(m => m.Din == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // GET: AVMedication/Create
        public IActionResult Create()
        {
            if (Request.Cookies["typeName"] != null)
            {
                ViewData["typeName"] = Request.Cookies["typeName"].ToString();
            }
            else if (HttpContext.Session.GetString("typeName") != null)
            {
                ViewData["typeName"] = HttpContext.Session.GetString("typeName");
            }

            ViewData["ConcentrationCode"] = new SelectList(_context.ConcentrationUnit.OrderBy(m => m.ConcentrationCode), "ConcentrationCode", "ConcentrationCode");
            ViewData["DispensingCode"] = new SelectList(_context.DispensingUnit.OrderBy(m => m.DispensingCode), "DispensingCode", "DispensingCode");
            ViewData["MedicationTypeId"] = new SelectList(_context.MedicationType, "MedicationTypeId", "Name");
            return View();
        }

        // POST: AVMedication/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Din,Name,Image,MedicationTypeId,DispensingCode,Concentration,ConcentrationCode")] Medication medication)
        {
            if (ModelState.IsValid)
            {
                var existingMedication = _context.Medication
                                        .Where(m => m.Name == medication.Name
                                        && m.ConcentrationCode == medication.ConcentrationCode
                                        && m.Concentration == medication.Concentration).FirstOrDefault();
                if (existingMedication != null)
                {
                    TempData["message"] = string.Format("Medication with name as {0}, concentration as {1} and concentration code as {2} already exists.", medication.Name, medication.Concentration, medication.ConcentrationCode);
                }
                else
                {
                    medication.MedicationTypeId = Convert.ToInt32(HttpContext.Session.GetString("medicationTypeId"));
                    _context.Add(medication);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["ConcentrationCode"] = new SelectList(_context.ConcentrationUnit.OrderBy(m => m.ConcentrationCode), "ConcentrationCode", "ConcentrationCode", medication.ConcentrationCode);
            ViewData["DispensingCode"] = new SelectList(_context.DispensingUnit.OrderBy(m => m.DispensingCode), "DispensingCode", "DispensingCode", medication.DispensingCode);
            ViewData["MedicationTypeId"] = new SelectList(_context.MedicationType, "MedicationTypeId", "Name", medication.MedicationTypeId);
            return View(medication);
        }

        // GET: AVMedication/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (Request.Cookies["typeName"] != null)
            {
                ViewData["typeName"] = Request.Cookies["typeName"].ToString();
            }
            else if (HttpContext.Session.GetString("typeName") != null)
            {
                ViewData["typeName"] = HttpContext.Session.GetString("typeName");
            }

            var medication = await _context.Medication.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }
            ViewData["ConcentrationCode"] = new SelectList(_context.ConcentrationUnit.OrderBy(m => m.ConcentrationCode), "ConcentrationCode", "ConcentrationCode", medication.ConcentrationCode);
            ViewData["DispensingCode"] = new SelectList(_context.DispensingUnit.OrderBy(m => m.DispensingCode), "DispensingCode", "DispensingCode", medication.DispensingCode);
            ViewData["MedicationTypeId"] = new SelectList(_context.MedicationType, "MedicationTypeId", "Name", medication.MedicationTypeId);
            return View(medication);
        }

        // POST: AVMedication/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Din,Name,Image,MedicationTypeId,DispensingCode,Concentration,ConcentrationCode")] Medication medication)
        {
            if (id != medication.Din)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationExists(medication.Din))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConcentrationCode"] = new SelectList(_context.ConcentrationUnit.OrderBy(m => m.ConcentrationCode), "ConcentrationCode", "ConcentrationCode", medication.ConcentrationCode);
            ViewData["DispensingCode"] = new SelectList(_context.DispensingUnit.OrderBy(m => m.DispensingCode), "DispensingCode", "DispensingCode", medication.DispensingCode);
            ViewData["MedicationTypeId"] = new SelectList(_context.MedicationType, "MedicationTypeId", "Name", medication.MedicationTypeId);
            return View(medication);
        }

        // GET: AVMedication/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (Request.Cookies["typeName"] != null)
            {
                ViewData["typeName"] = Request.Cookies["typeName"].ToString();
            }
            else if (HttpContext.Session.GetString("typeName") != null)
            {
                ViewData["typeName"] = HttpContext.Session.GetString("typeName");
            }

            var medication = await _context.Medication
                .Include(m => m.ConcentrationCodeNavigation)
                .Include(m => m.DispensingCodeNavigation)
                .Include(m => m.MedicationType)
                .FirstOrDefaultAsync(m => m.Din == id);
            if (medication == null)
            {
                return NotFound();
            }

            return View(medication);
        }

        // POST: AVMedication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var medication = await _context.Medication.FindAsync(id);
            _context.Medication.Remove(medication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicationExists(string id)
        {
            return _context.Medication.Any(e => e.Din == id);
        }
    }
}