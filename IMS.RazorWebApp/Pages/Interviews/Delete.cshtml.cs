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

namespace IMS.RazorWebApp.Pages.Interviews
{
    public class DeleteModel : PageModel
    {
        private readonly IinterviewsInfoBusiness _interviewBussiness;

        public DeleteModel(IMS.Data.Repository.Net17102214ImsContext context)
        {
            _interviewBussiness = new InterviewsInfoBusiness();
        }

        [BindProperty]
        public InterviewsInfo InterviewsInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _interviewBussiness.FindInterviewAsync(id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _interviewBussiness.FindAsync(id);
            if (interview != null)
            {
                InterviewsInfo = (InterviewsInfo)interview.Data;
                var result = await _interviewBussiness.DeleteAsync(InterviewsInfo);

                if (result.Status != Const.SUCCESS_DELETE_CODE)
                {
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
