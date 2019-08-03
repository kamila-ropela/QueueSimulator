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
        SimulationForm simulationForm = new SimulationForm();
        SimulationProcess simulationProcess = new SimulationProcess();
        List<Patient> patient = new List<Patient>();
        int PatientAddCount, ExpectedAddedPatients;
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
                            Priorytet: {patient.First().Priority}";

            return result;
        }

        public IActionResult StartSimulation(int countPatient, int countIteration, int Algorytm, string returnToQuery, string addToQuery, string twoQuery, int doctorCount)
        {
            AdditionalEvents = simulationForm.ConvertAdditionalEventsToBinary(countPatient, returnToQuery, addToQuery, twoQuery);
            simulationProcess.SetPriority(Algorytm);

            var patientList = PatientsDB.GetActivePatientFromPatientsTable();
            return PartialView("Patient", patientList);
        }

        //przy procesie symulacji
        public IActionResult ActivePatients(int iteration)
        {
            simulationProcess.SetStatus(iteration, AdditionalEvents);
            var patients = PatientsDB.GetActivePatientFromPatientsTable();

            return PartialView("Patient", patients);
        }

        [HttpPost]
        public IActionResult ReturnPatientInIteratin()
        {            
            return PartialView("Patient", patient);
        }
        
        //???
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

        //pojedyńczy patiennt
        public IActionResult AddRandomPatient(int patientCount)
        {
            PatientAddCount++;
            var patientList = newPatients.NewRandomPatient(patientCount);
            return PartialView("Patient", patientList);
        }

        //pojedyńczy pacjent
        public void AddRandomPatientFromDB(int patientCount)
        {
            PatientAddCount++;
            newPatients.NewPatientFromDB(patientCount);
        }

        //wszyscy pacjenci
        public IActionResult AddRandomPatients(int patientCount)
        {
            var patientList = newPatients.NewRandomPatient(patientCount);
            return PartialView("Patient", patientList);
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