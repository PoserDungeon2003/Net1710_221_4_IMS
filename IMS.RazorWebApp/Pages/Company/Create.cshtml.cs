using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using IMS.Common;
using IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Company
{
    public class CreateModel : PageModel
    {
        private readonly ICompanyBusiness _companyBusiness;

        public CreateModel()
        {
            _companyBusiness ??= new CompanyBusiness();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Company Company { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _companyBusiness.AddAsync(Company);
            if (result.Status != Const.SUCCESS_CREATE_CODE)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
