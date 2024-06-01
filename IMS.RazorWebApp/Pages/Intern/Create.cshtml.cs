using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using IMS.Business;
using IMS.Business.Business;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Intern
{
    public class CreateModel : PageModel
    {
        private readonly IMS.Data.Repository.Net1710_221_4_IMSContext _context; 
        private readonly InternBusiness _internBusiness;
        private readonly MentorBusiness _mentorBusiness;
        private readonly CompanyBusiness _companyBusiness;
        public string Message { get; set; }=default;


        public CreateModel()
        {
            _internBusiness ??= new InternBusiness();
            _mentorBusiness ??= new MentorBusiness();
            _companyBusiness ??= new CompanyBusiness();
        }

        public IActionResult OnGet()
        {
            var company = _companyBusiness.GetAllCompany();
            var mentor = _mentorBusiness.GetAllMentor();
            ViewData["CompanyId"] = new SelectList((System.Collections.IEnumerable)company.Data, "CompanyId", "Address");
            ViewData["MentorId"] = new SelectList((System.Collections.IEnumerable)mentor.Data, "MentorId", "Email");
            return Page();
        }

        [BindProperty]
        public Models.Intern Intern { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _internBusiness.Save(Intern);
            if (result.Status != Const.SUCCESS_CREATE_CODE)
            {
                OnGet();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
