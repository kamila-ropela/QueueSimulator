using Microsoft.AspNetCore.Http;
using QueueSimulator.Database;
using QueueSimulator.Models;
using System;
using System.Collections.Generic;

namespace QueueSimulator.Simulation
{
    public class NewPatients
    {
        public void GeneratePatientWithRandomData(int patientCount)
        {
            string[] name = { "Ania", "Anita", "Adam", "Adriam", "Bartosz", "Bartłmiej", "Barbara", "Bianka", "Celina", "Cycylia", "Czesław" };
            string[] surname = { "Kowalski", "Wybicki", "Lama", "Nowak", "Kruszek", "Fretowski", "Gołebiowski" };
            Random random;

            for (int i = 0; i < patientCount; i++)
            {
                random = new Random();
                var PatientName = name[random.Next(0, 10)] + " " + surname[random.Next(0, 6)];
                var Plec = random.Next(0, 1);
                var DrogiOddechowe = random.Next(1, 2);
                var CzestoscOddechow = random.Next(12, 25);
                var Pulsoksymetria = random.Next(85, 100);
                var Tetno = random.Next(40, 120);
                var CisnienieKrwi = random.Next(80, 110);
                var Disability = random.Next(1, 4);
                var Temperatura = (System.Math.Round(random.NextDouble() * (43 - 36) + 36, 1)).ToString();

                var GSC = random.Next(3, 15);
                var Four = random.Next(0, 16);

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
                    BP = CisnienieKrwi
                };

                PatientsDB.PostDataInPatientsTable(patient);
            }
        }

        public void GeneratePatientBasedOnData(IFormCollection formData)
        {
            var PatientName = formData["PatientName"];
            var Plec = Convert.ToInt32(formData["Plec"]);
            var DrogiOddechowe = Convert.ToInt32(formData["DrogiOddechowe"]);
            var CzestoscOddechow = Convert.ToInt32(formData["CzestoscOddechow"]);
            var Pulsoksymetria = Convert.ToInt32(formData["Pulsoksymetria"]);
            var Tetno = Convert.ToInt32(formData["Tetno"]);
            var CisnienieKrwi = Convert.ToInt32(formData["CisnienieKrwi"]);
            var Disability = Convert.ToInt32(formData["Disability"]);
            var Temperatura = (Convert.ToDouble(formData["Temperatura"])).ToString();
            var KontaktSlowny = Convert.ToInt32(formData["KontaktSlowny"].ToString().Substring(2, 1));
            var OtwieranieOczu = Convert.ToInt32(formData["OtwieranieOczu"].ToString().Substring(2, 1));
            var ReakcjaRuchowa = Convert.ToInt32(formData["ReakcjaRuchowa"].ToString().Substring(2, 1));
            var OdruchyPniaMózgu = Convert.ToInt32(formData["OdruchyPniaMózgu"].ToString().Substring(2, 1));
            var OtwieranieOczuFour = Convert.ToInt32(formData["OtwieranieOczu2"].ToString().Substring(2, 1));
            var OdruchyOddychania = Convert.ToInt32(formData["OdruchyOddychania"].ToString().Substring(2, 1));
            var ReakcjaRuchowaFour = Convert.ToInt32(formData["ReakcjaRuchowa2"].ToString().Substring(2, 1));

            var GSC = KontaktSlowny + OtwieranieOczu + ReakcjaRuchowa;
            var Four = ReakcjaRuchowaFour + OdruchyOddychania + OtwieranieOczuFour + OdruchyPniaMózgu;

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
                Sex = Plec,
                Four = Four,
            };

            PatientsDB.PostDataInPatientsTable(patient);
            PatientsDB.PostDataInSavedPatientsTable(patient);
        }

        public void NewPatientFromDB(int patientCount)
        {
            Random random = new Random();
            var itemsCount = PatientsDB.GetNumbersOfRowInTable("SavedPatients");

            for (int patient = 0; patient < patientCount; patient++)
            {                
               // PatientsDB.TransferRecordFromSavedPatientTableById(random.Next(1, itemsCount));
            }            
        }

        public List<Patient> NewRandomPatient(int patientCount)
        {
            GeneratePatientWithRandomData(patientCount);

            var patientList = PatientsDB.GetDataFromPatientsTable();
            return patientList;
        }
    }
}
