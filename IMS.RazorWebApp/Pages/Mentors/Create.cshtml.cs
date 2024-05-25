using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class CreateModel : PageModel
    {
        private readonly MentorBusiness _mentorBusiness;
        private readonly Data.Repository.Net1710_221_4_IMSContext _context;

        public CreateModel(Data.Repository.Net1710_221_4_IMSContext context)
        {
            _context = context;
            _mentorBusiness = new MentorBusiness();
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
            var result = await _mentorBusiness.AddAsync(Mentor);
            if (result.Status != Const.SUCCESS_CREATE_CODE)
            {
                OnGet();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
