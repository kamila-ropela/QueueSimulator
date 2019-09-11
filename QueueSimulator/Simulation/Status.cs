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

        public void BasedOnPriorityValue()
        {         
            simulationProcess.PropabilityLeaveQueryByPatient();
        }

        public void PriorityWithReturnToQuery(bool ifTwoQuery)
        {
            var noActivePatient = SimulationProcess.patientList.Where(x => x.Status == 0).Count();
            var returnPatientsCount = random.Next(0, noActivePatient); //ile pacjentow na być ponownie dodanych do kolejki

            if (ifTwoQuery)
                simulationProcess.PropabilityLeaveQueryByPatientInTwoQuery();
            else
                simulationProcess.PropabilityLeaveQueryByPatient();


            for (int i = 0; i < returnPatientsCount; i++)
            {
                var noActivePatientList = SimulationProcess.patientList.Where(x => x.Status == 0);
                var randomIndex = random.Next(0, noActivePatientList.Count() - 1);

                SimulationProcess.patientList.Where(x => x.Id == noActivePatientList.ElementAt(randomIndex).Id).ToList().ForEach(x => x.Status = 1);
                noActivePatientList.ElementAt(randomIndex).IfReturned = true;
                simulationProcess.UpdatePatientList(new List<Patient>{ noActivePatientList.Where(x => x.Id == noActivePatientList.ElementAt(randomIndex).Id).First()}, true);
            }            
        }

        public void PriorityWithAddToQuery()
        {            
            var newPatientsCount = random.Next(0, 15);

            newPatients.GeneratePatientWithRandomData(newPatientsCount);
            var lastAddedPatients = PatientsDB.GetLastAddedPatientFromPatientsTable(newPatientsCount);
            lastAddedPatients.ForEach(x => { x.IfAdded = true; x.IfReturned = false; });

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
            simulationProcess.UpdatePatientList(lastAddedPatients, false);           
        }

        public void PriorityWithTwoQuery()
        {
            simulationProcess.PropabilityLeaveQueryByPatientInTwoQuery();
        }       
    }
}
