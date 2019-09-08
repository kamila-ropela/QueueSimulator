using QueueSimulator.Database;
using QueueSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueSimulator.Simulation
{
    public class Status
    {
        Random random = new Random();
        SimulationProcess simulationProcess = new SimulationProcess();
        NewPatients newPatients = new NewPatients();

        public void BasedOnPriorityValue(int iteration)
        {         
            simulationProcess.PropabilityLeaveQueryByPatient();
        }

        public void PriorityWithReturnToQuery()
        {
            var patientList = PatientsDB.GetDataFromPatientsTable();
            var noActivePatient = patientList.Where(x => x.Status == 0).Count();
            var returnPatientsCount = random.Next(0, noActivePatient);

            simulationProcess.PropabilityLeaveQueryByPatient();

            for (int i = 1; i < returnPatientsCount; i++)
            {
                var noActivePatientList = patientList.Where(x => x.Status == 0);
                var randomIndex = random.Next(0, noActivePatientList.Count());
                var patientToAdd = new PatientContent() { Id = noActivePatientList.ElementAt(randomIndex).Id, Priority = noActivePatientList.ElementAt(randomIndex).Priority };
                SimulationProcess.activePatient.Add(patientToAdd);
                noActivePatientList.ElementAt(randomIndex).Status = 1;
                PatientsDB.UpdateStatusById(noActivePatientList.ElementAt(randomIndex).Id, "1");
            }
            
        }

        public void PriorityWithAddToQuery()
        {            
            var newPatientsCount = random.Next(0, 15);
            //newPatients.NewPatientFromDB(newPatientsCount);
            for (int i = 1; i < newPatientsCount; i++)
            {
                newPatients.GeneratePatientWithRandomData(1);

                //policzenie priorytetu dla nowego
                var lastAddedPatient = PatientsDB.GetLastAddedPatientFromPatientsTable();

                int priority = 0;
                switch (Helper.algorytm)
                {
                    case 1:
                        priority = Priority.CountPriorityBasedOnGlasgowScale(new List<Patient> { lastAddedPatient }).First().Priority;
                        break;
                    case 2:
                        priority = Priority.CountPriorityBasedOnFOURScale(new List<Patient> { lastAddedPatient }).First().Priority;
                        break;
                    case 3:
                        priority = Priority.CountPriorityBasedOnMETTS(new List<Patient> { lastAddedPatient }).First().Priority;
                        break;
                    case 4:
                        priority = Priority.CountPriorityBasedOnSCON(new List<Patient> { lastAddedPatient }).First().Priority;
                        break;
                    case 5:
                        priority = Priority.CountPriorityBasedOnSREN(new List<Patient> { lastAddedPatient }).First().Priority;
                        break;
                    case 6:
                        priority = Priority.CountPriorityBasedOnMIXED(new List<Patient> { lastAddedPatient }).First().Priority;
                        break;
                }

                lastAddedPatient.Priority = priority;
                lastAddedPatient.Status = 1;
                PatientsDB.UpdateStatusById(lastAddedPatient.Id, "1");
                PatientsDB.UpdatePriorityById(lastAddedPatient.Id, priority);

                //dodanie do tabllicy
                simulationProcess.UpdatePatientList(lastAddedPatient);
            }           
        }

        public void PriorityWithTwoQuery()
        {
            simulationProcess.PropabilityLeaveQueryByPatientInTwoQuery();
        }       
    }
}
