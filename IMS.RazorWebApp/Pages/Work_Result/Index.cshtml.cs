using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;
using NuGet.Protocol;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class IndexModel : PageModel
    {
        private readonly Data.Repository.Net1710_221_4_IMSContext _context;

        public IndexModel(Data.Repository.Net1710_221_4_IMSContext context)
        {
            _context = context;
        }

        public IList<WorkingResult> WorkingResult { get;set; } = default!;

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            WorkingResult = await _context.WorkingResults
            .Include(w => w.Intern)
            .Include(w => w.Mentor)
            .Include(w => w.Task).ToListAsync();
        }
    }
}
