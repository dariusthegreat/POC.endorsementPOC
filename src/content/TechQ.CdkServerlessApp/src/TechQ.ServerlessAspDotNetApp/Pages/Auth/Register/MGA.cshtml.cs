using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TechQ.ServerlessAspDotNetApp.Pages
{
    public class AccountInfoMGAModel : PageModel
    {
        private readonly ILogger<AccountInfoMGAModel> _logger;

        public AccountInfoMGAModel(ILogger<AccountInfoMGAModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}