using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TechQ.ServerlessAspDotNetApp.Pages
{
    public class RegisterStartModel : PageModel
    {
        private readonly ILogger<RegisterStartModel> _logger;

        public RegisterStartModel(ILogger<RegisterStartModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}