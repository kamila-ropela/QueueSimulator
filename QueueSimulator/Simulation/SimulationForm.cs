using System.Linq;
using QueueSimulator.Database;
using QueueSimulator.EnumAndDictionary;

namespace QueueSimulator.Simulation
{
    public class SimulationForm
    {
        NewPatients newPatients = new NewPatients();

        public int ConvertAdditionalEventsToBinary(int countPatient, string returnToQuery, string addToQuery, string twoQuery)
        {
            string EventByte = returnToQuery + addToQuery + twoQuery;
            int AdditionalEvents = Helper.ReturnByte(EventByte);
           // CheckIfDbHasEnoughtPatients(countPatient);

            return AdditionalEvents;
        }

        public void CheckIfDbHasEnoughtPatients(int countPatient)
        {
            var countPatientsInDb = PatientsDB.GetNumbersOfRowInTable("Patients");
            if (countPatientsInDb != countPatient)
                newPatients.GeneratePatientWithRandomData((countPatient - countPatientsInDb));
        }

        public string CreateNoteAboutPatient(int id)
        {
            var patientById = PatientsDB.GetDataByIdFromPatientsTable(id);
            
            var result = $@"<b>{patientById.First().PatientName} <br> <br>
                            Płeć: {Dictionary.GetSexByValue(patientById.First().Sex)} <br>
                            Drogi oddechowe: {Dictionary.GetAirwayByValue(patientById.First().Inspection)} <br><br> 
                            Czestość oddechow: {patientById.First().RR}/min <br> 
                            Pulsoksymetria: {patientById.First().POX}% <br> <br>
                            Tetno: {patientById.First().HR}/min <br> 
                            Cisnienie krwi: {patientById.First().BP} mm Hg SBP <br><br>
                            Niepełnosprawność: {Dictionary.GetDisabilityByValue(patientById.First().RLS)} <br> 
                            Temperatura: {patientById.First().Temperature}°C <br> <br>
                            Skala FOUR: {patientById.First().Four} <br> 
                            Skala Glasgow: {patientById.First().GSC} <br> <br>
                            Priorytet: {patientById.First().Priority}</b>";
            return result;
        }
    }
}
