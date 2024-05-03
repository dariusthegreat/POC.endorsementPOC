using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TechQ.ServerlessAspDotNetApp.Pages
{
    public class AccountInfoDriverModel : PageModel
    {
        private readonly ILogger<AccountInfoDriverModel> _logger;

        public AccountInfoDriverModel(ILogger<AccountInfoDriverModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}