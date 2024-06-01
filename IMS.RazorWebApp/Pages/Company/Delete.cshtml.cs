using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Data.Models;
using IMS.Business.Business;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Company
{
    public class DeleteModel : PageModel
    {
        private readonly ICompanyBusiness _companyBusiness;

        public DeleteModel()
        {
            _companyBusiness ??= new CompanyBusiness();
        }

        [BindProperty]
        public Models.Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyBusiness.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }
            else
            {
                Company = (Models.Company)company.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyBusiness.FindAsync(id);
            if (company != null)
            {
                Company = (Models.Company)company.Data;
                var result = await _companyBusiness.DeleteAsync(Company);

                if (result.Status != Const.SUCCESS_DELETE_CODE)
                {
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
