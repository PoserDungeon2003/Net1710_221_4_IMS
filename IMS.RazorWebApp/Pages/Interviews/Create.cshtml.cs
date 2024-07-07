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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics;

namespace IMS.RazorWebApp.Pages.Interviews
{
    public class CreateModel : PageModel
    {
        private readonly MentorBusiness _mentorBusiness;
        private readonly InternBusiness _internBusiness;

        public CreateModel(IMS.Data.Repository.Net17102214ImsContext context)
        {
            _mentorBusiness ??= new MentorBusiness();
            _internBusiness ??= new InternBusiness();
        }

        public IActionResult OnGet()
        {
           var mentorList = _mentorBusiness.GetAllAsync();
            var internList = _internBusiness.Getall();
            if (internList.Result.Data == null || mentorList.Result.Data == null)
            {
                return NotFound();
            }
            ViewData["MentorId"] = new SelectList((System.Collections.IEnumerable)mentorList.Result.Data, "MentorId", "FullName");
            ViewData["InternId"] = new SelectList((System.Collections.IEnumerable)internList.Result.Data, "InternId", "Name");
           
            return Page();
        }

        [BindProperty]
        public InterviewsInfo InterviewsInfo { get; set; } = default!;

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
