using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;
using Task = System.Threading.Tasks.Task;

namespace IMS.RazorWebApp.Pages.Intern
{
    public class IndexModel : PageModel
    {
        private readonly IMS.Data.Models.K17221imsContext _context;

        public IndexModel(IMS.Data.Models.K17221imsContext context)
        {
            _context = context;
        }

        public IList<Models.Intern> Intern { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Intern = await _context.Interns
                .Include(i => i.Mentor).ToListAsync();
        }
    }
}
