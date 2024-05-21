﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class IndexModel : PageModel
    {
        private readonly IMS.Data.Models.Net1710_221_4_IMSContext _context;

        public IndexModel(IMS.Data.Models.Net1710_221_4_IMSContext context)
        {
            _context = context;
        }

        public IList<Models.Mentor> Mentor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Mentor = await _context.Mentors
                .Include(m => m.Company).ToListAsync();
        }
    }
}
