using QueueSimulator.Database;
using QueueSimulator.Models;
using System;

namespace QueueSimulationTests
{
    class AddPatientsToDB
    {
        public AddPatientsToDB()
        {
            AddPatientsToDB.Add_Patient_Red(25);
            AddPatientsToDB.Add_Patient_Orange(25);
            AddPatientsToDB.Add_Patient_Yellow(25);
            AddPatientsToDB.Add_Patient_Green(25);
        }

        public static void Add_Patient_Red(int patientCount)
        {
            string[] name = { "Ania", "Anita", "Adam", "Adriam", "Bartosz", "Bartłmiej", "Barbara", "Bianka", "Celina", "Cycylia", "Czesław" };
            string[] surname = { "Kowalski", "Wybicki", "Lama", "Nowak", "Kruszek", "Fretowski", "Gołebiowski" };
            Random random;

            for (int i = 0; i < patientCount; i++)
            {
                random = new Random();
                var PatientName = name[random.Next(0, 10)] + " " + surname[random.Next(0, 6)];
                var Plec = random.Next(0, 1);
                var DrogiOddechowe = 1;
                var CzestoscOddechow = random.Next(31, 40);
                var Pulsoksymetria = random.Next(80, 89);
                var Tetno = random.Next(131, 150);
                var CisnienieKrwi = random.Next(80, 89);
                var Disability = 4;
                var Temperatura = (Math.Round(random.NextDouble() * (43 - 41) + 41, 1)).ToString().Replace(',', '.');

                var GSC = random.Next(0, 4);
                var Four = random.Next(0, 4);

                Patient patient = new Patient()
                {
                    PatientName = PatientName,
                    GSC = GSC,
                    Temperature = Temperatura,
                    Inspection = DrogiOddechowe,
                    RLS = Disability,
                    RR = CzestoscOddechow,
                    POX = Pulsoksymetria,
                    HR = Tetno,
                    BP = CisnienieKrwi,
                    Four = Four,
                    Sex = Plec,
                    Priority = 0,
                    Status = 0
                };

                PatientsDB.PostDataInPatientsTable(patient);
            }
        }

        public static void Add_Patient_Orange(int patientCount)
        {
            string[] name = { "Ania", "Anita", "Adam", "Adriam", "Bartosz", "Bartłmiej", "Barbara", "Bianka", "Celina", "Cycylia", "Czesław" };
            string[] surname = { "Kowalski", "Wybicki", "Lama", "Nowak", "Kruszek", "Fretowski", "Gołebiowski" };
            Random random;

            for (int i = 0; i < patientCount; i++)
            {
                random = new Random();
                var PatientName = name[random.Next(0, 10)] + " " + surname[random.Next(0, 6)];
                var Plec = random.Next(0, 1);
                var DrogiOddechowe = 0;
                var CzestoscOddechow = random.Next(26, 29);
                var Pulsoksymetria = random.Next(100, 110);
                var Tetno = random.Next(121, 129);
                var CisnienieKrwi = random.Next(100, 110);
                var Disability = 3;
                var Temperatura = (Math.Round(random.NextDouble() * (43 - 41) + 41, 1)).ToString().Replace(',', '.');

                var GSC = random.Next(5, 8);
                var Four = random.Next(5, 8);

                Patient patient = new Patient()
                {
                    PatientName = PatientName,
                    GSC = GSC,
                    Temperature = Temperatura,
                    Inspection = DrogiOddechowe,
                    RLS = Disability,
                    RR = CzestoscOddechow,
                    POX = Pulsoksymetria,
                    HR = Tetno,
                    BP = CisnienieKrwi,
                    Four = Four,
                    Sex = Plec,
                    Priority = 0,
                    Status = 0
                };

                PatientsDB.PostDataInPatientsTable(patient);
            }
        }

        public static void Add_Patient_Yellow(int patientCount)
        {
            string[] name = { "Ania", "Anita", "Adam", "Adriam", "Bartosz", "Bartłmiej", "Barbara", "Bianka", "Celina", "Cycylia", "Czesław" };
            string[] surname = { "Kowalski", "Wybicki", "Lama", "Nowak", "Kruszek", "Fretowski", "Gołebiowski" };
            Random random;

            for (int i = 0; i < patientCount; i++)
            {
                random = new Random();
                var PatientName = name[random.Next(0, 10)] + " " + surname[random.Next(0, 6)];
                var Plec = random.Next(0, 1);
                var DrogiOddechowe = 0;
                var CzestoscOddechow = random.Next(0, 8);
                var Pulsoksymetria = random.Next(100, 110);
                var Tetno = random.Next(41, 49);
                var CisnienieKrwi = random.Next(100, 110);
                var Disability = 2;
                var Temperatura = (Math.Round(random.NextDouble() * (40 - 37) + 37, 1)).ToString().Replace(',', '.');

                var GSC = random.Next(9, 12);
                var Four = random.Next(9, 12);

                Patient patient = new Patient()
                {
                    PatientName = PatientName,
                    GSC = GSC,
                    Temperature = Temperatura,
                    Inspection = DrogiOddechowe,
                    RLS = Disability,
                    RR = CzestoscOddechow,
                    POX = Pulsoksymetria,
                    HR = Tetno,
                    BP = CisnienieKrwi,
                    Four = Four,
                    Sex = Plec,
                    Priority = 0,
                    Status = 0
                };

                PatientsDB.PostDataInPatientsTable(patient);
            }
        }

        public static void Add_Patient_Green(int patientCount)
        {
            string[] name = { "Ania", "Anita", "Adam", "Adriam", "Bartosz", "Bartłmiej", "Barbara", "Bianka", "Celina", "Cycylia", "Czesław" };
            string[] surname = { "Kowalski", "Wybicki", "Lama", "Nowak", "Kruszek", "Fretowski", "Gołebiowski" };
            Random random;

            for (int i = 0; i < patientCount; i++)
            {
                random = new Random();
                var PatientName = name[random.Next(0, 10)] + " " + surname[random.Next(0, 6)];
                var Plec = random.Next(0, 1);
                var DrogiOddechowe = 0;
                var CzestoscOddechow = random.Next(10, 24);
                var Pulsoksymetria = random.Next(100, 110);
                var Tetno = random.Next(51, 109);
                var CisnienieKrwi = random.Next(100, 110);
                var Disability = 1;
                var Temperatura = (Math.Round(random.NextDouble() * (37 - 36) + 36, 1)).ToString().Replace(',', '.');

                var GSC = random.Next(13, 15);
                var Four = random.Next(13, 16);

                Patient patient = new Patient()
                {
                    PatientName = PatientName,
                    GSC = GSC,
                    Temperature = Temperatura,
                    Inspection = DrogiOddechowe,
                    RLS = Disability,
                    RR = CzestoscOddechow,
                    POX = Pulsoksymetria,
                    HR = Tetno,
                    BP = CisnienieKrwi,
                    Four = Four,
                    Sex = Plec,
                    Priority = 0,
                    Status = 0
                };

                PatientsDB.PostDataInPatientsTable(patient);
            }
        }
    }
}
