using Microsoft.AspNetCore.Mvc;
using QueueSimulator.Models;
using SelectPdf;
using System.Collections.Generic;

namespace QueueSimulator.Simulation
{
    public class SimulationRaport
    {
        private static List<List<Patient>> IterationList = new List<List<Patient>>();

        public void UpdatePatientListAfterIteration()
        {
            IterationList.Add(new List<Patient>(SimulationProcess.patientList));
        }

        public FileResult GenerateRaport()
        {
            HtmlToPdf converter = new HtmlToPdf();
            string html = "<html><body>hello world</body></html>";

            PdfDocument doc = converter.ConvertHtmlString(html);
            byte[] pdf = doc.Save();
            doc.Close();

            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Document.pdf";
            return fileResult;
        }
    }
}
