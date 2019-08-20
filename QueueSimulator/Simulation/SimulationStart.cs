﻿namespace QueueSimulator.Simulation
{
    public class SimulationStart
    {
        Priority priority = new Priority();
        Status status = new Status();

        public void SetPriority()
        {
            switch (Helper.algorytm)
            {
                case 1:
                    priority.CountPriorityBasedOnGlasgowScale();
                    break;
                case 2:
                    priority.CountPriorityBasedOnFOURScale();
                    break;
                case 3:
                    priority.CountPriorityBasedOnMETTS();
                    break;
                case 4:
                    priority.CountPriorityBasedOnSCON();
                    break;
                case 5:
                    priority.CountPriorityBasedOnSREN();
                    break;
                case 6:
                    priority.CountPriorityBasedOnMIXED();
                    break;
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