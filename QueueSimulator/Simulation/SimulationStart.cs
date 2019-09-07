using QueueSimulator.Models;
using System.Collections.Generic;

namespace QueueSimulator.Simulation
{
    public class SimulationStart
    {
        Priority priority = new Priority();
        Status status = new Status();

        public List<Patient> SetPriority()
        {
            switch (Helper.algorytm)
            {
                case 1:
                    return priority.CountPriorityBasedOnGlasgowScale();
                case 2:
                    return priority.CountPriorityBasedOnFOURScale();
                case 3:
                    return priority.CountPriorityBasedOnMETTS();
                case 4:
                    return priority.CountPriorityBasedOnSCON();
                case 5:
                    return priority.CountPriorityBasedOnSREN();
                case 6:
                    return priority.CountPriorityBasedOnMIXED();
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