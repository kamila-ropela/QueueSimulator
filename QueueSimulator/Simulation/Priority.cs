using QueueSimulator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QueueSimulator.Simulation
{
    public static class Priority
    {
        //1 -> need help now
        //3 -> can wait for help
        public static List<Patient> CountPriorityBasedOnGlasgowScale(List<Patient> patients)
        {
            patients.Where(x => x.GSC >= 13).ToList().ForEach(x => x.Priority = 4);
            patients.Where(x => x.GSC <= 12 && x.GSC >= 9).ToList().ForEach(x => x.Priority = 3);
            patients.Where(x => x.GSC <= 8 && x.GSC >= 4).ToList().ForEach(x => x.Priority = 2);
            patients.Where(x => x.GSC <= 5).ToList().ForEach(x => x.Priority = 1);

            return patients;
        }

        public static List<Patient> CountPriorityBasedOnFOURScale(List<Patient> patients)
        {
            patients.Where(x => x.Four >= 13).ToList().ForEach(x => x.Priority = 4);
            patients.Where(x => x.Four <= 12 && x.Four >= 9).ToList().ForEach(x => x.Priority = 3);
            patients.Where(x => x.Four <= 8 && x.Four >= 5).ToList().ForEach(x => x.Priority = 2);
            patients.Where(x => x.Four <= 4).ToList().ForEach(x => x.Priority = 1);

            return patients;
        }

        //1 - red
        //2 -orange
        //3 - yellow
        //4 - green
        //with oxygen ora without jak rozróznic ?
        public static List<Patient> CountPriorityBasedOnMETTS(List<Patient> patients)
        {
            patients = PatientsHaveRedPriority(patients);
            patients = PatientsHaveOrangePriority(patients);
            patients = PatientsHaveYellowPriority(patients);
            patients = PatientsHaveGreenPriority(patients);

            return patients;
        }

        //czas spedzony u lekarza na wizycie
        public static List<Patient> CountPriorityBasedOnSCON(List<Patient> patients)
        {
            return CountPriorityBasedOnMETTS(patients);
        }

        //pozostały czas w kolejce
        public static List<Patient> CountPriorityBasedOnSREN(List<Patient> patients)
        {
            return CountPriorityBasedOnMETTS(patients);
        }

        //polaczenie SCON i SREN z wagami
        public static List<Patient> CountPriorityBasedOnMIXED(List<Patient> patients)
        {
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

        public static List<Patient> PatientsHaveRedPriority(List<Patient> patients)
        {
            patients.Where(x => x.Inspection == 1).ToList().ForEach(x => x.Priority = 1);
            patients.Where(x => x.RR > 30).ToList().ForEach(x => x.Priority = 1);
            patients.Where(x => x.POX <= 90).ToList().ForEach(x => x.Priority = 1);
            patients.Where(x => x.HR > 130).ToList().ForEach(x => x.Priority = 1);
            patients.Where(x => x.BP <= 90).ToList().ForEach(x => x.Priority = 1);
            patients.Where(x => x.RLS == 4).ToList().ForEach(x => x.Priority = 1);

            return patients;
        }

        public static List<Patient> PatientsHaveOrangePriority(List<Patient> patients)
        {
            patients.Where(x => x.Priority == 0 && (x.Inspection == 0)).ToList().ForEach(x => x.Priority = 2);
            patients.Where(x => x.Priority == 0 && (x.RR > 25 && x.RR <= 30)).ToList().ForEach(x => x.Priority = 2);
            patients.Where(x => x.Priority == 0 && (x.HR <= 40 || (x.HR > 120 && x.HR <= 130))).ToList().ForEach(x => x.Priority = 2);
            patients.Where(x => x.Priority == 0 && (x.RLS == 3)).ToList().ForEach(x => x.Priority = 2);
            patients.Where(x => x.Priority == 0 && (Double.Parse(x.Temperature, CultureInfo.InvariantCulture) > 40 || Double.Parse(x.Temperature, CultureInfo.InvariantCulture) <= 35)).ToList().ForEach(x => x.Priority = 2);

            return patients;
        }

        public static List<Patient> PatientsHaveYellowPriority(List<Patient> patients)
        {
            patients.Where(x => x.Priority == 0 && (x.Inspection == 0)).ToList().ForEach(x => x.Priority = 3);
            patients.Where(x => x.Priority == 0 && (x.RR <= 9)).ToList().ForEach(x => x.Priority = 3);
            patients.Where(x => x.Priority == 0 && ((x.HR > 40 && x.HR <= 50) || (x.HR > 110 && x.HR <= 120))).ToList().ForEach(x => x.Priority = 3);
            patients.Where(x => x.Priority == 0 && (x.RLS == 2)).ToList().ForEach(x => x.Priority = 3);
            patients.Where(x => x.Priority == 0 && (Double.Parse(x.Temperature, CultureInfo.InvariantCulture) <= 41 && Double.Parse(x.Temperature, CultureInfo.InvariantCulture) > 38.5)).ToList().ForEach(x => x.Priority = 3);

            return patients;
        }

        public static List<Patient> PatientsHaveGreenPriority(List<Patient> patients)
        {
            patients.Where(x => x.Priority == 0 && (x.Inspection == 0)).ToList().ForEach(x => x.Priority = 4);
            patients.Where(x => x.Priority == 0 && (x.RR > 9 && x.RR <= 25)).ToList().ForEach(x => x.Priority = 4);
            patients.Where(x => x.Priority == 0 && (x.HR > 50 && x.HR <= 110)).ToList().ForEach(x => x.Priority = 4);
            patients.Where(x => x.Priority == 0 && (x.RLS == 1)).ToList().ForEach(x => x.Priority = 4);
            patients.Where(x => x.Priority == 0 && (Double.Parse(x.Temperature, CultureInfo.InvariantCulture) <= 38.4 && Double.Parse(x.Temperature, CultureInfo.InvariantCulture) > 35.1)).ToList().ForEach(x => x.Priority = 4);

            return patients;
        }
    }
}
