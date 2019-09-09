using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueSimulator.Simulation
{
    public static class CalculatePriorityOfLeavingPatients
    {
        static Random random = new Random();

        public static int[] ChangePriority(int[] patientPriorityGropu, int patientPriorityCount)
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

        public static int ChoosePatientToLeaveTwoQuery(int priorityMin, int priorityMax)
        {
            var patientPriorityGropu = SimulationProcess.activePatient.GroupBy(x => x.Priority).Select(x => x.Key).Where(x => x.Equals(priorityMin) || x.Equals(priorityMax)).ToArray(); //podział na grupy

            if (patientPriorityGropu.Count() != 1)
            {
                patientPriorityGropu = ChangePriority(patientPriorityGropu, patientPriorityGropu.Count());
                var patientPriorityOrder = patientPriorityGropu.OrderByDescending(x => x).Select(x => Convert.ToDouble(x)).Select(x => x / ((double)patientPriorityGropu.Sum()));


                var randomPriority = Math.Round(Math.Round((double)random.Next(1, patientPriorityGropu.Sum() + 1), 2) / (double)patientPriorityGropu.Sum(), 2);


                if (randomPriority <= patientPriorityOrder.ElementAt(1))
                    return priorityMin;
                else
                    return priorityMax;
            }

            return patientPriorityGropu[0];
        }

        public static int ChoosePatienToLeaveQuery()
        {
            var patientPriorityGropu = SimulationProcess.activePatient.GroupBy(x => x.Priority).Select(x => x.Key).ToArray(); //podział na grupy

            if (patientPriorityGropu.Count() == 0)
                return 0;

            if (patientPriorityGropu.Count() != 1)
            {
                patientPriorityGropu = ChangePriority(patientPriorityGropu, patientPriorityGropu.Count());
                var patientOrderDesc = patientPriorityGropu.OrderByDescending(x => x);
                var patientPriorityOrder = patientOrderDesc.Select(x => Convert.ToDouble(x)).Select(x => x / ((double)patientPriorityGropu.Sum()));
                
                var randomPriority = Math.Round(Math.Round((double)random.Next(1, patientPriorityGropu.Sum() + 1), 2) / (double)patientPriorityGropu.Sum(), 2);

                if (randomPriority >= 1 - patientPriorityOrder.ElementAt(0))
                    return ChangePriority(new int[] { patientOrderDesc.ElementAt(0) }, 1).First();
                else if (randomPriority >= 1 - patientPriorityOrder.ElementAt(0) - patientPriorityOrder.ElementAt(1))
                    return ChangePriority(new int[] { patientOrderDesc.ElementAt(1) }, 1).First();
                else if (randomPriority >= 1 - patientPriorityOrder.ElementAt(0) + patientPriorityOrder.ElementAt(1) + patientPriorityOrder.ElementAt(2))
                    return ChangePriority(new int[] { patientOrderDesc.ElementAt(2) }, 1).First();
                else
                    return ChangePriority(new int[] { patientOrderDesc.ElementAt(3) }, 1).First();
            }
            return patientPriorityGropu[0];
        }

    }
}
