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

namespace IMS.RazorWebApp.Pages.Mentors
{
    public class DetailsModel : PageModel
    {
        private readonly MentorBusiness _mentorBusiness;

        public DetailsModel()
        {
            _mentorBusiness ??= new MentorBusiness();
        }

        public Mentor Mentor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor = await _mentorBusiness.GetByIdAsync(id);
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
    }
}
