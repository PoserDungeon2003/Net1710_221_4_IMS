using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class CreateModel : PageModel
    {
        private readonly WorkingResultBusiness _workingResultBusiness;
        private readonly MentorBusiness _mentorBusiness;
        private readonly InternBusiness _internBusiness;

        public CreateModel(IMS.Data.Repository.Net1710_221_4_IMSContext context)
        {
            _workingResultBusiness ??= new WorkingResultBusiness();
            _mentorBusiness ??= new MentorBusiness();
            _internBusiness ??= new InternBusiness();
        }

        public IActionResult OnGet()
        {
       
    //    ViewData["InternId"] = new SelectList(_internBusiness.GetAllAsync()., "InternId");
    //     ViewData["MentorId"] = new SelectList(_context.Mentors, "MentorId");
    //     ViewData["TaskId"] = new SelectList(_context.Tasks, "TaskId");
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

            // _context.WorkingResults.Add(WorkingResult);
            // await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
