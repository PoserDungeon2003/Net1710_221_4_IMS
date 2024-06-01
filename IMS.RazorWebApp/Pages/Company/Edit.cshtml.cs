using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models = IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using IMS.Data.Models;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Company
{
    public class EditModel : PageModel
    {
        private readonly ICompanyBusiness _companyBusiness;

        public EditModel()
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

            var mentor = await _companyBusiness.GetByIdAsync(id);
            if (mentor == null)
            {
                return NotFound();
            }
            else
            {
                Company = (Models.Company)mentor.Data;
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await _companyBusiness.UpdateAsync(Company);
                if (result.Status != Const.SUCCESS_UPDATE_CODE)
                {
                    await OnGetAsync(Company.CompanyId);
                    return Page();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(bool)_companyBusiness.CompanyExisted(Company.CompanyId).Data)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
