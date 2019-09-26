using NUnit.Framework;
using QueueSimulator.Simulation;
using System;
using System.Linq;

namespace QueueSimulationTests
{
    public class PriorityToLeaveTests
    {


        [TestCase(1.0, 1)]
        [TestCase(0.9, 1)]
        [TestCase(0.8, 1)]
        [TestCase(0.7, 1)]
        [TestCase(0.6, 2)]
        [TestCase(0.5, 2)]
        [TestCase(0.4, 2)]
        [TestCase(0.3, 3)]
        [TestCase(0.2, 3)]
        [TestCase(0.1, 4)]

        public void Should_return_leaveing_patient_base_on_random_number_in_single_querry(double randomPriority, int expectPatientintOut)
        {
            int[] patientPriorityGropu = new int[]{ 1, 2, 3, 4};

            patientPriorityGropu = CalculatePriorityOfLeavingPatients.ChangePriority(patientPriorityGropu, patientPriorityGropu.Count());
            var patientOrderDesc = patientPriorityGropu.OrderByDescending(x => x);
            var patientPriorityOrder = patientOrderDesc.Select(
                 x => Convert.ToDouble(x))
                 .Select(x => Math.Round(x / ((double)patientPriorityGropu.Sum()), 1));

            var returnValue = CalculatePriorityOfLeavingPatients.PriorityToLeave(patientOrderDesc, patientPriorityOrder, randomPriority);

            Assert.AreEqual(expectPatientintOut, returnValue, "Should leave patient with diffrent priority");
        }
    }
}
