using QueueSimulator.Models;
using System.Collections.Generic;

namespace QueueSimulator.Database
{
    public static class Patients
    {
        public static List<Patient> GetDataFromPatientsTable()
        {
            return Helper.dbContext.GetPatientsDb($@"SELECT * FROM Patients");
        }

        public static List<Patient> GetDataFromSavedPatientsTable()
        {
            return Helper.dbContext.GetPatientsDb($@"SELECT * FROM SavedPatients");
        }

        public static void PostDataInPatientsTable(Patient data)
        {
            Helper.dbContext.ExecuteQuery($@"INSERT INTO Patients (PatientName, GSC, Inspection, RR, POX, HR, BP, RLS, Temperature, Sex, Four)
                                             VALUES ('{data.PatientName}', '{data.GSC}', '{data.Inspection}', '{data.RR}', 
                                                     '{data.POX}', '{data.HR}', '{data.BP}', '{data.RLS}', '{data.Temperature}', '{data.Sex}', '{data.Four}')");
        }

        public static void PostDataInSavedPatientsTable(Patient data)
        {
            Helper.dbContext.ExecuteQuery($@"INSERT INTO Patients (PatientName, GSC, Inspection, RR, POX, HR, BP, RLS, Temperature, Sex, Four)
                                             VALUES ('{data.PatientName}', '{data.GSC}', '{data.Inspection}', '{data.RR}', 
                                                     '{data.POX}', '{data.HR}', '{data.BP}', '{data.RLS}', '{data.Temperature}', '{data.Sex}', '{data.Four}')");
        }

        public static void CleanTable()
        {
            Helper.dbContext.ExecuteQuery($@"DELETE FROM Patients");
        }

        public static void UpdateStatusById(int id, string status)
        {
            Helper.dbContext.ExecuteQuery($@"UPDATE Patients SET Status = '{status}' WHERE Id = '{id}'");
        }

        public static void UpdatePriorityById(int id, int priority)
        {
            Helper.dbContext.ExecuteQuery($@"UPDATE Patients SET Priority = '{priority}' WHERE Id = '{id}'");
        }
    }
}
