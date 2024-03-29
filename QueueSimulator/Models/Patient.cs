﻿using System.ComponentModel;

namespace QueueSimulator.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [DisplayName("Pacjent")]
        public string PatientName { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public int GSC { get; set; }
        public int Inspection { get; set; }
        public int RR { get; set; }
        public int POX { get; set; }
        public int HR { get; set; }
        public int BP { get; set; }
        public int RLS { get; set; }
        public int Sex { get; set; }
        public int Four { get; set; }
        public string Temperature { get; set; }
        public double WeightPriority { get; set; }
        public bool IfAdded { get; set; }
        public bool IfReturned { get; set; }
    }
}
