using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models = IMS.Data.Models;
using IMS.Data.Repository;

namespace IMS.RazorWebApp.Pages.Interviews
{
    public class IndexModel : PageModel
    {
        private readonly IMS.Data.Repository.Net1710_221_4_IMSContext _context;

        public IndexModel(IMS.Data.Repository.Net1710_221_4_IMSContext context)
        {
            _context = context;
        }

        public IList<Models.InterviewsInfo> InterviewsInfo { get;set; } = default!;

        public async Task OnGetAsync()
        {
            InterviewsInfo = await _context.InterviewsInfos
                .Include(i => i.Intern).ToListAsync();
        }
    }
}
