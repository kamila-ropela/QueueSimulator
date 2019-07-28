using QueueSimulator.Database;

namespace QueueSimulator.Simulation
{
    public class Status
    {
        public void BasedOnPriorityValue()
        {
            var patients = PatientsDB.GetDataFromPatientsTable();

            foreach (var patient in patients)
            {
                //if(patient.Piority == changePriority)
                //{
                //    PatientsDB.UpdateStatusById(patient.Id, "0");
                //    return;
                //}
            }
        }

        public void PriorityWithReturnToQuery()
        {

        }

        public void PriorityWithAddToQuery()
        {

        }

        public void PriorityWithTwoQuery()
        {

        }
    }
}
