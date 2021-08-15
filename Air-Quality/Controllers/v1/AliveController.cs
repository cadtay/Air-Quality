using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Air_Quality.Controllers.v1
{
    [Route("")]
    public class AliveController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AliveController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public string Index()
        {

            var sb = new StringBuilder();
            sb.AppendLine("Air Quality API Service Status");
            sb.AppendLine("");
            sb.AppendLine("200 OK");
            sb.AppendLine("");
            sb.AppendLine($"Environment: {_hostingEnvironment.EnvironmentName}");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine($"Culture: {Thread.CurrentThread.CurrentCulture.EnglishName}");
            sb.AppendLine("");
            return sb.ToString();
        }
    }
}
