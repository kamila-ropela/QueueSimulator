using Microsoft.AspNetCore.Mvc;
using QueueSimulator.Models;
using SelectPdf;
using System.Collections.Generic;
using System.Linq;

namespace QueueSimulator.Simulation
{
    public static class SimulationRaport
    {
        public static List<List<Patient>> IterationList = new List<List<Patient>>();
        public static List<Raport> raportList = new List<Raport>();

        public static void UpdatePatientListAfterIteration()
        {
            SortData(SimulationProcess.patientList);
            IterationList.Add(new List<Patient>(SimulationProcess.patientList));
        }

        public static FileResult GenerateRaport()
        {
           // SortData();
            HtmlToPdf converter = new HtmlToPdf();

            PdfDocument doc = converter.ConvertUrl("https://localhost:44381/Simulation/Raport");
            byte[] pdf = doc.Save();
            doc.Close();

            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Document.pdf";
            return fileResult;
        }

        private static void SortData(List<Patient> item)
        {          
           /// foreach (var item in ii)
            //{
                raportList.Add(new Raport
                {
                    Red = item.Where(x => x.Priority == 1 && x.Status != 0).Count(),
                    Orange = item.Where(x => x.Priority == 2 && x.Status != 0).Count(),
                    Yellow = item.Where(x => x.Priority == 3 && x.Status != 0).Count(),
                    Grean = item.Where(x => x.Priority == 4 && x.Status != 0).Count(),

                    RedAdded = item.Where(x => x.Priority == 1 && x.IfAdded.Equals(true)).Count(),
                    OrangeAdded = item.Where(x => x.Priority == 2 && x.IfAdded.Equals(true)).Count(),
                    YellowAdded = item.Where(x => x.Priority == 3 && x.IfAdded.Equals(true)).Count(),
                    GreanAdded = item.Where(x => x.Priority == 4 && x.IfAdded.Equals(true)).Count(),

                    RedReturned = item.Where(x => x.Priority == 1 && x.IfReturned.Equals(true)).Count(),
                    OrangeReturned = item.Where(x => x.Priority == 2 && x.IfReturned.Equals(true)).Count(),
                    YellowReturned = item.Where(x => x.Priority == 3 && x.IfReturned.Equals(true)).Count(),
                    GreanReturned = item.Where(x => x.Priority == 4 && x.IfReturned.Equals(true)).Count()
                });
          //  }
        }
    }

    public class Raport
    {
        public int Red { get; set; }
        public int Orange { get; set; }
        public int Yellow { get; set; }
        public int Grean { get; set; }
        public int RedAdded { get; set; }
        public int OrangeAdded { get; set; }
        public int YellowAdded { get; set; }
        public int GreanAdded { get; set; }
        public int RedReturned { get; set; }
        public int OrangeReturned { get; set; }
        public int YellowReturned { get; set; }
        public int GreanReturned { get; set; }
    }
}
