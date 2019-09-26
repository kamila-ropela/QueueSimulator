using System.Linq;
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

            return AdditionalEvents;
        }

        public string CreateNoteAboutPatient(int id)
        {
            var patientById = 
               SimulationProcess.patientList.Where(x => x.Id == id).FirstOrDefault();
            
            var result = $@"<b>{patientById.PatientName} <br> <br>
                            Płeć: {Dictionary.GetSexByValue(patientById.Sex)} <br>
                            Drogi oddechowe: {Dictionary.GetAirwayByValue(patientById.Inspection)} <br><br> 
                            Czestość oddechow: {patientById.RR}/min <br> 
                            Pulsoksymetria: {patientById.POX}% <br> <br>
                            Tetno: {patientById.HR}/min <br> 
                            Cisnienie krwi: {patientById.BP} mm Hg SBP <br><br>
                            Niepełnosprawność: {Dictionary.GetDisabilityByValue(patientById.RLS)} <br> 
                            Temperatura: {patientById.Temperature}°C <br> <br>
                            Skala FOUR: {patientById.Four} <br> 
                            Skala Glasgow: {patientById.GSC} <br> <br>
                            Priorytet: {patientById.Priority}</b>";
            return result;
        }
    }
}
