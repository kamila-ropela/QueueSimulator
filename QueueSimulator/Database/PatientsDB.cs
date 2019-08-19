using QueueSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueSimulator.Database
{
    public static class PatientsDB
    {
        public static List<Patient> GetDataFromPatientsTable()
        {
            return Helper.dbContext.GetPatientsDb($@"SELECT * FROM Patients");
        }

        public static List<Patient> GetDataByIdFromPatientsTable(int id)
        {
            return Helper.dbContext.GetPatientsDb($@"SELECT * FROM Patients WHERE Id = '{id}'");
        }

        public static List<Patient> GetActivePatientFromPatientsTable()
        {
            return Helper.dbContext.GetPatientsDb($@"SELECT * FROM Patients WHERE Status = '1'");
        }

        public static Patient GetLastAddedPatientFromPatientsTable()
        {
            return Helper.dbContext.GetPatientsDb($@"SELECT * FROM Patients ORDER BY Id DESC LIMIT 1").First();
        }

        public static List<Patient> GetDataFromSavedPatientsTable()
        {
            return Helper.dbContext.GetPatientsDb($@"SELECT * FROM SavedPatients");
        }

        public static int GetNumbersOfRowInTable(string Table)
        {
            var result = Helper.dbContext.GetCountOfRows($@"SELECT COUNT(*) AS count FROM {Table}");
            return Convert.ToInt32(result);
        }

        public static int GetNumbersOfActivePatientsInTable()
        {
            var result = Helper.dbContext.GetCountOfRows($@"SELECT COUNT(*) AS count FROM Patients WHERE Status = '1'");
            return Convert.ToInt32(result);
        }

        public static void PostDataInPatientsTable(Patient data)
        {
            Helper.dbContext.ExecuteQuery($@"INSERT INTO Patients (PatientName, GSC, Inspection, RR, POX, HR, BP, RLS, Temperature, Sex, Four, Status, Priority)
                                             VALUES ('{data.PatientName}', '{data.GSC}', '{data.Inspection}', '{data.RR}', 
                                                     '{data.POX}', '{data.HR}', '{data.BP}', '{data.RLS}', '{data.Temperature}', 
                                                     '{data.Sex}', '{data.Four}', '{data.Status}', '{data.Priority}')");
        }

        public static void PostDataInSavedPatientsTable(Patient data)
        {
            Helper.dbContext.ExecuteQuery($@"INSERT INTO SavedPatients (PatientName, GSC, Inspection, RR, POX, HR, BP, RLS, Temperature, Sex, Four, Status, Priority)
                                             VALUES ('{data.PatientName}', '{data.GSC}', '{data.Inspection}', '{data.RR}', 
                                                     '{data.POX}', '{data.HR}', '{data.BP}', '{data.RLS}', '{data.Temperature}',
                                                     '{data.Sex}', '{data.Four}', '{data.Status}', '{data.Priority}')");
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

        public static void TransferRecordFromSavedPatientTableById(int id)
        {
            Helper.dbContext.ExecuteQuery($@"INSERT INTO SavedPatients SELECT * FROM Patients WHERE Id = '{id}'");
        }
    }
}
