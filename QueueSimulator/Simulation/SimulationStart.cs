namespace QueueSimulator.Simulation
{
    public class SimulationStart
    {
        int HighestPriority;
        static int Algorytm;

        public void SetPriority(int algorytm)
        {
            Priority priority = new Priority();
            Algorytm = algorytm;

            switch (algorytm)
            {
                case 1:
                    priority.CountPriorityBasedOnGlasgowScale();
                    HighestPriority = 3;
                    break;
                case 2:
                    priority.CountPriorityBasedOnFOURScale();
                    HighestPriority = 3;
                    break;
                case 3:
                    priority.CountPriorityBasedOnMETTS();
                    HighestPriority = 5;
                    break;
                case 4:
                    priority.CountPriorityBasedOnSCON();
                    HighestPriority = 3;
                    break;
                case 5:
                    priority.CountPriorityBasedOnSREN();
                    HighestPriority = 3;
                    break;
                case 6:
                    priority.CountPriorityBasedOnMIXED();
                    HighestPriority = 3;
                    break;
            }
        }

        public void SetStatus(int iteration, int additionalEvents, int doctorCount)
        {
            Status status = new Status(doctorCount, HighestPriority);

            //liczby w HighestPriority odpowidaja
            //returnToQuery + addToQuery + twoQuery
            switch (additionalEvents)
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