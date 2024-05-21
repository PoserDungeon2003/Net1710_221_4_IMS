using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Company
{
    public class IndexModel : PageModel
    {
        private readonly IMS.Data.Models.K17221imsContext _context;

        public IndexModel(IMS.Data.Models.K17221imsContext context)
        {
            _context = context;
        }

        public IList<Models.Company> Company { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Company = await _context.Companies.ToListAsync();
        }
    }
}
