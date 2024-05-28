﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IMS.RazorWebApp.Pages.Intern
{
    public class CreateModel : PageModel
    {
        private readonly IMS.Data.Repository.Net1710_221_4_IMSContext _context;

        public CreateModel(IMS.Data.Repository.Net1710_221_4_IMSContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "Address");
        ViewData["MentorId"] = new SelectList(_context.Mentors, "MentorId", "Email");
            return Page();
        }

        [BindProperty]
        public Models.Intern Intern { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Interns.Add(Intern);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
