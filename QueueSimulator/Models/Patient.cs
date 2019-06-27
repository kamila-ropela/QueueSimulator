using System.ComponentModel;

namespace QueueSimulator.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [DisplayName("Pacjent")]
        public string PatientName { get; set; }
        public string Status { get; set; }
    }
}
