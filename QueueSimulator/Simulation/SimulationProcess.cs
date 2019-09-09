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

        public void CleanTable()
        {
            PatientsDB.CleanTable();
        }

        //wylosowanie priorytetu z jakim pacjenci oposzcza kolejke
        public void PropabilityLeaveQueryByPatient()
        {
            for (int element = 0; element < 2 * Helper.doctorCount; element++)
            {
                var priorityLeave = this.ChoosePatienToLeaveQuery();
                DeletePatientFromTheList(priorityLeave);
            }

            activePatient.Where(x => x.Iteration % 2 == 0 && x.Priority != 1 && x.OrginalPriority == 2).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });
            activePatient.Where(x => x.Iteration % 3 == 0 && x.Priority != 1 && x.OrginalPriority == 3).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });
            activePatient.Where(x => x.Iteration % 5 == 0 && x.Priority != 1 && x.OrginalPriority == 4).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });

            activePatient.ForEach(x => x.Iteration++);
        }

        public void PropabilityLeaveQueryByPatientInTwoQuery()
        {
            //kolejka z wyzszym priorytetem
            for (int element = 0; element < Helper.doctorCount; element++)
            {
                var priorityLeave = ChoosePatientToLeaveTwoQuery(4, 3);
                DeletePatientFromTheList(priorityLeave);
            }

            //kkolejka z niższym priorytetem
            for (int element = 0; element < Helper.doctorCount; element++)
            {
                var priorityLeave = ChoosePatientToLeaveTwoQuery(2, 1);
                DeletePatientFromTheList(priorityLeave);
            }

            activePatient.Where(x => x.Iteration % 2 == 0 && x.Priority != 1 && x.OrginalPriority == 2).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });
            activePatient.Where(x => x.Iteration % 3 == 0 && x.Priority != 1 && x.OrginalPriority == 3).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });
            activePatient.Where(x => x.Iteration % 5 == 0 && x.Priority != 1 && x.OrginalPriority == 4).ToList().ForEach(x => { x.Priority--; patientList.Where(a => a.Id == x.Id).ToList().ForEach(a => a.Priority = x.Priority); });

            activePatient.ForEach(x => x.Iteration++);
        }

        private int[] ChangePriority(int[] patientPriorityGropu, int patientPriorityCount)
        {
            for (int i = 0; i < patientPriorityCount; i++)
            {
                if (patientPriorityGropu[i] == 1)
                {
                    patientPriorityGropu[i] = 4;
                    continue;
                }

                else if (patientPriorityGropu[i] == 2)
                {
                    patientPriorityGropu[i] = 3;
                    continue;
                }
                else if (patientPriorityGropu[i] == 3)
                {
                    patientPriorityGropu[i] = 2;
                    continue;
                }
                else
                {
                    patientPriorityGropu[i] = 1;
                    continue;
                }
            }

            return patientPriorityGropu;
        }

        private int ChoosePatienToLeaveQuery()
        {
            var patientPriorityGropu = patientList.GroupBy(x => x.Priority).Select(x => x.Key).ToArray(); //podział na grupy

            if (patientPriorityGropu.Count() == 0)
                return 0;

            if (patientPriorityGropu.Count() != 1)
            {
                patientPriorityGropu = ChangePriority(patientPriorityGropu, patientPriorityGropu.Count());
                var patientPriorityOrder = patientPriorityGropu.OrderByDescending(x => x).Select(x => Convert.ToDouble(x)).Select(x => x / ((double)patientPriorityGropu.Sum()));


                var randomPriority = Math.Round((double)random.Next(1, patientPriorityGropu.Sum() + 1), 2) / (double)patientPriorityGropu.Sum();

                if (randomPriority >= 1 - patientPriorityOrder.ElementAt(0))
                    return 1;
                else if (randomPriority >= 1 - patientPriorityOrder.ElementAt(0) - patientPriorityOrder.ElementAt(1))
                    return 2;
                else if (randomPriority >= 1 - patientPriorityOrder.ElementAt(0) + patientPriorityOrder.ElementAt(1) + patientPriorityOrder.ElementAt(2))
                    return 3;
                else
                    return 4;
            }
            return patientPriorityGropu[0];
        }

        private int ChoosePatientToLeaveTwoQuery(int priorityMin, int priorityMax)
        {
            var patientPriorityGropu = patientList.GroupBy(x => x.Priority).Select(x => x.Key).Where(x => x.Equals(priorityMin) || x.Equals(priorityMax)).ToArray(); //podział na grupy

            if (patientPriorityGropu.Count() != 1)
            {
                patientPriorityGropu = ChangePriority(patientPriorityGropu, patientPriorityGropu.Count());
                var patientPriorityOrder = patientPriorityGropu.OrderByDescending(x => x).Select(x => Convert.ToDouble(x)).Select(x => x / ((double)patientPriorityGropu.Sum()));


                var randomPriority = Math.Round((double)random.Next(1, patientPriorityGropu.Sum() + 1), 2) / (double)patientPriorityGropu.Sum();


                if (randomPriority <= patientPriorityOrder.ElementAt(1))
                    return priorityMin;
                else
                    return priorityMax;
            }

            return patientPriorityGropu[0];
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

                if (Helper.algorytm == 5 || Helper.algorytm == 6)
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
                if (Helper.algorytm == 4)
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
