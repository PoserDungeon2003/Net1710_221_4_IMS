using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class DeleteModel : PageModel
    {
        private readonly IMS.Data.Repository.Net1710_221_4_IMSContext _context;

        public DeleteModel(IMS.Data.Repository.Net1710_221_4_IMSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WorkingResult WorkingResult { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingresult = await _context.WorkingResults.FirstOrDefaultAsync(m => m.ResultId == id);

            if (workingresult == null)
            {
                return NotFound();
            }
            else
            {
                WorkingResult = workingresult;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingresult = await _context.WorkingResults.FindAsync(id);
            if (workingresult != null)
            {
                WorkingResult = workingresult;
                _context.WorkingResults.Remove(WorkingResult);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
