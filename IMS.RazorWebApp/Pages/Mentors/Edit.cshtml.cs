using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class EditModel : PageModel
    {
        private readonly IMS.Data.Models.Net1710_221_4_IMSContext _context;

        public EditModel(IMS.Data.Models.Net1710_221_4_IMSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mentor Mentor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor =  await _context.Mentors.FirstOrDefaultAsync(m => m.MentorId == id);
            if (mentor == null)
            {
                return NotFound();
            }
            Mentor = mentor;
           ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "Address");
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

            _context.Attach(Mentor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MentorExists(Mentor.MentorId))
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

        private bool MentorExists(int id)
        {
            return _context.Mentors.Any(e => e.MentorId == id);
        }
    }
}
