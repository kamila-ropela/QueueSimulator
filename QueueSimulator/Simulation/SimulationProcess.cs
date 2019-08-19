using QueueSimulator.Database;
using QueueSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueSimulator.Simulation
{
    public class SimulationProcess
    {
        Random random = new Random();
        public static List<PatientContent> activePatient = new List<PatientContent>();
        public static List<Patient> patientList = new List<Patient>();

        //public SimulationProcess(int doctor)
        //{
        //    doctorCount = doctor;
        //}
        
        public void CleanTable()
        {
            PatientsDB.CleanTable();
        }

        //wylosowanie priorytetu z jakim pacjenci oposzcza kolejke
        public void PropabilityLeaveQueryByPatient()
        {
            for (int element = 0; element < 2 * Helper.doctorCount; element++)
            {
                var priorityLeave = random.Next(1, Helper.highestPriority + 1);
                DeletePatientFromTheList(priorityLeave);
            }
        }

        public void PropabilityLeaveQueryByPatientInTwoQuery()
        {
            //kolejka z wyzszym priorytetem
            for (int element = 0; element < Helper.doctorCount; element++)
            {
                var priorityLeave = random.Next(1, Helper.highestPriority + 1);
                DeletePatientFromTheList(priorityLeave);
            }

            //kkolejka z niższym priorytetem
            for (int element = 0; element < Helper.doctorCount; element++)
            {
                var priorityLeave = random.Next(1, Helper.highestPriority / 2 + 1);
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
                patientList.Add(patient);
            }
        }

        //po dodaniu nowego pacjenta lub jego powrocie z listy
        public void UpdatePatientList(Patient lastPatient)
        {
            activePatient.Add(new PatientContent() { Id = lastPatient.Id, Priority = lastPatient.Priority });
            patientList.Add(lastPatient);
        }

        //usunięcie pacjenta gdy opusci kolejke
        public void DeletePatientFromTheList(int deletePriority)
        {
            foreach (var patient in activePatient)
            {
                if (patient.Priority == deletePriority)
                {
                    activePatient.Remove(patient);
                    patientList.Remove(patientList.Where(x => x.Id == patient.Id).First());
                    return;
                }
            }
        }

        public void CleanList()
        {
            activePatient.Clear();
            patientList.Clear();
        }
    }

    public class PatientContent
    {
        public int Id { get; set; }
        public int Priority { get; set; }
    }
}
