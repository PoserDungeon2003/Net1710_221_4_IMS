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

namespace IMS.RazorWebApp.Pages.Intern
{
    public class EditModel : PageModel
    {
        private readonly IMS.Data.Repository.Net17102214ImsContext _context;
        private readonly InternBusiness _internBusiness;
        private readonly MentorBusiness _mentorBusiness;
        private readonly CompanyBusiness _companyBusiness;

        public EditModel(IMS.Data.Repository.Net17102214ImsContext context)
        {
            _internBusiness ??= new InternBusiness();
            _mentorBusiness ??= new MentorBusiness();
            _companyBusiness ??= new CompanyBusiness();
        }

        [BindProperty]
        public Models.Intern Intern { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern =  await _internBusiness.GetByID(id);
            var mentor =  _mentorBusiness.GetAllMentor();
            var company = _companyBusiness.GetAllCompany();
            if (intern == null)
            {
                return NotFound();
            }
            Intern = (Models.Intern)intern.Data;
           ViewData["CompanyId"] = new SelectList((System.Collections.IEnumerable)company.Data, "CompanyId", "Address");
           ViewData["MentorId"] = new SelectList((System.Collections.IEnumerable)mentor.Data, "MentorId", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await _internBusiness.Update(Intern);
                if (result.Status != Const.SUCCESS_UPDATE_CODE)
                {
                    await OnGetAsync(Intern.InternId);
                    return Page();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(bool)_internBusiness.InternExisted(Intern.MentorId).Data)
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

        private bool InternExists(int id)
        {
            return _context.Interns.Any(e => e.InternId == id);
        }
    }
}
