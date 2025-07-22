using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportController(IWebHostEnvironment webHostEnvironment)
        {
           _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IActionResult Print()
        {
            string mimtype = "";
            int extention = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reportes\\Report.rdlc";
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("Rp1", "Welcome to Library");
            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extention, parameter, mimtype);
            return File(result.MainStream, "application/pdf");

        }
    }
}
