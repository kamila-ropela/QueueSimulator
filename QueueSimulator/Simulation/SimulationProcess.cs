﻿using QueueSimulator.Database;
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

            patientPriorityGropu = ChangePriority(patientPriorityGropu, patientPriorityGropu.Count());
            var patientPriorityOrder = patientPriorityGropu.OrderByDescending(x => x).Select(x => Convert.ToDouble(x)).Select(x => x/((double)patientPriorityGropu.Sum()));


            var randomPriority = Math.Round((double)random.Next(1, patientPriorityGropu.Sum() + 1), 2)/(double)patientPriorityGropu.Sum();

            if (randomPriority > 1 - patientPriorityOrder.ElementAt(0))
                return 1;
            else if (randomPriority > 1 - patientPriorityOrder.ElementAt(0) - patientPriorityOrder.ElementAt(1))
                return 2;
            else if (randomPriority > 1 - patientPriorityOrder.ElementAt(0) + patientPriorityOrder.ElementAt(1) + patientPriorityOrder.ElementAt(2))
                return 3;
            else
                return 4;
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
