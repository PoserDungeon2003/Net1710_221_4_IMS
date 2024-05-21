using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Intern
{
    public class EditModel : PageModel
    {
        private readonly IMS.Data.Models.Net1710_221_4_IMSContext _context;

        public EditModel(IMS.Data.Models.Net1710_221_4_IMSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Intern Intern { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern =  await _context.Interns.FirstOrDefaultAsync(m => m.InternId == id);
            if (intern == null)
            {
                return NotFound();
            }
            Intern = intern;
           ViewData["MentorId"] = new SelectList(_context.Mentors, "MentorId", "Email");
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

            _context.Attach(Intern).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InternExists(Intern.InternId))
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

        private bool InternExists(int id)
        {
            return _context.Interns.Any(e => e.InternId == id);
        }
    }
}
