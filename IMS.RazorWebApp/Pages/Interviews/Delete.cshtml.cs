using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;

namespace IMS.RazorWebApp.Pages.Interviews
{
    public class DeleteModel : PageModel
    {
        private readonly IMS.Data.Repository.Net17102214ImsContext _context;

        public DeleteModel(IMS.Data.Repository.Net17102214ImsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InterviewsInfo InterviewsInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewsinfo = await _context.InterviewsInfos.FirstOrDefaultAsync(m => m.InterviewinfoId == id);

            if (interviewsinfo == null)
            {
                return NotFound();
            }
            else
            {
                InterviewsInfo = interviewsinfo;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewsinfo = await _context.InterviewsInfos.FindAsync(id);
            if (interviewsinfo != null)
            {
                InterviewsInfo = interviewsinfo;
                _context.InterviewsInfos.Remove(InterviewsInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
