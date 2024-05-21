using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class CreateModel : PageModel
    {
        private readonly IMS.Data.Models.K17221imsContext _context;

        public CreateModel(IMS.Data.Models.K17221imsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "Address");
            return Page();
        }

        [BindProperty]
        public Mentor Mentor { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Mentors.Add(Mentor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
