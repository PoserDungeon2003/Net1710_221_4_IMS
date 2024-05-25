using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class EditModel : PageModel
    {
        private readonly IMS.Data.Repository.Net1710_221_4_IMSContext _context;

        public EditModel(IMS.Data.Repository.Net1710_221_4_IMSContext context)
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

            var workingresult =  await _context.WorkingResults.FirstOrDefaultAsync(m => m.ResultId == id);
            if (workingresult == null)
            {
                return NotFound();
            }
            WorkingResult = workingresult;
           ViewData["InternId"] = new SelectList(_context.Interns, "InternId", "JobPosition");
           ViewData["MentorId"] = new SelectList(_context.Mentors, "MentorId", "Email");
           ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "Description");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WorkingResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkingResultExists(WorkingResult.ResultId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WorkingResultExists(int id)
        {
            return _context.WorkingResults.Any(e => e.ResultId == id);
        }
    }
}
