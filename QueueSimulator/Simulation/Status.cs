using System;

namespace QueueSimulator.Simulation
{
    public class Status
    {
        Random random;
        SimulationProcess simulationProcess = new SimulationProcess();
        int doctorCount, highestPriority;

        public Status(int doctorStatus, int highestPriority) {
            doctorCount = doctorCount;
            highestPriority = highestPriority;
        }

        public void BasedOnPriorityValue(int iteration)
        {
            //var patients = PatientsDB.GetDataFromPatientsTable();            
            simulationProcess.PropabilityLeaveQueryByPatient(doctorCount, highestPriority);
        }

        public void PriorityWithReturnToQuery()
        {
            var returnPatientsCount = random.Next(0, 15);
            //dodawanie dowych do listy z listy db ze statusem 0
            simulationProcess.PropabilityLeaveQueryByPatient(doctorCount, highestPriority);
        }

        public void PriorityWithAddToQuery()
        {
            NewPatients newPatients = new NewPatients();
            var newPatientsCount = random.Next(0, 15);
            //newPatients.NewPatientFromDB(newPatientsCount);
            newPatients.GeneratePatientWithRandomData(newPatientsCount);
            //policzenie priorytetu dl atego nowego
        }

        public void PriorityWithTwoQuery()
        {
            simulationProcess.PropabilityLeaveQueryByPatientInTwoQuery(doctorCount, highestPriority);
        }       
    }
}
