using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TechQ.ServerlessAspDotNetApp.Pages
{
    public class CompanyModel : PageModel
    {
        private readonly ILogger<CompanyModel> _logger;

        public CompanyModel(ILogger<CompanyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}