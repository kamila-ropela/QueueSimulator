using QueueSimulator.Database;
using System;
using System.Collections.Generic;

namespace QueueSimulator.Simulation
{
    public class SimulationProcess
    {
        Random random;
        List<PatientContent> activePatient = new List<PatientContent>();
        //int highestPriority;

        
        public void CleanTable()
        {
            PatientsDB.CleanTable();
        }

        //wylosowanie priorytetu z jakim pacjenci oposzcza kolejke
        public void PropabilityLeaveQueryByPatient(int doctorCount, int highestPriority)
        {
            for (int element = 0; element < 2 * doctorCount; element++)
            {
                var priorityLeave = random.Next(1, highestPriority + 1);
                DeletePatientFromTheList(priorityLeave);
            }
        }

        public void PropabilityLeaveQueryByPatientInTwoQuery(int doctorCount, int highestPriority)
        {
            //kolejka z wyzszym priorytetem
            for (int element = 0; element < doctorCount; element++)
            {
                var priorityLeave = random.Next(1, highestPriority + 1);
                DeletePatientFromTheList(priorityLeave);
            }

            //kkolejka z niższym priorytetem
            for (int element = 0; element < doctorCount; element++)
            {
                var priorityLeave = random.Next(1, highestPriority / 2 + 1);
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
