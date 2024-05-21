﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models = IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Intern
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
        ViewData["MentorId"] = new SelectList(_context.Mentors, "MentorId", "Email");
            return Page();
        }

        [BindProperty]
        public Models.Intern Intern { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Interns.Add(Intern);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
