using NUnit.Framework;
using QueueSimulator.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueueSimulationTests
{
    public class PriorityToLeaveTests
    {
        

        //[TestCase(1.0, 1)]
        //[TestCase(0.9, 1)]
        //[TestCase(0.8, 1)]
        //[TestCase(0.7, 1)]
        //[TestCase(0.6, 2)]
        //[TestCase(0.5, 2)]
        //[TestCase(0.4, 2)]
        //[TestCase(0.3, 3)]
        //[TestCase(0.2, 3)]
        //[TestCase(0.1, 4)]
        [TestCase(0.1, 4)]
        [TestCase(0.4, 3)]
        [TestCase(0.6, 1)]

        public void Should_return_leaveing_patient_base_on_random_number_in_single_querry(double randomPriority, int expectPatientintOut)
        {
            int[] patientPriorityGropu = new int[]{ 1, 3, 4};

         //   var patientPriorityGropu = SimulationProcess.activePatient.GroupBy(x => x.Priority).Select(x => x.Key).ToArray(); //podział na grupy

                patientPriorityGropu = CalculatePriorityOfLeavingPatients.ChangePriority(patientPriorityGropu, patientPriorityGropu.Count());
                var patientOrderDesc = patientPriorityGropu.OrderByDescending(x => x);
                var patientPriorityOrder = patientOrderDesc.Select(
                    x => Convert.ToDouble(x))
                    .Select(x => Math.Round(x / ((double)patientPriorityGropu.Sum()), 1));

              //  var randomPriority = Math.Round(Math.Round((double)random.Next(1, patientPriorityGropu.Sum() + 1), 2) / (double)patientPriorityGropu.Sum(), 1);
            var returnValue = CalculatePriorityOfLeavingPatients.PriorityToLeave(patientOrderDesc, patientPriorityOrder, randomPriority);
            //  CalculatePriorityOfLeavingPatients.PriorityToLeave();

            Assert.AreEqual(expectPatientintOut, returnValue, "Should leave patient with diffrent priority");
        }

        [Test]
        public void Should_return_leaveing_patient_base_on_random_number_in_double_querry(double randomPriority, int expectPatientintOut, int priorityMin, int priorityMax)
        {
            var patientPriorityGropu = SimulationProcess.activePatient.GroupBy(x => x.Priority).Select(x => x.Key).Where(x => x.Equals(priorityMin) || x.Equals(priorityMax)).ToArray(); //podział na grupy

                patientPriorityGropu = CalculatePriorityOfLeavingPatients.ChangePriority(patientPriorityGropu, patientPriorityGropu.Count());
                var patientPriorityOrder = patientPriorityGropu.OrderByDescending(x => x).Select(x => Convert.ToDouble(x)).Select(x => x / ((double)patientPriorityGropu.Sum()));


                var returnValue = CalculatePriorityOfLeavingPatients.PriorityToLeaveInDoubleQuery(priorityMin, priorityMax, patientPriorityOrder, randomPriority);


            Assert.AreEqual(expectPatientintOut, returnValue, "Should leave patient with diffrent priority");
        }

        [Test]
        public void Should_return_leaveing_patient_when_is_only_one_in_single_querry(double random, int expectPatientintOut, int[] patientIn)
        {
            //  CalculatePriorityOfLeavingPatients.PriorityToLeave();
        }

        [Test]
        public void Should_return_leaveing_patient_when_is_only_one_in_double_querry(double random, int expectPatientintOut, int[] patientIn)
        {
            //  CalculatePriorityOfLeavingPatients.PriorityToLeave();
        }
    }
}
