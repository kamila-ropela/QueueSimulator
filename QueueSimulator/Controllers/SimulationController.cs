using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueSimulator.Models;

namespace QueueSimulator.Controllers
{
    public class SimulationController : Controller
    {
        public IActionResult Simulation()
        {
            ViewData["PatientList"] = new List<Patient>();
            return View();
        }

        public IActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPatient(IFormCollection formData)
        {
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}