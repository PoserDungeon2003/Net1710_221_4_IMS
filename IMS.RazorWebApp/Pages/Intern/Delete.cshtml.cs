using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Intern
{
    public class DeleteModel : PageModel
    {
        private readonly IMS.Data.Models.Net1710_221_4_IMSContext _context;

        public DeleteModel(IMS.Data.Models.Net1710_221_4_IMSContext context)
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

            var intern = await _context.Interns.FirstOrDefaultAsync(m => m.InternId == id);

            if (intern == null)
            {
                return NotFound();
            }
            else
            {
                Intern = intern;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern = await _context.Interns.FindAsync(id);
            if (intern != null)
            {
                Intern = intern;
                _context.Interns.Remove(Intern);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
