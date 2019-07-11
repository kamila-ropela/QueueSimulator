using System;
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
            var patientList = Patients.GetDataFromPatientsTable();

            ViewData["PatientList"] = patientList;
            return View();
        }

        [HttpPost]
        public void Simulation(IFormCollection data)
        {
            ;
        }

        public IActionResult AddPatient(string patientCount)
        {
            return View();
        }

        [HttpPost]
        public void AddPatient(IFormCollection formData)
        {
            var PatientName = Request.Form["PatientName"];
            var Plec = Convert.ToInt32(Request.Form["Plec"]);
            var DrogiOddechowe = Convert.ToInt32(Request.Form["DrogiOddechowe"]);
            var CzestoscOddechow = Convert.ToInt32(Request.Form["CzestoscOddechow"]);
            var Pulsoksymetria = Convert.ToInt32(Request.Form["Pulsoksymetria"]);
            var Tetno = Convert.ToInt32(Request.Form["Tetno"]);
            var CisnienieKrwi = Convert.ToInt32(Request.Form["CisnienieKrwi"]);
            var Disability = Convert.ToInt32(Request.Form["Disability"]);
            var Temperatura = (Convert.ToDouble(Request.Form["Temperatura"])).ToString();
            var KontaktSlowny = Convert.ToInt32(Request.Form["KontaktSlowny"].ToString().Substring(2, 1));
            var OtwieranieOczu = Convert.ToInt32(Request.Form["OtwieranieOczu"].ToString().Substring(2, 1));
            var ReakcjaRuchowa = Convert.ToInt32(Request.Form["ReakcjaRuchowa"].ToString().Substring(2, 1));
            var OdruchyPniaMózgu = Convert.ToInt32(Request.Form["OdruchyPniaMózgu"].ToString().Substring(2, 1));
            var OtwieranieOczuFour = Convert.ToInt32(Request.Form["OtwieranieOczu2"].ToString().Substring(2, 1));
            var OdruchyOddychania = Convert.ToInt32(Request.Form["OdruchyOddychania"].ToString().Substring(2, 1));
            var ReakcjaRuchowaFour = Convert.ToInt32(Request.Form["ReakcjaRuchowa2"].ToString().Substring(2, 1));
            
            var GSC = KontaktSlowny + OtwieranieOczu + ReakcjaRuchowa;
            var Four = ReakcjaRuchowaFour + OdruchyOddychania + OtwieranieOczuFour + OdruchyPniaMózgu;

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
                BP = CisnienieKrwi,
                Sex = Plec,
                Four = Four,
            };

            Patients.PostDataInPatientsTable(patient);
            Patients.PostDataInSavedPatientsTable(patient);
        }

        public void AddRandomPatients(string patientCount)
        {
            Random random;
            var count = Convert.ToInt32(patientCount);

            for (int i = 0; i < count; i++)
            {
                random = new Random();
                var PatientName = "Kasia " + random.Next(0, 100);
                var Plec = random.Next(0, 1);
                var DrogiOddechowe = random.Next(1, 2);
            var CzestoscOddechow = random.Next(12, 25);
                var Pulsoksymetria = random.Next(85, 100);
                var Tetno = random.Next(40, 120);
                var CisnienieKrwi = random.Next(80, 110);
                var Disability = random.Next(1, 4);
            var Temperatura = (System.Math.Round(random.NextDouble() * (43 - 36) + 36, 1)).ToString();

            var GSC = random.Next(3, 15);
                var Four = random.Next(0, 16);

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

            Patients.PostDataInPatientsTable(patient);
            }
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