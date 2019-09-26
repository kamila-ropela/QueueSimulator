using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueSimulator.Models;
using QueueSimulator.Simulation;
using System.Linq;
using QueueSimulator.Database;

namespace QueueSimulator.Controllers
{
    public class SimulationController : Controller
    {
        NewPatients newPatients = new NewPatients();
        SimulationForm simulationForm = new SimulationForm();
        SimulationProcess simulationProcess = new SimulationProcess();
        SimulationStart simulationStart = new SimulationStart();
        List<Patient> patient = new List<Patient>();

        public IActionResult Simulation()
        {
            Helper.dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;

            Helper.baseUrl = Request.HttpContext.Request.Scheme + "://" + Request.HttpContext.Request.Host.Value;
            return View();
        }

        public IActionResult Raport()
        {
            return View(SimulationRaport.raportList);
        }

        public IActionResult GenerateRaport()
        {
            return SimulationRaport.GenerateRaport();
        }

        public string SetPatientDataToModalPopup(int patientId)
        {
            return simulationForm.CreateNoteAboutPatient(patientId);
        }
       
        public IActionResult StartSimulation(int countPatient, int countIteration, int Algorytm, string returnToQuery, string addToQuery, string twoQuery, int doctorCount)
        {
            int additionalEvents = simulationForm.ConvertAdditionalEventsToBinary(countPatient, returnToQuery, addToQuery, twoQuery);
            Helper.doctorCount = doctorCount;
            Helper.additionalEvents = additionalEvents;
            Helper.algorytm = Algorytm;
            var patients = simulationStart.SetPriority();
            
            simulationProcess.CleanList();
            simulationProcess.FillPatientListOnTheBegging(patients, false);
            SimulationProcess.patientList.ForEach(x => { x.IfAdded = false; x.IfReturned = false; });
            SimulationRaport.UpdatePatientListAfterIteration();

            var patientList = SimulationProcess.patientList.Where(x => x.Status == 1);

            ViewBag.Iteration = 0;
            Helper.iteration = 0;
            return PartialView("Patient", patientList);
        }        

        //przy procesie symulacji
        public IActionResult ActivePatients()
        {
            Helper.iteration++;

            if(SimulationProcess.activePatient.Count() != 0)
                simulationStart.SetStatus();
            
            SimulationRaport.UpdatePatientListAfterIteration();
            var patients = SimulationProcess.patientList.Where(x => x.Status == 1);

            ViewBag.Iteration = Helper.iteration;
            return PartialView("Patient", patients);
        }

        public IActionResult CreateRaport()
        {
            SimulationProcess.CleanTable();
            return View();
        }

        public IActionResult AddPatient(string patientCount)
        {
            return PartialView("AddPatient");
        }

        [HttpPost]
        public IActionResult AddPatient(IFormCollection formData)
        {
            newPatients.GeneratePatientBasedOnData(formData);

            return View("Simulation");
        }

        //wszyscy pacjenci
        public void AddRandomPatients(int patientCount)
        {
            newPatients.NewRandomPatient(patientCount);
        }

        //wszyscy pacjenci
        public void AddRandomPatientsFromDB(int patientCount)
        {
            newPatients.NewPatientFromDB(patientCount);
        }

        public int GetPatientCountInDB()
        {
            return PatientsDB.GetNumbersOfRowInTable("Patients");
        }

        public int GetIteratinNumber()
        {
            return Helper.iteration;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}