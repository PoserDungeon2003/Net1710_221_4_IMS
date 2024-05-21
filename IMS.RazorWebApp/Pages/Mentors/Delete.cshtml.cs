using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class DeleteModel : PageModel
    {
        private readonly IMS.Data.Models.K17221imsContext _context;

        public DeleteModel(IMS.Data.Models.K17221imsContext context)
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

            var mentor = await _context.Mentors.FirstOrDefaultAsync(m => m.MentorId == id);

            if (mentor == null)
            {
                return NotFound();
            }
            else
            {
                Mentor = mentor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor = await _context.Mentors.FindAsync(id);
            if (mentor != null)
            {
                Mentor = mentor;
                _context.Mentors.Remove(Mentor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
