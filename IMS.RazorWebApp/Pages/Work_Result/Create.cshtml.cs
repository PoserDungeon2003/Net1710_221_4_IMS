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
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class CreateModel : PageModel
    {
        private readonly WorkingResultBusiness _workingResultBusiness;
        private readonly MentorBusiness _mentorBusiness;
        private readonly InternBusiness _internBusiness;
        private readonly TaskBusiness _taskBusiness;
        public string Message { get; set; } = string.Empty;

        public CreateModel(IMS.Data.Repository.Net17102214ImsContext context)
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
            if (internList.Result.Data == null || mentorList.Result.Data == null || taskList.Result.Data == null)
            {
                return NotFound();
            }
            ViewData["InternId"] = new SelectList((IEnumerable)internList.Result.Data, "InternId", "InternId");
            ViewData["MentorId"] = new SelectList((IEnumerable)mentorList.Result.Data, "MentorId", "MentorId");
            ViewData["TaskId"] = new SelectList((IEnumerable)taskList.Result.Data, "TaskId", "TaskId");
            return Page();
        }

        [BindProperty]
        public WorkingResult WorkingResult { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _workingResultBusiness.AddAsync(WorkingResult);
            Message = result.Message ?? "Unknow error";
            if (result.Status != Const.SUCCESS_CREATE_CODE)
            {
                OnGet();
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
