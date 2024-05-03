using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TechQ.ServerlessAspDotNetApp.Pages
{
    public class AccountInfoInsuranceCoModel : PageModel
    {
        private readonly ILogger<AccountInfoInsuranceCoModel> _logger;

        public AccountInfoInsuranceCoModel(ILogger<AccountInfoInsuranceCoModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}