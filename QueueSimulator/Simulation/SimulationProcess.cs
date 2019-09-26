using QueueSimulator.Database;
using QueueSimulator.EnumAndDictionary;
using QueueSimulator.Models;
using System.Collections.Generic;
using System.Linq;

namespace QueueSimulator.Simulation
{
    public class SimulationProcess
    {
        public static List<PatientContent> activePatient = new List<PatientContent>();
        public static List<Patient> patientList = new List<Patient>();

        public static void CleanTable()
        {
            PatientsDB.CleanTable();
        }

        //wylosowanie priorytetu z jakim pacjenci oposzcza kolejke
        public void PropabilityLeaveQueryByPatient()
        {
            for (int element = 0; element < 2 * Helper.doctorCount; element++)
            {
                if(activePatient.Count() != 0)
                {
                    var priorityLeave = CalculatePriorityOfLeavingPatients.ChoosePatienToLeaveQuery();
                    DeletePatientFromTheList(priorityLeave);
                }                
            }

            if(Helper.algorytm == (int)AlgorithmEnum.SREN)
            {
                activePatient.ForEach(x => x.Iteration++);

                activePatient.Where(x => x.Iteration % 2 == 0 && x.Iteration != 0 && x.Priority != 1 && x.OrginalPriority == 2).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });
                activePatient.Where(x => x.Iteration % 3 == 0 && x.Iteration != 0 && x.Priority != 1 && x.OrginalPriority == 3).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });
                activePatient.Where(x => x.Iteration % 5 == 0 && x.Iteration != 0 && x.Priority != 1 && x.OrginalPriority == 4).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });
            }
        }

        public void PropabilityLeaveQueryByPatientInTwoQuery()
        {
            //kolejka z wyzszym priorytetem
            for (int element = 0; element < Helper.doctorCount; element++)
            {
                if (activePatient.Count() != 0 && activePatient.Where(x => x.Priority == 4 || x.Priority == 3).Count() != 0)
                {
                    if (activePatient.Where(x => x.Priority == 4).Count() == 0)
                        DeletePatientFromTheList(3);
                    else if (activePatient.Where(x => x.Priority == 3).Count() == 0)
                        DeletePatientFromTheList(4);
                    else
                    {
                        var priorityLeave = CalculatePriorityOfLeavingPatients.ChoosePatientToLeaveTwoQuery(4, 3);
                        DeletePatientFromTheList(priorityLeave);
                    }
                }                    
            }

            //kkolejka z niższym priorytetem
            for (int element = 0; element < Helper.doctorCount; element++)
            {
                if (activePatient.Count() != 0 && activePatient.Where(x => x.Priority == 2 || x.Priority == 1).Count() != 0)
                {
                    if (activePatient.Where(x => x.Priority == 2).Count() == 0)
                        DeletePatientFromTheList(1);
                    else if (activePatient.Where(x => x.Priority == 1).Count() == 0)
                        DeletePatientFromTheList(2);
                    else
                    {
                        var priorityLeave = CalculatePriorityOfLeavingPatients.ChoosePatientToLeaveTwoQuery(2, 1);
                        DeletePatientFromTheList(priorityLeave);
                    }                    
                }                    
            }

            if(Helper.algorytm == (int)AlgorithmEnum.SREN)
            {
                activePatient.ForEach(x => x.Iteration++);

                activePatient.Where(x => x.Iteration % 2 == 0 && x.Iteration != 0 && x.Priority != 1 && x.OrginalPriority == 2).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });
                activePatient.Where(x => x.Iteration % 3 == 0 && x.Iteration != 0 && x.Priority != 1 && x.OrginalPriority == 3).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });
                activePatient.Where(x => x.Iteration % 5 == 0 && x.Iteration != 0 && x.Priority != 1 && x.OrginalPriority == 4).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });
            }
        }

        //wypełnienie pacjetow na poczatku symulacji
        public void FillPatientListOnTheBegging(List<Patient> patients, bool IfReturnToQuery)
        {
            foreach (var patient in patients)
            {
                var pat = new PatientContent
                {
                    Id = patient.Id,
                    Priority = patient.Priority,
                    OrginalPriority = patient.Priority,
                    Iteration = 0
                };

                if (Helper.algorytm == (int)AlgorithmEnum.SREN)
                {                
                    switch (pat.Priority)
                    {
                        case 4:
                            pat.InerationUpdate = 5;
                            pat.Priority = 4;
                            patient.Priority = 4;
                            break;
                        case 3:
                            pat.InerationUpdate = 3;
                            pat.Priority = 4;
                            patient.Priority = 4;
                            break;
                        case 2:
                            pat.InerationUpdate = 2;
                            pat.Priority = 4;
                            patient.Priority = 4;
                            break;
                    }
                }
                if (Helper.algorytm == (int)AlgorithmEnum.SCON)
                {
                    switch (pat.Priority)
                    {
                        case 4:
                            pat.Priority = 2;
                            patient.Priority = 2;
                            break;
                        case 3:
                            pat.Priority = 3;
                            patient.Priority = 3;
                            break;
                        case 2:
                            pat.Priority = 4;
                            patient.Priority = 4;
                            break;
                        case 1:
                            pat.Priority = 1;
                            patient.Priority = 1;
                            break;
                    }

                    if (IfReturnToQuery && pat.Priority != 1)
                    {
                        pat.Priority--;
                        patient.Priority--;
                    }
                }                                    

                activePatient.Add(pat);
                if(!IfReturnToQuery)
                    patientList.Add(patient);
            }
        }

        //po dodaniu nowego pacjenta lub jego powrocie z listy
        public void UpdatePatientList(List<Patient> lastPatients, bool IfReturnToQuery)
        {
            this.FillPatientListOnTheBegging(lastPatients, IfReturnToQuery);
        }

        //usunięcie pacjenta gdy opusci kolejke
        public void DeletePatientFromTheList(int deletePriority)
        {
            var patientToDelete = activePatient.Where(x => x.Priority == deletePriority).First();

            activePatient.Remove(patientToDelete);
            patientList.Where(x => x.Id == patientToDelete.Id).First().Status = 0;
        }

        public void CleanList()
        {
            activePatient.Clear();
            patientList.Clear();
            SimulationRaport.IterationList.Clear();

            SimulationRaport.raportList.Clear();
        }
    }

    public class PatientContent
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public int OrginalPriority { get; set; }
        public int InerationUpdate { get; set; }
        public int Iteration { get; set; }
    }
}
