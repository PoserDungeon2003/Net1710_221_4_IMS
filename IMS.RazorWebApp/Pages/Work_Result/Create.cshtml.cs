using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IMS.Data.Models;
using IMS.Data.Repository;

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class CreateModel : PageModel
    {
        private readonly IMS.Data.Repository.Net1710_221_4_IMSContext _context;

        public CreateModel(IMS.Data.Repository.Net1710_221_4_IMSContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["InternId"] = new SelectList(_context.Interns, "InternId", "JobPosition");
        ViewData["MentorId"] = new SelectList(_context.Mentors, "MentorId", "Email");
        ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId", "Description");
            return Page();
        }

        [BindProperty]
        public WorkingResult WorkingResult { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WorkingResults.Add(WorkingResult);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
