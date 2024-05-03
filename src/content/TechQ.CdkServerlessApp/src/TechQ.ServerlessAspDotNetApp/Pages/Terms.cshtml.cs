using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TechQ.ServerlessAspDotNetApp.Pages
{
    public class TermsModel : PageModel
    {
        private readonly ILogger<TermsModel> _logger;

        public TermsModel(ILogger<TermsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}