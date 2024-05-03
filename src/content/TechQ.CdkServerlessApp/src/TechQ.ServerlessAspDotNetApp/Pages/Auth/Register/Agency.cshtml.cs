using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TechQ.ServerlessAspDotNetApp.Pages
{
    public class AccountInfoAgencyModel : PageModel
    {
        private readonly ILogger<AccountInfoAgencyModel> _logger;

        public AccountInfoAgencyModel(ILogger<AccountInfoAgencyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}