using NUnit.Framework;
using QueueSimulator.Models;
using QueueSimulator.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueSimulationTests
{
    public class PriorityTests
    {
        [TestCase(13, 4)]
        [TestCase(11, 3)]
        [TestCase(12, 3)]
        [TestCase(9, 3)]
        [TestCase(8, 2)]
        [TestCase(6, 2)]
        [TestCase(4, 1)]
        [TestCase(3, 1)]
        public void Should_set_priority_in_accordance_with_Glasgow(int glasgowValue, int expectedPriority)
        {
            var list = new List<Patient> { new Patient { GSC = glasgowValue } };

            var returnList = Priority.CountPriorityBasedOnGlasgowScale(list);

            Assert.AreEqual(returnList.First().Priority, expectedPriority, $"Should be priority {expectedPriority}, but was {returnList.First().Priority}");
        }

        [TestCase(13, 4)]
        [TestCase(11, 3)]
        [TestCase(12, 3)]
        [TestCase(9, 3)]
        [TestCase(8, 2)]
        [TestCase(6, 2)]
        [TestCase(5, 2)]
        [TestCase(3, 1)]
        public void Should_set_priority_in_accordance_with_Four(int fourValue, int expectedPriority)
        {
            var list = new List<Patient> { new Patient { Four = fourValue } };

            var returnList = Priority.CountPriorityBasedOnFOURScale(list);

            Assert.AreEqual(returnList.First().Priority, expectedPriority, $"Should be priority {expectedPriority}, but was {returnList.First().Priority}");
        }

        [TestCase(1, 40, 80, 140, 80, 4, "0.0", 1)]
        [TestCase(0, 40, 80, 120, 90, 4, "0.0", 1)]
        [TestCase(0, 26, 100, 125, 100, 3, "42.0", 2)]
        [TestCase(0, 29, 100, 55, 100, 3, "39.0", 2)]
        [TestCase(0, 8, 100, 45, 100, 2, "39.0", 3)]
        [TestCase(0, 8, 100, 51, 100, 2, "36.0", 3)]
        [TestCase(0, 20, 100, 70, 100, 1, "36.0", 4)]
        public void Should_set_priority_in_accordance_with_METTS(int instection, int rr, int pox, int hr, int bp, int rls, string temperature, int expectedPriority)
        {
            var list = new List<Patient> {
                new Patient
                {
                    BP = bp,
                    HR = hr,
                    Inspection = instection,
                    Temperature = temperature,
                    RR = rr,
                    POX = pox,
                    RLS = rls                    
                } };

            var returnList = Priority.CountPriorityBasedOnMETTS(list);

            Assert.AreEqual(returnList.First().Priority, expectedPriority, $"Should be priority {expectedPriority}, but was {returnList.First().Priority}");
        }

        [TestCase(4, 6, 33, 160, "0.0", 1, 1.4)]
        [TestCase(3, 4, 32, 160, "0.0", 1, 1.1)]
        [TestCase(6, 7, 38, 133, "41.0", 2, 1.7)]
        [TestCase(12, 7, 27, 121, "41.0", 2, 2.3)]
        [TestCase(11, 5, 6, 44, "39.0", 3, 2.7)]
        [TestCase(12, 10, 7, 30, "36.0", 3, 2.9)]
        [TestCase(13, 14, 7, 60, "36.0", 4, 3.9)]
        [TestCase(13, 14, 10, 70, "37.0", 4, 4.0)]
        public void Should_set_priority_in_accordance_with_Mixed(int gsc, int four, int rr, int hr, string temperature, int expectedPriority, double expectedWeight)
        {
            var list = new List<Patient> {
                new Patient
                {
                    Four = four,
                    HR = hr,
                    GSC = gsc,
                    Temperature = temperature,
                    RR = rr
                } };

            var returnList = Priority.CountPriorityBasedOnMIXED(list);
            Assert.AreEqual(returnList.First().Priority, expectedPriority, $"Should be priority {expectedPriority}, but was {returnList.First().Priority}. Weigt was {returnList.First().WeightPriority}");
            Assert.AreEqual(Math.Round(returnList.First().WeightPriority, 2), Math.Round(expectedWeight, 2) , $"Should be weight {expectedWeight}, but was {returnList.First().WeightPriority}");
        }
    }
}
