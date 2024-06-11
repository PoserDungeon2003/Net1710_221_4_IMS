using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class EditModel : PageModel
    {
        private readonly IMentorBusiness _mentorBusiness;
        private readonly ICompanyBusiness _companyBusiness;

        public EditModel()
        {
            _mentorBusiness ??= new MentorBusiness();
            _companyBusiness ??= new CompanyBusiness();
        }

        [BindProperty]
        public Mentor Mentor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor =  await _mentorBusiness.FindAsync(id);
            var company = _companyBusiness.GetAllCompany();
            if (mentor == null)
            {
                return NotFound();
            }
            Mentor = (Mentor)mentor.Data;
           ViewData["CompanyId"] = new SelectList((System.Collections.IEnumerable)company.Data, "CompanyId", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await _mentorBusiness.UpdateAsync(Mentor);
                if (result.Status != Const.SUCCESS_UPDATE_CODE)
                {
                    await OnGetAsync(Mentor.MentorId);
                    return Page();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(bool)_mentorBusiness.MentorExisted(Mentor.MentorId).Data)
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
