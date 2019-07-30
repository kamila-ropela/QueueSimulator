using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        int AdditionalEvents;

        public IActionResult Simulation()
        {
            Helper.dbContext = HttpContext.RequestServices.GetService(typeof(DbContext)) as DbContext;

            var patientList = PatientsDB.GetDataFromPatientsTable();
            List<Patient> l = new List<Patient>();
            ViewData["Data"] = patientList.Last();
            ViewData["PatientList"] = patientList;
            ViewData["Status"] = "empty";
            return View();
        }

        public string SetPatientDataToModalPupop(int patientId)
        {
            var patient = PatientsDB.GetDataByIdFromPatientsTable(17);

            string result = $@"Name: {patient.First().PatientName} <br> 
                            Płeć: {patient.First().Sex} <br> 
                            Drogi oddechowe: {patient.First().Inspection} <br> 
                            Czestość oddechow: {patient.First().RR} <br> 
                            Pulsoksymetria: {patient.First().POX} <br> 
                            Tetno: {patient.First().HR} <br> 
                            Cisnienie krwi: {patient.First().BP} <br>
                            Niepełnosprawność: {patient.First().RLS} <br> 
                            Temperatura: {patient.First().Temperature} <br> 
                            Skala FOUR: {patient.First().Four} <br> 
                            Skala Glasgow: {patient.First().GSC} <br> 
                            Priorytet: {patient.First().Piority}";

            return result;
        }

        public IActionResult StartSimulation(int countPatient, int countIteration, int Algorytm, string returnToQuery, string addToQuery, string twoQuery, int doctorCount)
        {
            ConvertAdditionalEventsToBinary(countPatient, returnToQuery, addToQuery, twoQuery);

            this.Algorytm = Algorytm;
            SetPriority();

            var patientList = PatientsDB.GetActivePatientFromPatientsTable();
            return PartialView("Patient", patientList);
        }

        private void ConvertAdditionalEventsToBinary(int countPatient, string returnToQuery, string addToQuery, string twoQuery)
        {
            string EventByte = returnToQuery + addToQuery + twoQuery;
            AdditionalEvents = Helper.ReturnByte(EventByte);
            CheckIfDbHasEnoughtPatients(countPatient);
        }

        private void CheckIfDbHasEnoughtPatients(int countPatient)
        {
            int countPatientsInDb = Convert.ToInt32(PatientsDB.GetNumbersOfRowInTable("Patients"));
            if (countPatientsInDb != countPatient)
                newPatients.GeneratePatientWithRandomData((countPatient - countPatientsInDb));
        }

        public IActionResult ActivePatients(int iteration)
        {
            SetStatus(iteration);
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
            PatientAddCount++;
            newPatients.GeneratePatientBasedOnData(formData);

            return View("Simulation");
        }

        public IActionResult AddRandomPatient(int patientCount)
        {
            PatientAddCount++;
            newPatients.GeneratePatientWithRandomData(patientCount);

            var patientList = PatientsDB.GetDataFromPatientsTable();
            return PartialView("Patient", patientList);
        }

        public void AddRandomPatientFromDB(int patientCount)
        {
            PatientAddCount++;
            newPatients.NewPatientFromDB(patientCount);
        }

        public IActionResult AddRandomPatients(int patientCount)
        {
            newPatients.GeneratePatientWithRandomData(patientCount);

            var patientList = PatientsDB.GetDataFromPatientsTable();
            return PartialView("Patient", patientList);
        }

        public void AddRandomPatientsFromDB(int patientCount)
        {
            newPatients.NewPatientFromDB(patientCount);
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

        public void SetStatus(int iteration)
        {
            Status status = new Status();

            //returnToQuery + addToQuery + twoQuery
            switch (AdditionalEvents)
            {
                case 0:
                    status.BasedOnPriorityValue(iteration);
                    break;
                case 1:
                    //status.BasedOnPriorityValue();
                    status.PriorityWithTwoQuery();
                    break;
                case 2:
                    status.PriorityWithAddToQuery();
                    status.BasedOnPriorityValue(iteration);
                    break;
                case 3:
                    //status.BasedOnPriorityValue();
                    status.PriorityWithAddToQuery();
                    status.PriorityWithTwoQuery();
                    break;
                case 4:
                    status.PriorityWithReturnToQuery();
                    status.BasedOnPriorityValue(iteration);
                    break;
                case 5:
                    // status.BasedOnPriorityValue();
                    status.PriorityWithReturnToQuery();
                    status.PriorityWithTwoQuery();
                    break;
                case 6:
                    status.PriorityWithReturnToQuery();
                    status.PriorityWithAddToQuery();
                    status.BasedOnPriorityValue(iteration);
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