using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TechQ.ServerlessAspDotNetApp.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly ILogger<ForgotPasswordModel> _logger;

        public ForgotPasswordModel(ILogger<ForgotPasswordModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}