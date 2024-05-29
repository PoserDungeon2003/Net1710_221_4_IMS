using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using IMS.Data.Models;

namespace IMS.RazorWebApp.Pages.Company
{
    public class DetailsModel : PageModel
    {
        private readonly ICompanyBusiness _companyBusiness;

        public DetailsModel()
        {
            _companyBusiness = new CompanyBusiness();
        }

        public Models.Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyBusiness.GetByIdAsync(id);
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
    }
}
