using QueueSimulator.Database;
using System;
using System.Collections.Generic;

namespace QueueSimulator.Simulation
{
    public class SimulationProcess
    {
        Random random;
        List<PatientContent> activePatient = new List<PatientContent>();
        int highestPriority;

        public void SetPriority(int algorytm)
        {
            Priority priority = new Priority();

            switch (algorytm)
            {
                case 1:
                    priority.CountPriorityBasedOnGlasgowScale();
                    highestPriority = 3;
                    break;
                case 2:
                    priority.CountPriorityBasedOnFOURScale();
                    highestPriority = 3;
                    break;
                case 3:
                    priority.CountPriorityBasedOnMETTS();
                    highestPriority = 5;
                    break;
                case 4:
                    priority.CountPriorityBasedOnSCON();
                    highestPriority = 3;
                    break;
                case 5:
                    priority.CountPriorityBasedOnSREN();
                    highestPriority = 3;
                    break;
                case 6:
                    priority.CountPriorityBasedOnMIXED();
                    highestPriority = 3;
                    break;
            }
        }

        public void SetStatus(int iteration, int additionalEvents)
        {
            Status status = new Status();

            //returnToQuery + addToQuery + twoQuery
            switch (additionalEvents)
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

        //wylosowanie priorytetu z jakim pacjenci oposzcza kolejke
        public void PropabilityLeaveQueryByPatient(int doctorCount)
        {
            for (int element = 0; element < 2 * doctorCount; element++)
            {
                var priorityLeave = random.Next(1, highestPriority + 1);
                DeletePatientFromTheList(priorityLeave);
            }
        }

        //wypełnienie pacjetow na poczatku symulacji
        public void FillPatientListOnTheBegging()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            foreach (var patient in patients)
            {
                activePatient.Add(new PatientContent() { Id = patient.Id, Priority = patient.Priority });
            }
        }

        //po dodaniu nowego pacjenta lub jego powrocie z listy
        public void UpdatePatientList()
        {
            var lastPatient = PatientsDB.GetLastAddedPatientFromPatientsTable();
            activePatient.Add(new PatientContent() { Id = lastPatient.Id, Priority = lastPatient.Priority });
        }

        //usunięcie pacjenta gdy opusci kolejke
        public void DeletePatientFromTheList(int deletePriority)
        {
            foreach (var patient in activePatient)
            {
                if (patient.Priority == deletePriority)
                {
                    activePatient.Remove(patient);
                    return;
                }
            }
        }

        public void CleanList()
        {
            activePatient.Clear();
        }
    }

    public class PatientContent
    {
        public int Id { get; set; }
        public int Priority { get; set; }
    }
}
