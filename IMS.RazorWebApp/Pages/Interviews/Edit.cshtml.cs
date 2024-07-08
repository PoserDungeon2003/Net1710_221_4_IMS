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

namespace IMS.RazorWebApp.Pages.Interviews
{
    public class EditModel : PageModel
    {
        private readonly IMS.Data.Repository.Net17102214ImsContext _context;

        public EditModel(IMS.Data.Repository.Net17102214ImsContext context)
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

            var interviewsinfo =  await _context.InterviewsInfos.FirstOrDefaultAsync(m => m.InterviewinfoId == id);
            if (interviewsinfo == null)
            {
                return NotFound();
            }
            InterviewsInfo = interviewsinfo;
           ViewData["InternId"] = new SelectList(_context.Interns, "InternId", "JobPosition");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Attach(InterviewsInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterviewsInfoExists(InterviewsInfo.InterviewinfoId))
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

        private bool InterviewsInfoExists(int id)
        {
            return _context.InterviewsInfos.Any(e => e.InterviewinfoId == id);
        }
    }
}
