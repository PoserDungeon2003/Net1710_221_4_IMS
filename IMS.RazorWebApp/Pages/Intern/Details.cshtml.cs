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
    public class DetailsModel : PageModel
    {
        private readonly IMS.Data.Models.K17221imsContext _context;

        public DetailsModel(IMS.Data.Models.K17221imsContext context)
        {
            _context = context;
        }

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
    }
}
