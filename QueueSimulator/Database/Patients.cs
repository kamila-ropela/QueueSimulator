using QueueSimulator.Models;
using System.Collections.Generic;

namespace QueueSimulator.Database
{
    public static class Patients
    {
        public static List<Patient> GetData()
        {
            return Helper.dbContext.GetPatientsDb($@"SELECT * FROM Patients");
        }

        public static void PostData(Patient data)
        {
            Helper.dbContext.ExecuteQuery($@"INSERT INTO Patients (PatientName, GSC, Inspection, RR, POX, HR, BP, RLS, Temperature)
                                             VALUES ('{data.PatientName}', '{data.GSC}', '{data.Inspection}', '{data.RR}', 
                                                     '{data.POX}', '{data.HR}', '{data.BP}', '{data.RLS}', '{data.Temperature}')");
        }

        public static void CleanTable()
        {
            Helper.dbContext.ExecuteQuery($@"DELETE * FROM Patients");
        }
    }
}
