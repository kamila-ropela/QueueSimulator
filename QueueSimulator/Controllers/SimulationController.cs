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
        int PatientAddCount, ExpectedAddedPatients;
        int Algorytm;
        int additionalEvents;

        public IActionResult Simulation()
        {
            Helper.dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;

            //var patientList = PatientsDB.GetDataFromPatientsTable();
            List<Patient> patientList = new List<Patient>();
            ViewData["PatientList"] = patientList;
            ViewData["Status"] = "empty";
            return View();
        }

        public void ReturnByte(string number)
        {
            var result = 0;
            var splitNumbers = number.ToCharArray();

            if (splitNumbers[0] == '0')
                result += 0;
            else
                result += 4;
            if (splitNumbers[1] == '0')
                result += 0;
            else
                result += 2;
            if (splitNumbers[2] == '0')
                result += 0;
            else
                result += 1;

            additionalEvents = result;
        }
        public IActionResult StartSimulation(int countPatient, int countIteration, int Algorytm, string returnToQuery, string addToQuery, string twoQuery)
        {            
            string EventByte = returnToQuery + addToQuery + twoQuery;
            ReturnByte(EventByte);

            this.Algorytm = Algorytm;
            SetPriority();

            var patientList = PatientsDB.GetActivePatientFromPatientsTable();
            return PartialView("Patient", patientList);
        }

        public IActionResult ActivePatients()
        {
            SetStatus();
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
            PatientAddCount = 0;
            return PartialView("AddPatient");
        }

        [HttpPost]
        public IActionResult AddPatient(IFormCollection formData)
        {
            if (false)
            {
                PatientAddCount++;
                newPatients.GeneratePatientBasedOnData(formData);
            }
            return View("Simulation");
        }

        public IActionResult Check()
        {
            ;
            return PartialView();
        }

        public IActionResult AddRandomPatient(string patientCount)
        {
            PatientAddCount++;
            newPatients.GeneratePatientWithRandomData(patientCount);

            var patientList = PatientsDB.GetDataFromPatientsTable();
            return PartialView("Patient", patientList);
        }

        public void AddRandomPatientFromDB(string patientCount)
        {
            PatientAddCount++;
            newPatients.NewPatientFromDB();
        }

        public IActionResult AddRandomPatients(string patientCount)
        {
            newPatients.GeneratePatientWithRandomData(patientCount);

            var patientList = PatientsDB.GetDataFromPatientsTable();
            return PartialView("Patient", patientList);
        }

        public void AddRandomPatientsFromDB(string patientCount)
        {
            newPatients.NewPatientFromDB();
        }

        public void SetPriority()
        {
            Priority priority = new Priority();

            switch (Algorytm)
            {
                case 1:
                    priority.CountPriorityBasedOnGlasgowScale();
                    break;
                case 2:
                    priority.CountPriorityBasedOnFOURScale();
                    break;
                case 3:
                    priority.CountPriorityBasedOnMETTS();
                    break;
                case 4:
                    priority.CountPriorityBasedOnSCON();
                    break;
                case 5:
                    priority.CountPriorityBasedOnSREN();
                    break;
                case 6:
                    priority.CountPriorityBasedOnMIXED();
                    break;
            }
        }

        public void SetStatus()
        {
            Status status = new Status();

            //returnToQuery + addToQuery + twoQuery
            switch (additionalEvents){
                case 0:
                    status.BasedOnPriorityValue();
                    break;
                case 1:
                    //status.BasedOnPriorityValue();
                    status.PriorityWithTwoQuery();
                    break;
                case 2:
                    status.BasedOnPriorityValue();
                    status.PriorityWithAddToQuery();
                    break;
                case 3:
                    //status.BasedOnPriorityValue();
                    status.PriorityWithAddToQuery();
                    status.PriorityWithTwoQuery();
                    break;
                case 4:
                    status.BasedOnPriorityValue();
                    status.PriorityWithReturnToQuery();
                    break;
                case 5:
                   // status.BasedOnPriorityValue();
                    status.PriorityWithReturnToQuery();
                    status.PriorityWithTwoQuery();
                    break;
                case 6:
                    status.BasedOnPriorityValue();
                    status.PriorityWithReturnToQuery();
                    status.PriorityWithAddToQuery();
                    break;
                case 7:
                    //status.BasedOnPriorityValue();
                    status.PriorityWithReturnToQuery();
                    status.PriorityWithAddToQuery();
                    status.PriorityWithTwoQuery();
                    break;
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