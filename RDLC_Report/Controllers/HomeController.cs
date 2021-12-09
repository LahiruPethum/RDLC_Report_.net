using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RDLC_Report.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace RDLC_Report.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Print()
        {
            var dt = new DataTable();
            dt = GetEmployeeList();
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Report\\Report.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsEmployee", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);

            return File(result.MainStream, "application/pdf");

        }

        public DataTable GetEmployeeList()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpName");
            dt.Columns.Add("EmpAge");
            dt.Columns.Add("Place");
            dt.Columns.Add("EmpID");

            DataRow row;
            for (int i = 0; i < 100; i++) {
                row = dt.NewRow();
                row["EmpName"] = "Lahiru";
                row["EmpAge"] = 27;
                row["Place"] = "Rambukkana";
                row["EmpID"] = i;

                dt.Rows.Add(row);

            }

            return dt;
        }


    }
}
