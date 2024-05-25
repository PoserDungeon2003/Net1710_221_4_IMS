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
    public class DetailsModel : PageModel
    {
        private readonly Data.Repository.Net1710_221_4_IMSContext _context;

        public DetailsModel(Data.Repository.Net1710_221_4_IMSContext context)
        {
            _context = context;
        }

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
    }
}
