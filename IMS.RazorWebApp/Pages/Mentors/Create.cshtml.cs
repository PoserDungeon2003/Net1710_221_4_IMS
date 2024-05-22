using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IMS.Data.Models;
using IMS.Data.Repository;

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class CreateModel : PageModel
    {
        private readonly Net1710_221_4_IMSContext _context;

        public CreateModel(Net1710_221_4_IMSContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "Name");
            return Page();
        }

        [BindProperty]
        public Mentor Mentor { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    OnGet();
            //    return Page();
            //}

            _context.Mentors.Add(Mentor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
