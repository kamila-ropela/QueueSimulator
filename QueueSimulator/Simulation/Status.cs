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

            newPatients.GeneratePatientWithRandomData(newPatientsCount);
            var lastAddedPatients = PatientsDB.GetLastAddedPatientFromPatientsTable(newPatientsCount);

            switch (Helper.algorytm)
            {
                case 1:
                    lastAddedPatients = Priority.CountPriorityBasedOnGlasgowScale(lastAddedPatients);
                    break;
                case 2:
                    lastAddedPatients = Priority.CountPriorityBasedOnFOURScale(lastAddedPatients);
                    break;
                case 3:
                    lastAddedPatients = Priority.CountPriorityBasedOnMETTS(lastAddedPatients);
                    break;
                case 4:
                    lastAddedPatients = Priority.CountPriorityBasedOnSCON(lastAddedPatients);
                    break;
                case 5:
                    lastAddedPatients = Priority.CountPriorityBasedOnSREN(lastAddedPatients);
                    break;
                case 6:
                    lastAddedPatients = Priority.CountPriorityBasedOnMIXED(lastAddedPatients);
                    break;
            }

            lastAddedPatients.ForEach(x => x.Status = 1);

            //dodanie do tabllicy
            simulationProcess.UpdatePatientList(lastAddedPatients);           
        }

        public void PriorityWithTwoQuery()
        {
            simulationProcess.PropabilityLeaveQueryByPatientInTwoQuery();
        }       
    }
}
