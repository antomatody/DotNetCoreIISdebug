using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnetcoreweb1.Models;

namespace dotnetcoreweb1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogWarning("An example of a Warning trace");
            _logger.LogError("An example of an Error level message");
            return View();
        }

        public void TestLog()
        {
            _logger.LogWarning("warning logger");
            _logger.LogError("error logger");
        }


        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogWarning("An example of a Warning trace..");
            _logger.LogError("An example of an Error level message");

            return new string[] { "value1", "value2" };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
