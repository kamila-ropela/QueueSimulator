using QueueSimulator.Database;
using System;

namespace QueueSimulator.Simulation
{
    public class Priority
    {
        //1 -> need help now
        //3 -> can wait for help
        public void CountPriorityBasedOnGlasgowScale()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            foreach (var patient in patients)
            {
                /*string priority;
                var GSC = patient.GSC;
                if(GSC >= 13)
                    priority = "3";
                else if(GSC <= 8)
                    priority = "1";
                else
                    priority = "2";*/
                Random random = new Random();
                var priority = random.Next(1, 4);

                PatientsDB.UpdatePriorityById(patient.Id, priority);
                PatientsDB.UpdateStatusById(patient.Id, "1");
            }
        }

        public void CountPriorityBasedOnFOURScale()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            foreach (var patient in patients)
            {
                int priority;
                var GSC = patient.GSC;
                if(GSC >= 13)
                    priority = 3;
                else if(GSC <= 6)
                    priority = 1;
                else
                    priority = 2;

                PatientsDB.UpdatePriorityById(patient.Id, priority);
                PatientsDB.UpdateStatusById(patient.Id, "1");
            }
        }

        public void CountPriorityBasedOnMETTS()
        {

        }

        //czas spedzony u lekarza na wizycie
        public void CountPriorityBasedOnSCON()
        {

        }

        //pozostały czas w kolejce
        public void CountPriorityBasedOnSREN()
        {

        }

        public void CountPriorityBasedOnMIXED()
        {

        }
    }
}
