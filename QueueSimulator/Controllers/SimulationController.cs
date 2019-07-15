using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueSimulator.Database;
using QueueSimulator.Models;
using QueueSimulator.Simulation;

namespace QueueSimulator.Controllers
{
    public class SimulationController : Controller
    {
        NewPatients newPatients = new NewPatients();

        public IActionResult Simulation()
        {
            Helper.dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;
            var patientList = PatientsDB.GetDataFromPatientsTable();

            ViewData["PatientList"] = patientList;
            ViewData["Status"] = "List";
            return View();
        }

        public IActionResult StartSimulation(int countPatient, int countIteration, int Algorytm, int returnToQuery, int addToQuery, int twoQuery)
        {
            SetPriorityAndStatus();


            var patientList = PatientsDB.GetDataFromPatientsTable();
            return PartialView("Patient", patientList);
        }

        public IActionResult GiveActivePatients(string piority)
        {
            ChangeStatus(piority);
            var patients = PatientsDB.GetActivePatientFromPatientsTable();

            return PartialView("Patient", patients);
        }

        [HttpPost]
        public IActionResult ReturnPatientInIteratin()
        {
            List<Patient> patient = new List<Patient>();
            return PartialView("Patient", patient);
        }

        public IActionResult AddPatient(string patientCount)
        {
            return PartialView("AddPatient");
        }

        [HttpPost]
        public void AddPatient(IFormCollection formData)
        {
            newPatients.GeneratePatientBasedOnData(formData);
        }
       
        public IActionResult AddRandomPatients(string patientCount)
        {
            newPatients.GeneratePatientWithRandomData(patientCount);

            var patientList = PatientsDB.GetDataFromPatientsTable();
            return PartialView("Patient", patientList);
        }

        public void AddRandomPatientFromDB(string patientCount)
        {
            newPatients.NewPatientFromDB();
        }

        public void SetPriorityAndStatus()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            foreach (var patient in patients)
            {
                /*string priority;
                var GSC = patient.GSC;
                if(GSC >= 13)
                    priority = "3";
                else if(GSC <= 8)
                    priority = "1";
                else
                    priority = "2";*/
                Random random = new Random();
                 var priority = random.Next(0, 3);

                PatientsDB.UpdatePriorityById(patient.Id, priority);
                PatientsDB.UpdateStatusById(patient.Id, "1");
            }
        }

        public void ChangeStatus(string changePriority)
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            foreach (var patient in patients)
            {
                if(patient.Piority == changePriority)
                {
                    PatientsDB.UpdateStatusById(patient.Id, "0");
                    return;
                }
            }
        }

        public void CleanTable()
        {
            PatientsDB.CleanTable();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}