using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class DeleteModel : PageModel
    {
        private readonly IMentorBusiness _mentorBusiness;

        public DeleteModel()
        {
            _mentorBusiness ??= new MentorBusiness();
        }

        [BindProperty]
        public Mentor Mentor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor = await _mentorBusiness.FindAsync(id);

            if (mentor == null)
            {
                return NotFound();
            }
            else
            {
                Mentor = (Mentor)mentor.Data;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor = await _mentorBusiness.FindAsync(id);
            if (mentor != null)
            {
                Mentor = (Mentor)mentor.Data;
                var result = await _mentorBusiness.DeleteAsync(Mentor);

                if (result.Status != Const.SUCCESS_DELETE_CODE)
                {
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
