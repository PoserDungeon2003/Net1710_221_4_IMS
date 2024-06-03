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

namespace IMS.RazorWebApp.Pages.Work_Result
{
    public class DeleteModel : PageModel
    {
        private readonly WorkingResultBusiness _workingResultBusiness;

        public DeleteModel()
        {
            _workingResultBusiness = new WorkingResultBusiness();
        }

        [BindProperty]
        public WorkingResult WorkingResult { get; set; } = default!;
        public string Message { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var workingresult = await _workingResultBusiness.GetByIdAsync(id);
            if (workingresult is not null)
            {
                WorkingResult = (workingresult.Data as WorkingResult)!;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var workingresult = await _workingResultBusiness.GetByIdAsync(id);
            // if (workingresult.Status == Const.SUCCESS_READ_CODE)
            // {
            //     WorkingResult = (workingresult.Data as WorkingResult)!;
            //     Message = workingresult.Message ?? "Get data success";
            // }
            // if (workingresult.Status == Const.WARNING_NO_DATA_CODE)
            // {
            //     Message = workingresult.Message ?? "No data";
            // }
            // else
            // {
            //     Message = workingresult.Message ?? "Read data fail";
            // }
            WorkingResult = (workingresult.Data as WorkingResult)!;
            var result = await _workingResultBusiness.Delete(WorkingResult);
            Message = workingresult.Message ?? "";
            if (result.Status != Const.SUCCESS_DELETE_CODE)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
