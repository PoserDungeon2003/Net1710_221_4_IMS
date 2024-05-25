using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class DeleteModel : PageModel
    {
        private readonly Data.Repository.Net1710_221_4_IMSContext _context;

        public DeleteModel(Data.Repository.Net1710_221_4_IMSContext context)
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
