using System;
using QueueSimulator.Database;

namespace QueueSimulator.Simulation
{
    public class SimulationForm
    {
        NewPatients newPatients = new NewPatients();

        public int ConvertAdditionalEventsToBinary(int countPatient, string returnToQuery, string addToQuery, string twoQuery)
        {
            string EventByte = returnToQuery + addToQuery + twoQuery;
            int AdditionalEvents = Helper.ReturnByte(EventByte);
            CheckIfDbHasEnoughtPatients(countPatient);

            return AdditionalEvents;
        }

        public void CheckIfDbHasEnoughtPatients(int countPatient)
        {
            int countPatientsInDb = Convert.ToInt32(PatientsDB.GetNumbersOfRowInTable("Patients"));
            if (countPatientsInDb != countPatient)
                newPatients.GeneratePatientWithRandomData((countPatient - countPatientsInDb));
        }
    }
}
