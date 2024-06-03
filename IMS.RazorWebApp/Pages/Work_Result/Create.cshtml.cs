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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class CreateModel : PageModel
    {
        private readonly WorkingResultBusiness _workingResultBusiness;
        private readonly MentorBusiness _mentorBusiness;
        private readonly InternBusiness _internBusiness;
        private readonly TaskBusiness _taskBusiness;

        public CreateModel(IMS.Data.Repository.Net1710_221_4_IMSContext context)
        {
            _workingResultBusiness ??= new WorkingResultBusiness();
            _mentorBusiness ??= new MentorBusiness();
            _internBusiness ??= new InternBusiness();
            _taskBusiness ??= new TaskBusiness();
        }

        public IActionResult OnGet()
        {
            var internList = _internBusiness.GetAllAsync();
            var mentorList = _mentorBusiness.GetAllAsync();
            var taskList = _taskBusiness.GetAllAsync();
            if (internList.Result.Data != null)
            {
                ViewData["InternId"] = new SelectList((IEnumerable)internList.Result.Data, "InternId", "InternId");
            }
            if (mentorList.Result.Data != null)
            {
                ViewData["MentorId"] = new SelectList((IEnumerable)mentorList.Result.Data, "MentorId", "MentorId");
            }
            if (taskList.Result.Data != null)
            {
                ViewData["TaskId"] = new SelectList((IEnumerable)taskList.Result.Data, "TaskId", "TaskId");
            }
            return Page();
        }

        [BindProperty]
        public WorkingResult WorkingResult { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _workingResultBusiness.AddAsync(WorkingResult);
            if (result.Status != Const.SUCCESS_CREATE_CODE)
            {
                OnGet();
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
