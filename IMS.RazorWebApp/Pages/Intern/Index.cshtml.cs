using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;
using IMS.Data.Repository;

namespace IMS.RazorWebApp.Pages.Intern
{
    public class IndexModel : PageModel
    {
        private readonly IMS.Data.Repository.Net17102214ImsContext _context;

        public IndexModel(IMS.Data.Repository.Net17102214ImsContext context)
        {
            _context = context;
        }

        public IList<Models.Intern> Intern { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Intern = await _context.Interns
                .Include(i => i.Company)
                .Include(i => i.Mentor).ToListAsync();
        }
    }
}
