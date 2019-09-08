using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueSimulator.Models;
using QueueSimulator.Simulation;
using SelectPdf;

namespace QueueSimulator.Controllers
{
    public class SimulationController : Controller
    {
        NewPatients newPatients = new NewPatients();
        SimulationForm simulationForm = new SimulationForm();
        SimulationProcess simulationProcess = new SimulationProcess();
        SimulationStart simulationStart = new SimulationStart();
        List<Patient> patient = new List<Patient>();
        SimulationRaport simulationRaport = new SimulationRaport();
        int PatientAddCount, ExpectedAddedPatients;

        public IActionResult Simulation()
        {
            Helper.dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
            return View();
        }

        public string SetPatientDataToModalPopup(int patientId)
        {
            return simulationForm.CreateNoteAboutPatient(patientId);
        }
       
        public IActionResult StartSimulation(int countPatient, int countIteration, int Algorytm, string returnToQuery, string addToQuery, string twoQuery, int doctorCount)
        {
            simulationRaport.GenerateRaport();
            int additionalEvents = simulationForm.ConvertAdditionalEventsToBinary(countPatient, returnToQuery, addToQuery, twoQuery);
            Helper.doctorCount = doctorCount;
            Helper.additionalEvents = additionalEvents;
            Helper.algorytm = Algorytm;
            var patients = simulationStart.SetPriority();
            
            simulationProcess.CleanList();
            simulationProcess.FillPatientListOnTheBegging(patients);
            simulationRaport.UpdatePatientListAfterIteration();

            var patientList = SimulationProcess.patientList;
            return PartialView("Patient", patientList);
        }

        public IActionResult CreateRaport()
        {
            return View();
        }

        //przy procesie symulacji
        public IActionResult ActivePatients(int iteration)
        {
            simulationStart.SetStatus(iteration);
            
            simulationRaport.UpdatePatientListAfterIteration();
            var patients = SimulationProcess.patientList;

            return PartialView("Patient", patients);
        }

        public IActionResult AddPatient(string patientCount)
        {
            //PatientAddCount = 0;
            return PartialView("AddPatient");
        }

        [HttpPost]
        public IActionResult AddPatient(IFormCollection formData)
        {
            //PatientAddCount++;
            newPatients.GeneratePatientBasedOnData(formData);

            return View("Simulation");
        }

        //pojedyńczy patiennt
        public void AddRandomPatient(int patientCount)
        {
            //PatientAddCount++;
            newPatients.NewRandomPatient(patientCount);
            //var patientList;
            //return PartialView("Patient", patientList);
        }

        //pojedyńczy pacjent
        public void AddRandomPatientFromDB(int patientCount)
        {
            //PatientAddCount++;
            newPatients.NewPatientFromDB(patientCount);
        }

        //wszyscy pacjenci
        public void AddRandomPatients(int patientCount)
        {
            newPatients.NewRandomPatient(patientCount);
            //var patientList;
            //return PartialView("Patient", patientList);
        }

        //wszyscy pacjenci
        public void AddRandomPatientsFromDB(int patientCount)
        {
            newPatients.NewPatientFromDB(patientCount);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}