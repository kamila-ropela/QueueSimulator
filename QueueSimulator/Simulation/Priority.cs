using QueueSimulator.Database;
using QueueSimulator.Models;
using System;
using System.Globalization;

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
                int priority = GlasgowScale(patient);
                //Random random = new Random();
                //var priority = random.Next(1, 4);

                PatientsDB.UpdatePriorityById(patient.Id, priority);
                PatientsDB.UpdateStatusById(patient.Id, "1");
            }
        }

        public static int GlasgowScale(Patient patient)
        {
            int priority;
            var GSC = patient.GSC;
            if (GSC >= 13)
                priority = 3;
            else if (GSC <= 8)
                priority = 1;
            else
                priority = 2;
            return priority;
        }

        public void CountPriorityBasedOnFOURScale()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            foreach (var patient in patients)
            {
                int priority = FourScale(patient);

                PatientsDB.UpdatePriorityById(patient.Id, priority);
                PatientsDB.UpdateStatusById(patient.Id, "1");
            }
        }

        public static int FourScale(Patient patient)
        {
            int priority;
            var GSC = patient.GSC;
            if (GSC >= 13)
                priority = 3;
            else if (GSC <= 6)
                priority = 1;
            else
                priority = 2;
            return priority;
        }

        //1 - red
        //2 -orange
        //3 - yellow
        //4 - green
        //with oxygen ora without jak rozróznic ?
        public void CountPriorityBasedOnMETTS()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            foreach (var patient in patients)
            {
                var piority = CountPiorityMetts(patient);
                PatientsDB.UpdatePriorityById(patient.Id, piority);
            }
        }

        //czas spedzony u lekarza na wizycie
        public void CountPriorityBasedOnSCON()
        {

        }

        //pozostały czas w kolejce
        public void CountPriorityBasedOnSREN()
        {

        }

        //polaczenie SCON i SREN z wagami
        public void CountPriorityBasedOnMIXED()
        {

        }

        public static int CountPiorityMetts(Patient patient)
        {
            var Piority = 0;
            int[] histogramPiority = new int[4];


            if (CheckIfPatientHaveRedPiority(patient))
                Piority = 4;
            else if (CheckIfPatientHaveOrangePiority(patient))
                Piority = 3;
            else if (CheckIfPatientHaveYellowPiority(patient))
                Piority = 2;
            else if (CheckIfPatientHaveGreenPiority(patient))
                Piority = 1;

            return Piority;
        }

        private static bool CheckIfPatientHaveRedPiority(Patient results)
        {
            if (results.Inspection == 1)
                return true;
            if ((results.RR > 30 || results.RR < 8) && results.POX < 90)
                return true;
            if (results.HR > 130 || results.BP < 90)
                return true;
            if (results.RLS > 3)
                return true;
            return false;
        }

        private static bool CheckIfPatientHaveOrangePiority(Patient results)
        {
            if (results.Inspection == 0)
                return true;
            if (results.RR > 25 || results.POX < 90)
                return true;
            if (results.HR > 120 || results.BP < 40)
                return true;
            if (results.RLS <= 3 || results.RLS >= 2)
                return true;
            if (Double.Parse(results.Temperature, CultureInfo.InvariantCulture) > 41 || Double.Parse(results.Temperature, CultureInfo.InvariantCulture) < 35)
                return true;
            return false;
        }

        private static bool CheckIfPatientHaveYellowPiority(Patient results)
        {
            if (results.Inspection == 0)
                return true;
            if ((results.POX <= 91 || results.POX >= 95) && results.RR < 25)
                return true;
            if (results.BP > 130 || results.HR < 90)
                return true;
            if (results.RLS > 1)
                return true;
            if (Double.Parse(results.Temperature, CultureInfo.InvariantCulture) > 38.5 || Double.Parse(results.Temperature, CultureInfo.InvariantCulture) < 41)
                return true;
            return false;
        }

        private static bool CheckIfPatientHaveGreenPiority(Patient results)
        {
            if (results.Inspection == 0)
                return true;
            if ((results.RR <= 9 || results.RR >= 25) && results.POX > 95)
                return true;
            if (results.BP < 51 || results.HR > 109)
                return true;
            if (results.RLS > 0)
                return true;
            if (Double.Parse(results.Temperature, CultureInfo.InvariantCulture) > 35.1 || Double.Parse(results.Temperature, CultureInfo.InvariantCulture) < 38.4)
                return true;
            return false;
        }
    }
}
