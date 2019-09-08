using QueueSimulator.Database;
using QueueSimulator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QueueSimulator.Simulation
{
    public class Priority
    {
        //1 -> need help now
        //3 -> can wait for help
        public List<Patient> CountPriorityBasedOnGlasgowScale()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            patients.Where(x => x.GSC >= 13).ToList().ForEach(x => x.Priority = 4);
            patients.Where(x => x.GSC <= 12 && x.GSC >= 9).ToList().ForEach(x => x.Priority = 3);
            patients.Where(x => x.GSC <= 8 && x.GSC >= 4).ToList().ForEach(x => x.Priority = 2);
            patients.Where(x => x.GSC <= 5).ToList().ForEach(x => x.Priority = 1);

            patients.ForEach(x => x.Status = 1);

            return patients;
        }

        public static int GlasgowScale(Patient patient)
        {
            int priority;
            var GSC = patient.GSC;

            if (GSC >= 13)
                priority = 4;
            else if (GSC <= 12 && GSC >= 9)
                priority = 3;
            else if (GSC <= 5)
                priority = 1;
            else
                priority = 2;

            return priority;
        }

        public List<Patient> CountPriorityBasedOnFOURScale()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            patients.Where(x => x.Four >= 13).ToList().ForEach(x => x.Priority = 4);
            patients.Where(x => x.Four <= 12 && x.Four >= 9).ToList().ForEach(x => x.Priority = 3);
            patients.Where(x => x.Four <= 8 && x.Four >= 5).ToList().ForEach(x => x.Priority = 2);
            patients.Where(x => x.Four <= 4).ToList().ForEach(x => x.Priority = 1);

            patients.ForEach(x => x.Status = 1);

            return patients;
        }

        public static int FourScale(Patient patient)
        {
            int priority;
            var Four = patient.Four;

            if (Four >= 13)
                priority = 4;
            else if (Four <= 12 && Four >= 9)
                priority = 3;
            else if (Four <= 4)
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
        public List<Patient> CountPriorityBasedOnMETTS()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            patients.Where(x => CheckIfPatientHaveGreenPiority(x)).ToList().ForEach(x => x.Priority = 4);
            patients.Where(x => CheckIfPatientHaveYellowPiority(x)).ToList().ForEach(x => x.Priority = 3);
            patients.Where(x => CheckIfPatientHaveOrangePiority(x)).ToList().ForEach(x => x.Priority = 2);
            patients.Where(x => CheckIfPatientHaveRedPiority(x)).ToList().ForEach(x => x.Priority = 1);

            patients.ForEach(x => x.Status = 1);

            return patients;
        }

        //czas spedzony u lekarza na wizycie
        public List<Patient> CountPriorityBasedOnSCON()
        {
            return null;
        }

        //pozostały czas w kolejce
        public List<Patient> CountPriorityBasedOnSREN()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            patients.Where(x => CheckIfPatientHaveGreenPiority(x)).ToList().ForEach(x => x.Priority = 4);
            patients.Where(x => CheckIfPatientHaveYellowPiority(x)).ToList().ForEach(x => x.Priority = 3);
            patients.Where(x => CheckIfPatientHaveOrangePiority(x)).ToList().ForEach(x => x.Priority = 2);
            patients.Where(x => CheckIfPatientHaveRedPiority(x)).ToList().ForEach(x => x.Priority = 1);

            patients.ForEach(x => x.Status = 1);

            return patients;
        }

        public static List<Patient> CountAlgorithmSREN()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            //patients.Where(x => CheckIfPatientHaveGreenPiority(x)).ToList().ForEach(x => x.Priority = 4);
            //patients.Where(x => CheckIfPatientHaveYellowPiority(x)).ToList().ForEach(x => x.Priority = 3);
            //patients.Where(x => CheckIfPatientHaveOrangePiority(x)).ToList().ForEach(x => x.Priority = 2);
            //patients.Where(x => CheckIfPatientHaveRedPiority(x)).ToList().ForEach(x => x.Priority = 1);

            //patients.ForEach(x => x.Status = 1);

            return patients;
        }

        //polaczenie SCON i SREN z wagami
        public List<Patient> CountPriorityBasedOnMIXED()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            return WeigthInMixedAlgorithm(patients);
        }

        public static List<Patient> WeigthInMixedAlgorithm(List<Patient> patients)
        {
            double[] weight = new double[] { 3.0 / 10.0, 3.0 / 10.0, 2.0 / 10.0, 1.0 / 10.0, 1.0 / 10.0 };

            //glasgow
            patients.Where(x => x.GSC >= 13).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (4.0 * weight[0]));
            patients.Where(x => x.GSC <= 12 && x.GSC >= 9).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (3.0 * weight[0]));
            patients.Where(x => x.GSC <= 8 && x.GSC >= 4).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (2.0 * weight[0]));
            patients.Where(x => x.GSC <= 5).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (1.0 * weight[0]));

            //four
            patients.Where(x => x.Four >= 13).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (4.0 * weight[1]));
            patients.Where(x => x.Four <= 12 && x.Four >= 9).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (3.0 * weight[1]));
            patients.Where(x => x.Four <= 8 && x.Four >= 5).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (2.0 * weight[1]));
            patients.Where(x => x.Four <= 4).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (1.0 * weight[1]));

            //Curcilation (bp, hr)
            patients.Where(x => x.RR > 30).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (1.0 * weight[1]));
            patients.Where(x => x.RR <= 26 && x.RR >= 29).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (2.0 * weight[1]));
            patients.Where(x => x.RR <= 8).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (3.0 * weight[1]));
            patients.Where(x => x.RR <= 9 && x.RR >= 25).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (4.0 * weight[1]));

            //respiration rate
            patients.Where(x => x.HR >= 130 || x.BP < 90).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (1.0 * weight[1]));
            patients.Where(x => x.HR < 40 || (x.HR <= 120 && x.HR >= 129)).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (2.0 * weight[1]));
            patients.Where(x => (x.HR <= 141 && x.HR >= 50) || (x.HR <= 110 && x.HR >= 119)).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (3.0 * weight[1]));
            patients.Where(x => x.HR <= 51 && x.HR >= 109).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (4.0 * weight[1]));

            //body temperature
            patients.Where(x => Double.Parse(x.Temperature, CultureInfo.InvariantCulture) <= 35.0 && Double.Parse(x.Temperature, CultureInfo.InvariantCulture) >= 38.4).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (4.0 * weight[1]));
            patients.Where(x => Double.Parse(x.Temperature, CultureInfo.InvariantCulture) <= 38.5 && Double.Parse(x.Temperature, CultureInfo.InvariantCulture) > 41).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (3.0 * weight[1]));
            patients.Where(x => Double.Parse(x.Temperature, CultureInfo.InvariantCulture) < 35 || Double.Parse(x.Temperature, CultureInfo.InvariantCulture) >= 41).ToList().ForEach(x => x.WeightPriority = x.WeightPriority + (2.0 * weight[1]));
           
            patients.ForEach(x => x.Priority = Convert.ToInt32(x.WeightPriority));
            return patients;
        }

        public static int CountPiorityMetts(Patient patient)
        {
            var Piority = 0;
            int[] histogramPiority = new int[4];


            if (CheckIfPatientHaveRedPiority(patient))
                Piority = 1;
            else if (CheckIfPatientHaveOrangePiority(patient))
                Piority = 2;
            else if (CheckIfPatientHaveYellowPiority(patient))
                Piority = 3;
            else if (CheckIfPatientHaveGreenPiority(patient))
                Piority = 4;

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
