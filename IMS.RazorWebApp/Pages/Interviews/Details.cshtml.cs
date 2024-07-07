using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Common;
using IMS.Business.Business;

namespace IMS.RazorWebApp.Pages.Interviews
{
    public class DetailsModel : PageModel
    {
        private readonly IinterviewsInfoBusiness _interview;

        public DetailsModel(IMS.Data.Repository.Net17102214ImsContext context)
        {
            _interview = new InterviewsInfoBusiness();
        }

        public InterviewsInfo InterviewsInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _interview.FindInterviewAsync(id);
            if (interview == null)
            {
                return NotFound();
            }
            else
            {
                InterviewsInfo = (InterviewsInfo)interview.Data;
            }
            return Page();
        }
    }
}
