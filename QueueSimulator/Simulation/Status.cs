using QueueSimulator.Database;
using System;
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
                        priority = Priority.GlasgowScale(lastAddedPatient);
                        break;
                    case 2:
                        priority = Priority.FourScale(lastAddedPatient);
                        break;
                    case 3:
                        priority = Priority.CountPiorityMetts(lastAddedPatient);
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
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
