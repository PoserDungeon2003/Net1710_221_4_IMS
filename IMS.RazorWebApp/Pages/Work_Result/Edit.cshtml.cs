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
using System.Collections;
using IMS.Common;

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class EditModel : PageModel
    {
        private readonly IWorkingResultBusiness _workingResultBusiness;
        private readonly IMentorBusiness _mentorBusiness;
        private readonly ITaskBusiness _taskBusiness;
        private readonly IInternBusiness _internBusiness;

        public EditModel()
        {
            _mentorBusiness ??= new MentorBusiness();
            _taskBusiness ??= new TaskBusiness();
            _internBusiness ??= new InternBusiness();
            _workingResultBusiness ??= new WorkingResultBusiness();
        }

        [BindProperty]
        public WorkingResult WorkingResult { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var workingresult = await _workingResultBusiness.GetByIdAsync(id);
            var mentor = _mentorBusiness.GetAllAsync();
            var task = _taskBusiness.GetAllAsync();
            var intern = _internBusiness.GetAllAsync();

            if (intern.Result.Data == null || mentor.Result.Data == null || task.Result.Data == null || workingresult == null || id == null)
            {
                return NotFound();
            }

            WorkingResult = (workingresult.Data as WorkingResult)!;
            ViewData["InternId"] = new SelectList((IEnumerable)intern.Result.Data, "InternId", "InternId");
            ViewData["MentorId"] = new SelectList((IEnumerable)mentor.Result.Data, "MentorId", "MentorId");
            ViewData["TaskId"] = new SelectList((IEnumerable)task.Result.Data, "TaskId", "TaskId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
                var result = await _workingResultBusiness.Update(WorkingResult);
                if (result.Status != Const.SUCCESS_UPDATE_CODE)
                {
                    await OnGetAsync(WorkingResult.ResultId);
                    return Page();
                }
                 
            return RedirectToPage("./Index");
        }
    }
}
