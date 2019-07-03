using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueSimulator.Database;
using QueueSimulator.Models;

namespace QueueSimulator.Controllers
{
    public class SimulationController : Controller
    {
        public IActionResult Simulation()
        {
            Helper.dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;

            ViewData["PatientList"] = new List<Patient>();
            return View();
        }

        public IActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        public void AddPatient(IFormCollection formData)
        {
            var PatientName = Request.Form["PatientName"];
            var DrogiOddechowe = Convert.ToInt32(Request.Form["DrogiOddechowe"]);
            var CzestoscOddechow = Convert.ToInt32(Request.Form["CzestoscOddechow"]);
            var Pulsoksymetria = Convert.ToInt32(Request.Form["Pulsoksymetria"]);
            var Tetno = Convert.ToInt32(Request.Form["Tetno"]);
            var CisnienieKrwi = Convert.ToInt32(Request.Form["CisnienieKrwi"]);
            var Disability = Convert.ToInt32(Request.Form["Disability"]);
            var Temperatura = Convert.ToInt32(Request.Form["Temperatura"]);
            var KontaktSlowny = Convert.ToInt32(Request.Form["KontaktSlowny"].ToString().Substring(2, 1));
            var OtwieranieOczu = Convert.ToInt32(Request.Form["OtwieranieOczu"].ToString().Substring(2, 1));
            var ReakcjaRuchowa = Convert.ToInt32(Request.Form["ReakcjaRuchowa"].ToString().Substring(2, 1));

            var GSC = KontaktSlowny + OtwieranieOczu + ReakcjaRuchowa;

            //TODO dodac status i piority
            Patient patient = new Patient()
            {
                PatientName = PatientName,
                GSC = GSC,
                Temperature = Temperatura,
                Inspection = DrogiOddechowe,
                RLS = Disability,
                RR = CzestoscOddechow,
                POX = Pulsoksymetria,
                HR = Tetno,
                BP = CisnienieKrwi
            };

            Patients.PostData(patient);
        }

        public void CleanTable()
        {
            Patients.CleanTable();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}