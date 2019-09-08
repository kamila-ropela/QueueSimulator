using QueueSimulator.Database;
using QueueSimulator.Models;
using System.Collections.Generic;

namespace QueueSimulator.Simulation
{
    public class SimulationStart
    {
        Status status = new Status();

        public List<Patient> SetPriority()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();
            patients.ForEach(x => x.Status = 1);

            switch (Helper.algorytm)
            {
                case 1:
                    return Priority.CountPriorityBasedOnGlasgowScale(patients);
                case 2:
                    return Priority.CountPriorityBasedOnFOURScale(patients);
                case 3:
                    return Priority.CountPriorityBasedOnMETTS(patients);
                case 4:
                    return Priority.CountPriorityBasedOnSCON(patients);
                case 5:
                    return Priority.CountPriorityBasedOnSREN(patients);
                case 6:
                    return Priority.CountPriorityBasedOnMIXED(patients);
                default:
                    return null;
            }
        }

        public void SetStatus(int iteration)
        {
            //liczby w HighestPriority odpowidaja
            //returnToQuery + addToQuery + twoQuery
            switch (Helper.additionalEvents)
            {
                case 0:
                    status.BasedOnPriorityValue(iteration);
                    break;
                case 1:
                    status.PriorityWithTwoQuery();
                    break;
                case 2:
                    status.PriorityWithAddToQuery();
                    status.BasedOnPriorityValue(iteration);
                    break;
                case 3:
                    status.PriorityWithAddToQuery();
                    status.PriorityWithTwoQuery();
                    break;
                case 4:
                    status.PriorityWithReturnToQuery();
                    status.BasedOnPriorityValue(iteration);
                    break;
                case 5:
                    status.PriorityWithReturnToQuery();
                    status.PriorityWithTwoQuery();
                    break;
                case 6:
                    status.PriorityWithReturnToQuery();
                    status.PriorityWithAddToQuery();
                    status.BasedOnPriorityValue(iteration);
                    break;
                case 7:
                    status.PriorityWithReturnToQuery();
                    status.PriorityWithAddToQuery();
                    status.PriorityWithTwoQuery();
                    break;
            }
        }

    }
}