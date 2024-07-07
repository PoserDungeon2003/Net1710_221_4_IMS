using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IMS.Data.Models;
using IMS.Data.Repository;
using IMS.Business.Business;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Interviews
{
    public class CreateModel : PageModel
    {
        private readonly IMentorBusiness _mentorBusiness;
        private readonly IInternBusiness _internBusiness;
        private readonly IinterviewsInfoBusiness _interviewBusiness;

        public CreateModel(IMS.Data.Repository.Net17102214ImsContext context)
        {
            _mentorBusiness ??= new MentorBusiness();
            _internBusiness ??= new InternBusiness();
            _interviewBusiness ??= new InterviewsInfoBusiness();
        }

        public IActionResult OnGet()
        {
           var mentorList = _mentorBusiness.GetAllAsync();
            var internList = _internBusiness.Getall();
            if (internList.Result.Data == null || mentorList.Result.Data == null)
            {
                return NotFound();
            }
            ViewData["MentorId"] = new SelectList((System.Collections.IEnumerable)mentorList.Result.Data, "MentorId", "FullName");
            ViewData["InternId"] = new SelectList((System.Collections.IEnumerable)internList.Result.Data, "InternId", "Name");
           
            return Page();
        }

        [BindProperty]
        public InterviewsInfo InterviewsInfo { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _interviewBusiness.AddAsync(InterviewsInfo);
            if (result.Status != Const.SUCCESS_CREATE_CODE)
            {
                OnGet();
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
