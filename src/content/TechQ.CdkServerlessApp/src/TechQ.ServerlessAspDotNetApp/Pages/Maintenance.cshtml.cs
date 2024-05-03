using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TechQ.ServerlessAspDotNetApp.Pages
{
    public class MaintenanceModel : PageModel
    {
        private readonly ILogger<MaintenanceModel> _logger;

        public MaintenanceModel(ILogger<MaintenanceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}