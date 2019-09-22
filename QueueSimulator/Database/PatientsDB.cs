using QueueSimulator.Models;
using System;
using System.Collections.Generic;

namespace QueueSimulator.Database
{
    public static class PatientsDB
    {
        public static List<Patient> GetDataFromPatientsTable()
        {
            return Helper.dbContext.GetPatientsDb($@"SELECT * FROM Patients");
        }

        public static List<Patient> GetLastAddedPatientFromPatientsTable(int lastAddedPatients)
        {
            return Helper.dbContext.GetPatientsDb($@"SELECT * FROM Patients ORDER BY Id DESC LIMIT {lastAddedPatients}");
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

        public static void TransferRecordFromSavedPatientTableById(int id)
        {
            Helper.dbContext.ExecuteQuery($@"INSERT INTO Patients (PatientName, GSC, Inspection, RR, POX, HR, BP, RLS, Temperature, Sex, Four, Status, Priority)
                                                VALUES ( (SELECT PatientName FROM SavedPatients WHERE Id = '{id}'),
(SELECT GSC FROM SavedPatients WHERE Id = '{id}'),
(SELECT Inspection FROM SavedPatients WHERE Id = '{id}'),
(SELECT RR FROM SavedPatients WHERE Id = '{id}'),
(SELECT POX FROM SavedPatients WHERE Id = '{id}'),
(SELECT HR FROM SavedPatients WHERE Id = '{id}'),
(SELECT BP FROM SavedPatients WHERE Id = '{id}'),
(SELECT RLS FROM SavedPatients WHERE Id = '{id}'),
(SELECT Temperature FROM SavedPatients WHERE Id = '{id}'),
(SELECT Sex FROM SavedPatients WHERE Id = '{id}'),
(SELECT Four FROM SavedPatients WHERE Id = '{id}'),
(SELECT Status FROM SavedPatients WHERE Id = '{id}'),
(SELECT Priority FROM SavedPatients WHERE Id = '{id}'))");
        }
    }
}
